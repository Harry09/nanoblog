using Nanoblog.Common.Dto;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.Services;
using Nanoblog.Core.ViewModels.Pages;
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
        private bool _isDeletable;
        private bool _deleted;

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

        public bool InsideDetail { get; set; }

        public ICommand ShowCommentsCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public EntryListItemViewModel(EntryDto entry)
        {
            ShowCommentsCommand = new RelayCommand(OnShowComments);
            DeleteCommand = new RelayCommand(OnDelete);

            _entryData = entry;
            UserName = entry.Author.UserName;
            Date = entry.CreateTime.ToString();
            Text = entry.Text;
            CommentsCount = entry.CommentsCount;

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
    }
}
