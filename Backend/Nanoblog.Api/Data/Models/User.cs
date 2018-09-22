using System;
using System.ComponentModel.DataAnnotations;

namespace Nanoblog.Api.Data.Models
{
    public class User
    {
		[Key]
		public string Id { get; set; }

		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string Salt { get; set; }

		[Required]
		public string Role { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required]
		public DateTime JoinTime { get; protected set; }

		public User()
		{
			JoinTime = DateTime.Now;
		}
	}
}
