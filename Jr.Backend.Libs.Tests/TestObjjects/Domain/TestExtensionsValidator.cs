using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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