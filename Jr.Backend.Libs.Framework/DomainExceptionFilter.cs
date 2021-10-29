using FluentValidation;
using Jr.Backend.Libs.Domain.Abstractions.Interfaces.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Jr.Backend.Libs.Framework
{
    public sealed class DomainExceptionFilter : IExceptionFilter
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;

        public DomainExceptionFilter()
        {
            exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
                {typeof(DomainException), HandleDomainException },
                {typeof(AlreadyRegisteredException), HandleAlreadyRegisteredException },
                { typeof(InfrastructureException), HandleInfrastructureException }
            };
        }

        public void OnException(ExceptionContext context)
        {
            HandleException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (exceptionHandlers.ContainsKey(type))
            {
                exceptionHandlers[type].Invoke(context);
                return;
            }
            HandleInfrastructureException(context);
        }

        private void HandleAlreadyRegisteredException(ExceptionContext context)
        {
            var exception = context.Exception as AlreadyRegisteredException;

            var details = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                Title = "The specified resource already registred",
                Detail = exception.Message,
                Status = StatusCodes.Status409Conflict
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status409Conflict
            };

            context.ExceptionHandled = true;
        }

        private void HandleDomainException(ExceptionContext context)
        {
            var domainException = context.Exception as DomainException;

            var details = new ProblemDetails()
            {
                Type = "",
                Title = "Unprocessable Entity",
                Detail = domainException.Message,
                Status = StatusCodes.Status422UnprocessableEntity
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }

        private void HandleInfrastructureException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                Detail = context.Exception.Message
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "BadRequest",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = string.Join("\n", exception.Errors)
            };
            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}