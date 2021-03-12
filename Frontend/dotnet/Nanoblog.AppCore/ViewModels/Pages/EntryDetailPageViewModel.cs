using Nanoblog.Common.Commands.Comment;
using Nanoblog.AppCore.Navigation;
using Nanoblog.ApiService;
using Nanoblog.AppCore.ViewModels.Controls.AppBar;
using Nanoblog.AppCore.ViewModels.Controls.CommentList;
using Nanoblog.AppCore.ViewModels.Controls.EntryList;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.AppCore.ViewModels.Pages
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
            PageNavigator.Instance.Push(new AddPageViewModel
            {
                OnAdd = async m =>
                {
                    await CommentService.Instance.Add(new AddComment
                    {
                        Text = m.Text,
                        EntryId = EntryVM.Id
                    });

                    PageNavigator.Instance.Pop();

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
