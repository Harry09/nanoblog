using Nanoblog.Common;
using Nanoblog.Common.Exception;

namespace Nanoblog.Api.Data.Models
{
    public class EntryKarma : KarmaBase
    {
        public Entry Entry { get; private set; }

        public EntryKarma()
            : base()
        {

        }

        public EntryKarma(User author, Entry entry, KarmaValue value)
            : base(author, value)
        {
            SetEntry(entry);
        }

        private void SetEntry(Entry entry)
        {
            if (entry is null)
            {
                throw new ApiException("Invalid karma entry id");
            }

            Entry = entry;
        }
    }
}
