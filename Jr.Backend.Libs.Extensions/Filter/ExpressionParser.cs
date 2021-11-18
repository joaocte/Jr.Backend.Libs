using System.Linq.Expressions;

namespace Jr.Backend.Libs.Extensions.Filter
{
    internal class ExpressionParser
    {
        public WhereClause Criteria { get; set; }
        public Expression FieldToFilter { get; set; }
        public Expression FilterBy { get; set; }
    }
}