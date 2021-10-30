using Jr.Backend.Libs.Domain.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jr.Backend.Libs.Framework
{
    public sealed class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException)
            {
                var exception = context.Exception as DomainException;

                var details = new ProblemDetails()
                {
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/400",
                    Title = "Bad Request",
                    Detail = exception.Message,
                    Status = StatusCodes.Status400BadRequest
                };

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is AlreadyRegisteredException)
            {
                var exception = context.Exception as AlreadyRegisteredException;

                var details = new ProblemDetails()
                {
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/409",
                    Title = "The request could not be completed due to a conflict.",
                    Detail = exception.Message,
                    Status = StatusCodes.Status409Conflict
                };

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status409Conflict
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is ForbiddenAccessException)
            {
                var exception = context.Exception as ForbiddenAccessException;

                var details = new ProblemDetails()
                {
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/403",
                    Title = "The client did not have permission to access the requested resource.",
                    Detail = exception.Message,
                    Status = StatusCodes.Status403Forbidden
                };

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is InfrastructureException)
            {
                var exception = context.Exception as InfrastructureException;

                var details = new ProblemDetails()
                {
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/503",
                    Title = "The server was unavailable.",
                    Detail = exception.Message,
                    Status = StatusCodes.Status503ServiceUnavailable
                };

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is NotFoundException)
            {
                var exception = context.Exception as NotFoundException;

                var details = new ProblemDetails()
                {
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404",
                    Title = "The requested resource was not found.",
                    Detail = exception.Message,
                    Status = StatusCodes.Status404NotFound
                };

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is NoContentException)
            {
                var exception = context.Exception as NoContentException;

                var details = new ProblemDetails()
                {
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/204",
                    Title = "The server has fulfilled the request but does not need to return a response body.",
                    Detail = exception.Message,
                    Status = StatusCodes.Status204NoContent
                };

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status204NoContent
                };

                context.ExceptionHandled = true;
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500",
                    Title = "The request was not completed due to an internal error on the server side.",
                    Detail = context.Exception.Message,
                    Status = StatusCodes.Status500InternalServerError
                };

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}