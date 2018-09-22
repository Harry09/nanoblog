using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Models
{
    public class Entry
    {
		[Key]
		public string Id { get; set; }

		[Required]
		public User Author { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public DateTime CreateTime { get; protected set; }

		public DateTime UpdateTime { get; set; }

		public Entry()
		{
			CreateTime = DateTime.Now;
		}

	}
}
