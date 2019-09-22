using Nanoblog.Common;
using Nanoblog.Common.Dto;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.Services;
using Nanoblog.Core.Services.Api;
using Nanoblog.Core.ViewModels.Pages;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels.Controls.EntryList
{
    public class EntryListItemViewModel : BaseViewModel
    {
        private EntryDto _entryData;

        private string _userName;
        private string _date;
        private string _text;
        private int _commentsCount;
        private int _karmaCount;
        private bool _isDeletable;
        private bool _deleted;
        private bool _isUpVote;
        private bool _isDownVote;

        private KarmaValue _voteType;

        public string Id { get => _entryData.Id; }

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

        public int CommentsCount
        {
            get => _commentsCount;
            set => Update(ref _commentsCount, value);
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

        public KarmaValue Vote
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

        public ICommand ShowCommentsCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand ChangeVoteCommand { get; set; }

        public EntryListItemViewModel(EntryDto entry)
        {
            ShowCommentsCommand = new RelayCommand(OnShowComments);
            DeleteCommand = new RelayCommand(OnDelete);
            ChangeVoteCommand = new RelayCommand<KarmaValue>(OnChangeVote);

            _entryData = entry;
            UserName = entry.Author.UserName;
            Date = entry.CreateTime.ToString();
            Text = entry.Text;
            CommentsCount = entry.CommentsCount;
            KarmaCount = entry.KarmaCount;
            Vote = entry.UserVote;

            IsDeletable = entry.Author.Id == App.CurrentUser.Id;
        }

        void OnShowComments()
        {
            if (!InsideDetail)
            {
                PageNavigator.Instance.Push<EntryDetailPageViewModel, EntryListItemViewModel>(this);
            }
        }

        async void OnDelete()
        {
            if (IsDeletable)
            {
                await EntryService.Instance.Delete(_entryData.Id);

                Deleted = true;

                if (InsideDetail)
                {
                    PageNavigator.Instance.Pop();
                }
            }
        }

        async void OnChangeVote(KarmaValue karmaValue)
        {
            // if user is author of this entry
            if (IsDeletable)
                return;

            if (karmaValue == Vote)
            {
                await KarmaService.Instance.RemoveVote(KarmaService.VoteFor.Entry, _entryData.Id);
            }
            else
            {
                switch (karmaValue)
                {
                    case KarmaValue.Plus:
                        await KarmaService.Instance.UpVote(KarmaService.VoteFor.Entry, _entryData.Id);
                        break;
                    case KarmaValue.Minus:
                        await KarmaService.Instance.DownVote(KarmaService.VoteFor.Entry, _entryData.Id);
                        break;
                    case KarmaValue.None:
                        break;
                }
            }

            await RefreshKarma();
        }

        async Task RefreshKarma()
        {
            Vote = await KarmaService.Instance.GetUserVote(KarmaService.VoteFor.Entry, new GetUserVoteParams { ItemId = _entryData.Id, UserId = App.CurrentUser.Id });

            KarmaCount = await KarmaService.Instance.CountKarma(KarmaService.VoteFor.Entry, _entryData.Id);
        }
    }
}
