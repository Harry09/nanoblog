using Nanoblog.Common.Commands.Entry;
using Nanoblog.AppCore.Navigation;
using Nanoblog.ApiService;
using Nanoblog.AppCore.ViewModels.Controls.AppBar;
using Nanoblog.AppCore.ViewModels.Controls.EntryList;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.AppCore.ViewModels.Pages
{
    public class EntryListPageViewModel : BaseViewModel
    {
        public EntryListViewModel EntryListVM { get; set; } = new EntryListViewModel();

        public UserAppBarViewModel UserAppBarVM { get; set; } = new UserAppBarViewModel();

        public ICommand AddPostCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public EntryListPageViewModel()
        {
            AddPostCommand = new RelayCommand(OnAddPost);
            RefreshCommand = new RelayCommand(OnRefresh);

            _ = LoadEntryList();
        }

        public async Task LoadEntryList()
        {
            EntryListVM.List?.Clear();

            Busy = true;

            var entryList = await EntryService.Instance.Newest();

            EntryListVM.LoadData(entryList);

            Busy = false;
        }

        void OnAddPost()
        {
            PageNavigator.Instance.Push<AddPageViewModel>(async m =>
            {
                if (!m.Cancelled)
                {
                    await EntryService.Instance.Add(new AddEntry
                    {
                        Text = m.Text
                    });

                    await LoadEntryList();
                }
            });
        }

        async void OnRefresh()
        {
            await LoadEntryList();
        }
    }
}
