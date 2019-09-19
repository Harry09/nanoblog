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
using Nanoblog.Common.Data.Commands.Entry;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class EntryListPageViewModel : BaseViewModel
    {
        private string _navBarMessage;

        public EntryListViewModel EntryListVM { get; set; } = new EntryListViewModel();

        public ICommand AddPostCommand { get; set; }

        public ICommand LogOutCommand { get; set; }

        public string NavBarMessage
        {
            get => _navBarMessage;
            set => Update(ref _navBarMessage, value);
        }

        public EntryListPageViewModel()
        {
            AddPostCommand = new RelayCommand(OnAddPost);
            LogOutCommand = new RelayCommand(OnLogOut);

            NavBarMessage = $"Logged as {App.CurrentUser.UserName}";

            LoadEntryList();
        }

        public async Task LoadEntryList()
        {
            Busy = true;

            var entryList = await EntryService.Instance.Newest();
            EntryListVM.List = entryList.Select(m =>
            {
                return new EntryListItemViewModel
                {
                    UserName = m.Author.UserName,
                    Date = m.CreateTime.ToString(),
                    CommentsCount = m.CommentsCount,
                    Text = m.Text
                };
            }).ToObservable();

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

        void OnLogOut(object _)
        {
            JwtService.Instance.Reset();
            App.CurrentUser = null;

            PageNavigator.Instance.Navigate<LoginPageViewModel>();
        }
    }
}
