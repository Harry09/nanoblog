using Nanoblog.Core.ViewModels.Controls.EntryList;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.Services;
using Nanoblog.Core.Extensions;
using Nanoblog.Common.Commands.Entry;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class EntryListPageViewModel : BaseViewModel
    {
        private string _navBarMessage;

        public EntryListViewModel EntryListVM { get; set; } = new EntryListViewModel();

        public ICommand AddPostCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public ICommand LogOutCommand { get; set; }

        public string NavBarMessage
        {
            get => _navBarMessage;
            set => Update(ref _navBarMessage, value);
        }

        public EntryListPageViewModel()
        {
            AddPostCommand = new RelayCommand(OnAddPost);
            RefreshCommand = new RelayCommand(OnRefresh);
            LogOutCommand = new RelayCommand(OnLogOut);

            NavBarMessage = $"Logged as {App.CurrentUser.UserName}";

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

        void OnAddPost(object _)
        {
            PageNavigator.Instance.Push<AddPageViewModel>(async m => {
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

        async void OnRefresh(object _)
        {
            await LoadEntryList();
        }

        void OnLogOut(object _)
        {
            JwtService.Instance.Reset();
            App.CurrentUser = null;

            PageNavigator.Instance.Navigate<LoginPageViewModel>();
        }
    }
}
