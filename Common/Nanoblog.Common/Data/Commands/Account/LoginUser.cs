using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Common.Data.Commands.Account
{
    public class LoginUser
    {
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
