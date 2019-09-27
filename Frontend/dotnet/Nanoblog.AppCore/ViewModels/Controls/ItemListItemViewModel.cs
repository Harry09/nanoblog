using Nanoblog.Common;
using Nanoblog.ApiService;
using Nanoblog.ApiService.Api;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.AppCore.ViewModels.Controls
{
    public class ItemListItemViewModel : BaseViewModel
    {
        private string _userName;
        private string _date;
        private string _text;
        private int _karmaCount;
        private bool _isDeletable;
        private bool _deleted;
        private bool _isUpVote;
        private bool _isDownVote;

        private KarmaValue _voteType;

        public string UserName
        {
            get => _userName;
            set => Update(ref _userName, value);
        }

        public string Date
        {
            get => _date;
            set => Update(ref _date, value);
        }

        public string Text
        {
            get => _text;
            set => Update(ref _text, value);
        }

        public int KarmaCount
        {
            get => _karmaCount;
            set => Update(ref _karmaCount, value);
        }

        public bool IsDeletable
        {
            get => _isDeletable;
            set => Update(ref _isDeletable, value);
        }

        public bool Deleted
        {
            get => _deleted;
            set => Update(ref _deleted, value);
        }

        public bool IsUpVote
        {
            get => _isUpVote;
            set => Update(ref _isUpVote, value);
        }

        public bool IsDownVote
        {
            get => _isDownVote;
            set => Update(ref _isDownVote, value);
        }

        public KarmaValue UserVote
        {
            get => _voteType;
            set
            {
                _voteType = value;

                switch (value)
                {
                    case KarmaValue.Plus:
                        IsUpVote = true;
                        IsDownVote = false;
                        break;
                    case KarmaValue.None:
                        IsUpVote = false;
                        IsDownVote = false;
                        break;
                    case KarmaValue.Minus:
                        IsUpVote = false;
                        IsDownVote = true;
                        break;
                }
            }
        }

        public bool InsideDetail { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand ChangeVoteCommand { get; set; }

        protected async Task OnChangeVote(KarmaService.VoteFor voteFor, KarmaValue karmaValue, string itemId)
        {
            // if user is author of this entry
            if (IsDeletable)
                return;

            if (karmaValue == UserVote)
            {
                await KarmaService.Instance.RemoveVote(voteFor, itemId);
            }
            else
            {
                switch (karmaValue)
                {
                    case KarmaValue.Plus:
                        await KarmaService.Instance.UpVote(voteFor, itemId);
                        break;
                    case KarmaValue.Minus:
                        await KarmaService.Instance.DownVote(voteFor, itemId);
                        break;
                    case KarmaValue.None:
                        break;
                }
            }

            await RefreshKarma(voteFor, itemId);
        }

        protected async Task RefreshKarma(KarmaService.VoteFor voteFor, string itemId)
        {
            UserVote = await KarmaService.Instance.GetUserVote(voteFor, new GetUserVoteParams { ItemId = itemId, UserId = App.CurrentUser.Id });

            KarmaCount = await KarmaService.Instance.CountKarma(voteFor, itemId);
        }
    }
}
