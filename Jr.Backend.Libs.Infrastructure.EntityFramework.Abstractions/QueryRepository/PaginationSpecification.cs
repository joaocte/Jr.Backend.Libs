using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository
{
    public class PaginationSpecification<T> : SpecificationBase<T>
         where T : class
    {
        /// <summary>
        /// Gets or sets the current page index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
    }
}