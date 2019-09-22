using Nanoblog.Common.Commands.Comment;
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
        private EntryListItemViewModel _entryVM;

        public EntryListItemViewModel EntryVM
        {
            get => _entryVM;
            set => Update(ref _entryVM, value);
        }

        public CommentListViewModel CommentsVM { get; } = new CommentListViewModel();

        public UserAppBarViewModel UserAppBarVM { get; } = new UserAppBarViewModel();

        public ICommand BackCommand { get; set; }

        public ICommand AddCommentCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public EntryDetailPageViewModel(EntryListItemViewModel entry)
        {
            BackCommand = new RelayCommand(OnBack);
            AddCommentCommand = new RelayCommand(OnAddComment);
            RefreshCommand = new RelayCommand(OnRefresh);

            EntryVM = entry;

            entry.InsideDetail = true;

            _ = LoadCommentsList();
        }

        async Task LoadCommentsList()
        {
            var comments = await CommentService.Instance.GetComments(_entryVM.Id);

            CommentsVM.LoadData(comments);

            EntryVM.CommentsCount = CommentsVM.List.Count;
        }

        void OnBack()
        {
            _entryVM.InsideDetail = false;
            PageNavigator.Instance.Pop();
        }

        void OnAddComment()
        {
            PageNavigator.Instance.Push<AddPageViewModel>(async m =>
            {
                if (!m.Cancelled)
                {
                    await CommentService.Instance.Add(new AddComment
                    {
                        Text = m.Text,
                        EntryId = EntryVM.Id
                    });

                    await LoadCommentsList();
                }
            });
        }

        async void OnRefresh()
        {
            await LoadCommentsList();
        }
    }
}
