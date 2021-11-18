using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Jr.Backend.Libs.Extensions.Filter
{
    internal class ExpressionParserCollection : List<ExpressionParser>
    {
        public ParameterExpression ParameterExpression { get; set; }

        public List<ExpressionParser> Ordered()
        {
            return this.OrderBy(b => b.Criteria.UseOr).ToList();
        }
    }
}