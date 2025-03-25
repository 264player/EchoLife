using EchoLife.Common.Exceptions;
using FluentValidation;

namespace EchoLife.Common.Validation;

public static class FluentValidationExtensions
{
    public static void ValidateAndThrowArgumentException<T>(
        this IValidator<T> validator,
        T instance
    )
    {
        var result = validator.Validate(instance);

        if (!result.IsValid)
        {
            var ex = new ValidationException(result.Errors);
            throw new EntityArgumentException(ex.Message, ex);
        }
    }
}
