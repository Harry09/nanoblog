using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Data.Dto
{
    public class UserDto
    {
		public string Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string Role { get; set; }

		public DateTime JoinTime { get; set; }
	}
}
