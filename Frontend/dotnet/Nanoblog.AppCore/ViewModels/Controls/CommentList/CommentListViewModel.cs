using Nanoblog.Common.Dto;
using Nanoblog.AppCore.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nanoblog.AppCore.ViewModels.Controls.CommentList
{
    public class CommentListViewModel : BaseViewModel
    {

        private ObservableCollection<CommentListItemViewModel> _list;

        public ObservableCollection<CommentListItemViewModel> List
        {
            get => _list;
            private set => Update(ref _list, value);
        }

        public CommentListViewModel()
        {
        }

        public void LoadData(IEnumerable<CommentDto> data)
        {
            List = data.Select(x => new CommentListItemViewModel(x)).ToObservable();
        }
    }
}
