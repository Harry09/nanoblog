using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Common.Data.Dto
{
    public class AuthorDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public DateTime JoinTime { get; set; }
    }
}
