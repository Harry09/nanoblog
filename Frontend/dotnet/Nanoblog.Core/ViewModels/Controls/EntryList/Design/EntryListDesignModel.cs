using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.ViewModels.Controls.EntryList.Design
{
    public class EntryListDesignModel : EntryListViewModel
    {
        public static EntryListDesignModel Instance => new EntryListDesignModel();

        public EntryListDesignModel()
        {
            List = new ObservableCollection<EntryListItemViewModel>
            {
                new EntryListItemDesignModel
                {
                    UserName = "Obi",
                    Date = "Today",
                    Text = "Hello there"
                },
                new EntryListItemDesignModel()
                {
                    UserName = "User",
                    Date = "12 hours ago",
                    Text = "What a nice application"
                }
            };
        }
    }
}
