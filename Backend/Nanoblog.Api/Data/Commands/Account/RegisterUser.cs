using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Commands.Account
{
    public class RegisterUser
    {
		public string Email { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }
	}
}
