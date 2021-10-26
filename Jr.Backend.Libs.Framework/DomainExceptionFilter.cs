using FluentValidation;
using Jr.Backend.Libs.Domain.Abstractions.Interfaces.Exceptions;
using Jr.Backend.Libs.Framework.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace Jr.Backend.Libs.Framework
{
    public sealed class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException)
            {
                var domainException = context.Exception as DomainException;

                context.Result = new ObjectResult(new ErrorResult(domainException));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            }
            else if (context.Exception is ValidationException)
            {
                var validationException = context.Exception as ValidationException;

                context.Result = new ObjectResult(new ErrorResult(validationException?.Errors?.Select(s => s.ErrorMessage)));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            }
            else if (context.Exception is NotFoundException)
            {
                var applicationException = context.Exception as NotFoundException;

                context.Result = new ObjectResult(new ErrorResult(applicationException));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (context.Exception is AlreadyRegisteredException)
            {
                var applicationException = context.Exception as AlreadyRegisteredException;
                context.Result = new ObjectResult(new ErrorResult(applicationException));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else if (context.Exception is InfrastructureException)
            {
                var infrastructureException = context.Exception as InfrastructureException;

                context.Result = new ObjectResult(new ErrorResult(infrastructureException));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                context.Result = new ObjectResult(new ErrorResult(context.Exception));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}