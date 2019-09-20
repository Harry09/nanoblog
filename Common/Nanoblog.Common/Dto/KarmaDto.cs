using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Common.Dto
{
    public class KarmaDto
    {
        public string Id { get; set; }

        public UserDto Author { get; set; }

        public KarmaValue Value { get; set; }
    }
}
