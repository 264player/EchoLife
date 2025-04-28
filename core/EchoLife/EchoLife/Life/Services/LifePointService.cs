using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Life.Data;
using EchoLife.Life.Dtos;
using EchoLife.Life.Models;
using FluentValidation;

namespace EchoLife.Life.Services;

public class LifePointService(
    ILifePointRepository _lifePointRepository,
    ILifePointUserMapRepository _lifePointUserMapRepository,
    IValidator<QueryLifePointsRequest> queryLifePointsRequestValidator
) : ILifePointService
{
    public async Task<LifePointResponse> CreateLifePointAsync(
        ClaimsPrincipal user,
        LifePointRequest lifePointRequest
    )
    {
        var userId = ClaimsManager.GetUserId(user)!;

        var point = new LifePoint
        {
            Id = IdGenerator.GenerateUlid(),
            Content = lifePointRequest.Content,
            Hidden = lifePointRequest.Hidden,
            UserId = userId,
        };
        var lifePoint =
            await _lifePointRepository.CreateAsync(point) ?? throw new UnknowException();
        await _lifePointUserMapRepository.CreateAsync(
            new PointUserMap
            {
                Id = Guid.NewGuid().ToString(),
                UserId = point.UserId,
                PointId = point.Id,
            }
        );

        return LifePointResponse.From(lifePoint);
    }

    public async Task<LifePointResponse> GetLifePointAsync(ClaimsPrincipal me, string pointId)
    {
        var myId = ClaimsManager.GetUserId(me);

        var point = await _lifePointRepository.ReadAsync(pointId);

        if (point == null || (point.UserId != myId && point.Hidden))
        {
            throw new LifePointNotFoundException(pointId);
        }

        return LifePointResponse.From(point);
    }

    public async Task<List<LifePoint>> GetLifePointByUserIdAsync(
        ClaimsPrincipal me,
        string userId,
        QueryLifePointsRequest queryLifePointsRequest
    )
    {
        queryLifePointsRequestValidator.ValidateAndThrow(queryLifePointsRequest);

        var myId = ClaimsManager.GetUserId(me);

        return await _lifePointRepository.ReadAsync(
            p =>
                (p.UserId == userId)
                && (myId == userId || !p.Hidden)
                && (
                    queryLifePointsRequest.CursorId == null
                    || p.Id.CompareTo(queryLifePointsRequest.CursorId) < 0
                ),
            queryLifePointsRequest.Count
        );
    }

    public async Task<LifePointResponse> UpdateLifePointAsync(
        ClaimsPrincipal me,
        string pointId,
        LifePointRequest lifePointRequest
    )
    {
        var point = await EnsureAndGetLifePoint(pointId);

        var myId = ClaimsManager.GetAuthorizedUserId(me);

        if (point.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        var updatedPoint =
            await _lifePointRepository.UpdateAsync(Update(point, lifePointRequest))
            ?? throw new UnknowException();

        return LifePointResponse.From(updatedPoint);

        static LifePoint Update(LifePoint lifePoint, LifePointRequest lifePointRequest)
        {
            lifePoint.Hidden = lifePointRequest.Hidden;
            lifePoint.Content = lifePointRequest.Content;
            return lifePoint;
        }
    }

    public async Task DeleteLifePointAsync(ClaimsPrincipal me, string pointId)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var point = await EnsureAndGetLifePoint(pointId);

        if (point.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await _lifePointRepository.DeleteAsync(pointId);
    }

    public async Task JoinLifePointAsync(
        ClaimsPrincipal me,
        string pointId,
        IEnumerable<string> userIdList
    )
    {
        string? myId = ClaimsManager.GetAuthorizedUserId(me);

        foreach (var userId in userIdList)
        {
            await _lifePointUserMapRepository.CreateAsync(
                new PointUserMap
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    PointId = pointId,
                }
            );
        }
    }

    public async Task LeaveLifePointAsync(ClaimsPrincipal me, string pointId)
    {
        string? myId = ClaimsManager.GetAuthorizedUserId(me);
        await _lifePointUserMapRepository.DeleteAsync(myId, pointId);
    }

    private async Task<LifePoint> EnsureAndGetLifePoint(string pointId)
    {
        return await _lifePointRepository.ReadAsync(pointId)
            ?? throw new LifePointNotFoundException(pointId);
    }
}
