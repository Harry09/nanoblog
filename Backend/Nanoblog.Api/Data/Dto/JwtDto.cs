using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Dto
{
    public class JwtDto
    {
		public string Token { get; set; }

		public string RefreshToken { get; set; }

		public long Expires { get; set; }
	}
}
