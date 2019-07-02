using Nanoblog.Core.Data;
using Nanoblog.Core.Data.Exception;

namespace Nanoblog.Api.Data.Models
{
    public class CommentKarma : KarmaBase
    {
        public Comment Comment { get; private set; }

        public CommentKarma()
            : base()
        {

        }

        public CommentKarma(User author, Comment comment, KarmaValue value)
            : base(author, value)
        {
            SetComment(comment);
        }

        private void SetComment(Comment comment)
        {
            if (comment is null)
            {
                throw new ApiException("Invalid karma comment id");
            }

            Comment = comment;
        }
    }
}
