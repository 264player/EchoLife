using EchoLife.Life.Dtos;
using FluentValidation;

namespace EchoLife.Life.Validators;

public class QueryLifePointsRequestValidator : AbstractValidator<QueryLifePointsRequest>
{
    public QueryLifePointsRequestValidator()
    {
        RuleFor(q => q.Count).GreaterThanOrEqualTo(0);
    }
}
