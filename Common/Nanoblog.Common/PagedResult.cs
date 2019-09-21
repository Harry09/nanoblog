using Nanoblog.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nanoblog.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public static PagedResult<T> Create(PagedQuery pagedQuery, IEnumerable<T> items)
        {
            var pagedResult = new PagedResult<T>();

            var currentPage = Math.Max(0, pagedQuery.CurrentPage);
            var skip = currentPage * pagedQuery.LimitPerPage;

            pagedResult.Items = items.Skip(skip).Take(pagedQuery.LimitPerPage);

            return pagedResult;
        }
    }
}
