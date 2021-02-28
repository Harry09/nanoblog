using Nanoblog.Common.Exception;
using Nanoblog.Common.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Nanoblog.Api.Data.Models
{
    public class Entry
    {
        [Key]
        public string Id { get; private set; }

        public User Author { get; private set; }

        public string Text { get; private set; }

        public bool Deleted { get; private set; }

        public DateTime CreateTime { get; private set; }

        public Entry()
        {
            CreateTime = DateTime.Now;
        }

        public Entry(string id, User user, string text) : this()
        {
            Id = id;
            SetAuthor(user);
            SetText(text);
        }

        private void SetAuthor(User user)
        {
            if (user is null)
            {
                throw new ApiException("Invalid entry user id");
            }

            Author = user;
        }

        public void SetText(string text)
        {
            if (text is null || text.IsEmpty())
            {
                throw new ApiException("Entry text is empty");
            }

            Text = text.Trim();
        }

        public void Delete()
        {
            Deleted = true;
        }
    }
}
