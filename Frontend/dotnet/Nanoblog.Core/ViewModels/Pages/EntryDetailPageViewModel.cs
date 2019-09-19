using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Controls.CommentList;
using Nanoblog.Core.ViewModels.Controls.EntryList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public CommentListViewModel Comments
        {
            get => _comments;
            set => Update(ref _comments, value);
        }

        public ICommand BackCommand { get; set; }

        public EntryDetailPageViewModel(EntryListItemViewModel entry)
        {
            Entry = entry;
            entry.InsideDetail = true;

            BackCommand = new RelayCommand(OnBack);

            // TODO: Load comments using Entry.Id
        }

        void OnBack(object _)
        {
            _entry.InsideDetail = false;
            PageNavigator.Instance.Pop();
        }
    }
}
