using EchoLife.Will.Models;

namespace EchoLife.Will.Dtos;

public record WillVersionResponse(
    string Id,
    string WillId,
    string Value,
    string WillType,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    public static WillVersionResponse From(WillVersion willVersion)
    {
        return new WillVersionResponse(
            willVersion.Id,
            willVersion.WillId,
            willVersion.Content,
            willVersion.WillType,
            willVersion.CreatedAt,
            willVersion.UpdatedAt
        );
    }
}
