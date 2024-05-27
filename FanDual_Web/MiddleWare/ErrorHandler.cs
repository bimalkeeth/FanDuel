using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FanDual_Web.MiddleWare;

public class ErrorHandler(ILogger<ErrorHandler> logger) : ExceptionFilterAttribute
{
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            // Some logic to handle specific exceptions
            var errorMessage = context.Exception is ArgumentException
                ? "ArgumentException occurred"
                : "Some unknown error occurred";

            // Maybe, logging the exception
            logger.LogError(context.Exception, errorMessage);

            // Returning response
            context.Result = new BadRequestResult();
        }
}