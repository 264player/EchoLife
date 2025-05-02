using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EchoLife.Common.Exceptions;

public class ExceptionHandlingAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<
            ILogger<ExceptionHandlingAttribute>
        >();

        if (context.Exception is InternalException e)
        {
            logger.LogWarning(
                e,
                "InternalException occurred: {Error} - {ErrorInfo}",
                e.ExceptionInfo.Error,
                e.ExceptionInfo.ErrorInfo
            );

            context.Result = new JsonResult(e.ExceptionInfo)
            {
                StatusCode = (int)HandleException(context.Exception),
            };
            return;
        }
        var einfo = new ExceptionInfo
        {
            Error = "Internal Exception",
            ErrorInfo = context.Exception.Message,
        };
        context.Result = new JsonResult(einfo)
        {
            StatusCode = (int)HandleException(context.Exception),
        };

        logger.LogWarning(
            context.Exception,
            "InternalException occurred: {Error} - {ErrorInfo}",
            einfo.Error,
            einfo.ErrorInfo
        );
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
