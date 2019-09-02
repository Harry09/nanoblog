using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Data.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public string ParentId { get; set; }

        public string Text { get; set; }

        public DateTime CreateTime { get; set; }

        public int KarmaCount { get; set; }

        public KarmaValue UserVote { get; set; }
    }
}
