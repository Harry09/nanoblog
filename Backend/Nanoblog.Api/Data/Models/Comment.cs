using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Models
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public User Author { get; set; }

        public Entry Parent { get; set; }

        [Required]
        public string Text { get; set; }

        public bool Deleted { get; set; }

        [Required]
        public DateTime CreateTime { get; private set; }

        public Comment()
        {
            CreateTime = DateTime.Now;
        }
    }
}
