using System.ComponentModel.DataAnnotations;

namespace Nanoblog.Api.Data.Models
{
    public class RefreshToken
    {
        [Key]
		public string Token { get; set; }

		public User User { get; set; }

		public bool Revoked { get; set; } = false;
	}
}
