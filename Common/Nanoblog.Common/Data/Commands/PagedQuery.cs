using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Common.Data.Commands
{
    public class PagedQuery
    {
        public int CurrentPage { get; set; }

        public int LimitPerPage { get; set; }
    }
}
