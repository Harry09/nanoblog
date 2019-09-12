using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.ViewModels.Controls.EntryList
{
    public class EntryListViewModel : BaseViewModel
    {

        private ObservableCollection<EntryListItemViewModel> _list;

        public ObservableCollection<EntryListItemViewModel> List
        {
            get => _list;
            set => Update(ref _list, value);
        }

    }
}
