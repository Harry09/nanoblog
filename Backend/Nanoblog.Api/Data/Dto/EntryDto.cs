using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Dto
{
    public class EntryDto
    {
		public string Id { get; set; }

		public UserDto Author { get; set; }

		public string Text { get; set; }

		public DateTime CreateTime { get; set; }
	}
}
