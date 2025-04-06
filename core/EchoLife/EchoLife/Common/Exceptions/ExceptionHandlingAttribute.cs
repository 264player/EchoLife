using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EchoLife.Common.Exceptions;

public class ExceptionHandlingAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Result = new JsonResult(new { Error = context.Exception.Message })
        {
            StatusCode = (int)HandleException(context.Exception),
        };
    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        return Task.FromResult(
            context.Result = new JsonResult(new { Error = context.Exception.Message })
            {
                StatusCode = (int)HandleException(context.Exception),
            }
        );
    }

    public static HttpStatusCode HandleException(Exception exception)
    {
        return exception switch
        {
            ForbiddenException => HttpStatusCode.Forbidden,
            ResourceNotFoundException => HttpStatusCode.NotFound,
            EntityArgumentException => HttpStatusCode.UnprocessableEntity,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
