using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Models
{
    public class RefreshToken
    {
		[Key]
		public string Token { get; set; }

		[Required]
		public User User { get; set; }

		[Required]
		public bool Revoked { get; set; } = false;
	}
}
