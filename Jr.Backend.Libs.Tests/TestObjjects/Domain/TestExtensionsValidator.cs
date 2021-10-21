using FluentValidation;
using System;

namespace Jr.Backend.Libs.Tests.TestObjjects.Domain
{
    public class TestExtensionsValidator<T> : InlineValidator<T>
    {
        public TestExtensionsValidator()
        { }

        public TestExtensionsValidator(params Action<TestExtensionsValidator<T>>[] actionList)
        {
            foreach (var action in actionList) action.Invoke(this);
        }
    }
}