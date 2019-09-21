using Nanoblog.Core.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nanoblog.Core.ViewModels.Controls.CommentList
{
    public class CommentListViewModel : BaseViewModel
    {

        private ObservableCollection<CommentListItemViewModel> _list;

        public ObservableCollection<CommentListItemViewModel> List
        {
            get => _list;
            set => Update(ref _list, value);
        }

        public CommentListViewModel()
        {

        }

        public void LoadData(IEnumerable<CommentListItemViewModel> data)
        {
            List = data.ToObservable();
        }
    }
}
