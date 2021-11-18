using Jr.Backend.Libs.Extensions.Filter;
using System;

namespace Jr.Backend.Libs.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryOperatorAttribute : Attribute
    {
        public WhereOperator Operator { get; set; } = WhereOperator.Equals;
        public bool UseNot { get; set; } = false;
        public bool CaseSensitive { get; set; } = true;
        public string HasName { get; set; }
        public int Max { get; set; }
        public bool UseOr { get; set; } = false;
    }
}