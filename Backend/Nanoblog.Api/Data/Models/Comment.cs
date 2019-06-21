using Nanoblog.Core.Data.Exception;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Nanoblog.Core.Extensions;

namespace Nanoblog.Api.Data.Models
{
    public class Comment
    {
        [Key]
        public string Id { get; private set; }

        public User Author { get; private set; }

        public Entry Parent { get; private set; }

        public string Text { get; private set; }

        public bool Deleted { get; private set; }

        public DateTime CreateTime { get; }

        public Comment()
        {
            CreateTime = DateTime.Now;
        }

        public Comment(User user, Entry entry, string text) : this()
        {
            SetAuthor(user);
            SetParent(entry);
            SetText(text);
        }

        public void SetAuthor(User user)
        {
            if (user is null)
            {
                throw new ApiException("Invalid comment author id");
            }

            Author = user;
        }

        public void SetParent(Entry entry)
        {
            if (entry is null)
            {
                throw new ApiException("Invalid comment parent id");
            }

            Parent = entry;
        }

        public void SetText(string text)
        {
            if (text is null || text.IsEmpty())
            {
                throw new ApiException("Comment text is empty");
            }

            Text = text.Trim();
        }

        public void Delete()
        {
            Deleted = true;
        }
    }
}
