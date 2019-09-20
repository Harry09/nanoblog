using Nanoblog.Common;
using Nanoblog.Common.Exception;
using System.ComponentModel.DataAnnotations;

namespace Nanoblog.Api.Data.Models
{
    public class KarmaBase
    {
        [Key]
        public string Id { get; private set; }

        public User Author { get; private set; }

        public KarmaValue Value { get; private set; }

        public KarmaBase()
        {
        }

        public KarmaBase(User author, KarmaValue value)
        {
            SetAuthor(author);
            SetValue(value);
        }

        private void SetAuthor(User user)
        {
            if (user is null)
            {
                throw new ApiException("Invalid karma entry id");
            }

            Author = user;
        }

        public void SetValue(KarmaValue value)
        {
            Value = value;
        }
    }
}
