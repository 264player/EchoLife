using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EchoLife.Common.Exceptions;

public class ExceptionHandlingAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is InternalException e)
        {
            context.Result = new JsonResult(e.ExceptionInfo)
            {
                StatusCode = (int)HandleException(context.Exception),
            };
            return;
        }

        context.Result = new JsonResult(
            new ExceptionInfo
            {
                Error = "Internal Exception",
                ErrorInfo = context.Exception.Message,
            }
        )
        {
            StatusCode = (int)HandleException(context.Exception),
        };
    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is InternalException e)
        {
            context.Result = new JsonResult(e.ExceptionInfo)
            {
                StatusCode = (int)HandleException(context.Exception),
            };
            return Task.CompletedTask;
        }

        context.Result = new JsonResult(
            new ExceptionInfo
            {
                Error = "Internal Exception",
                ErrorInfo = context.Exception.Message,
            }
        )
        {
            StatusCode = (int)HandleException(context.Exception),
        };
        return Task.CompletedTask;
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
