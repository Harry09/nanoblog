using Nanoblog.Core.ViewModels.Controls.EntryList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class EntryListPageViewModel : BaseViewModel
    {
        //private EntryListViewModel entryListVM;

        //public EntryListViewModel EntryListViewModel
        //{
        //    get => entryListVM;
        //    set => Update(ref entryListVM, value);
        //}

        public EntryListViewModel EntryListVM { get; set; } = new EntryListViewModel();

        public EntryListPageViewModel()
        {
            LoadList();
        }

        public void LoadList()
        {
            EntryListVM.List = new ObservableCollection<EntryListItemViewModel>
            {
                new EntryListItemViewModel
                {
                    UserName = "Obi",
                    Date = "Today",
                    Text = "Hello there"
                },
                new EntryListItemViewModel
                {
                    UserName = "User",
                    Date = "12 hours ago",
                    Text = "What a nice application"
                },
                new EntryListItemViewModel
                {
                    UserName = "Obi",
                    Date = "Today",
                    Text = "Hello there"
                },
                new EntryListItemViewModel
                {
                    UserName = "User",
                    Date = "12 hours ago",
                    Text = "What a nice application"
                }
            };
        }
    }
}
