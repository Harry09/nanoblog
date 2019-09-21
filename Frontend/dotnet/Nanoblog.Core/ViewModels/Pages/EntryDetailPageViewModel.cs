using Nanoblog.Common.Dto;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.Services;
using Nanoblog.Core.ViewModels.Controls.AppBar;
using Nanoblog.Core.ViewModels.Controls.CommentList;
using Nanoblog.Core.ViewModels.Controls.EntryList;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class EntryDetailPageViewModel : BaseViewModel
    {
        private EntryListItemViewModel _entry;
        private CommentListViewModel _comments;

        public EntryListItemViewModel Entry
        {
            get => _entry;
            set => Update(ref _entry, value);
        }

        public CommentListViewModel Comments { get; private set; }

        public UserAppBarViewModel UserAppBarVM => new UserAppBarViewModel();

        public ICommand BackCommand { get; set; }

        public EntryDetailPageViewModel(EntryListItemViewModel entry)
        {
            Entry = entry;
            Comments = new CommentListViewModel(); 

            entry.InsideDetail = true;

            BackCommand = new RelayCommand(OnBack);

            _ = LoadCommentsList();
        }

        async Task LoadCommentsList()
        {
            var comments = await CommentService.Instance.GetComments(_entry.Id);

            Comments.LoadData(comments);
        }

        void OnBack()
        {
            _entry.InsideDetail = false;
            PageNavigator.Instance.Pop();
        }
    }
}
