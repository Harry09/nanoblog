using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Settings
{
    public class JwtSettings
    {
		public string Key { get; set; }

		public string Issuer { get; set; }

		public int ExpiryMinutes { get; set; }
	}
}
