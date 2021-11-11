using Jr.Backend.Libs.Domain.Abstractions.Exceptions;
using Jr.Backend.Libs.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Jr.Backend.Libs.Tests.Framework
{
    public class DomainExceptionFilterTests
    {
        private readonly ActionContext actionContext;
        private readonly ActionDescriptor actionDescriptor;
        private readonly CustomExceptionFilter domainExceptionFilter;
        private readonly ExceptionContext exceptionContext;
        private readonly List<IFilterMetadata> filters;
        private readonly HttpContext httpContext;
        private readonly RouteData routeData;

        public DomainExceptionFilterTests()
        {
            httpContext = Substitute.For<HttpContext>();
            routeData = Substitute.For<RouteData>();
            actionDescriptor = Substitute.For<ActionDescriptor>();
            actionContext = Substitute.For<ActionContext>(httpContext, routeData, actionDescriptor);
            filters = Substitute.For<List<IFilterMetadata>>();
            exceptionContext = Substitute.For<ExceptionContext>(actionContext, filters);
            domainExceptionFilter = new CustomExceptionFilter();
        }

        [Fact]
        public void WhenContextHasAlreadyRegisteredException_RetornContextResultWithConflict()
        {
            var expectedStatusCode = (int)HttpStatusCode.Conflict;
            var exceptionMsg = "msg-qualquer";
            Exception exception = Substitute.For<AlreadyRegisteredException>(exceptionMsg);
            exception.Message.Returns(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            domainExceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result.StatusCode);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
        }
    }
}