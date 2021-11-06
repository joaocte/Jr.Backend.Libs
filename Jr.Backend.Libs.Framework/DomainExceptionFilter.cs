﻿using Jr.Backend.Libs.Domain.Abstractions.Exceptions;
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
            exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>> {
                {typeof(DomainException), DomainExceptionHandler},
                {typeof(AlreadyRegisteredException), AlreadyRegisteredExceptionHandler},
                {typeof(ForbiddenAccessException), ForbiddenAccessExceptionHandler},
                {typeof(InfrastructureException), InfrastructureExceptionHandler},
                {typeof(NotFoundException), NotFoundExceptionHandler},
                {typeof(NoContentException), NoContentExceptionHandler}
            };
        }

        public void OnException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (exceptionHandlers.ContainsKey(type))
            {
                exceptionHandlers[type].Invoke(context);
                return;
            }
            InfrastructureExceptionHandler(context);
        }

        private void AlreadyRegisteredExceptionHandler(ExceptionContext context)
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

        private void DomainExceptionHandler(ExceptionContext context)
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

        private void ForbiddenAccessExceptionHandler(ExceptionContext context)
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

        private void InfrastructureExceptionHandler(ExceptionContext context)
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

        private void NoContentExceptionHandler(ExceptionContext context)
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

        private void NotFoundExceptionHandler(ExceptionContext context)
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
    }
}