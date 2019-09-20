using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Common.Commands.Comment
{
    public class AddComment
    {
        public string EntryId { get; set; }

        public string Text { get; set; }
    }
}
