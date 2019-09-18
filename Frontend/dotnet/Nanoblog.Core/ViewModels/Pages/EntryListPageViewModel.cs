using Nanoblog.Core.ViewModels.Controls.EntryList;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nanoblog.Core.Navigation;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class EntryListPageViewModel : BaseViewModel
    {
        public EntryListViewModel EntryListVM { get; set; } = new EntryListViewModel();

        public ICommand AddPostCommand { get; set; }

        public EntryListPageViewModel()
        {
            AddPostCommand = new RelayCommand(OnAddPost);

            LoadData();
        }

        public void LoadData()
        {
            EntryListVM.LoadData(new EntryListItemViewModel[]
            {
                new EntryListItemViewModel
                {
                    UserName = "Obi",
                    Date = "Today",
                    Text = "Hello there",
                    CommentsCount = 12
                },
                new EntryListItemViewModel
                {
                    UserName = "User",
                    Date = "12 hours ago",
                    Text = "What a nice application",
                    CommentsCount = 54
                },
                new EntryListItemViewModel
                {
                    UserName = "Obi",
                    Date = "Today",
                    Text = "Hello there",
                    CommentsCount = 321
                },
                new EntryListItemViewModel
                {
                    UserName = "User",
                    Date = "12 hours ago",
                    Text = "What a nice application",
                    CommentsCount = 2
                }
            });
        }
    
        void OnAddPost(object _)
        {
            PageNavigator.Instance.Push<AddPageViewModel>(m => {
                if (!m.Cancelled)
                {
                    EntryListVM.List.Add(new EntryListItemViewModel {
                        UserName = "Harry2",
                        Date = DateTime.Now.ToString(),
                        Text = m.Text,
                        CommentsCount = 32
                    });
                }
            });
        }
    }
}
