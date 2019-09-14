using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.ViewModels.Controls.CommentList.Design
{
    public class CommentListDesignModel : CommentListViewModel
    {
        public static CommentListDesignModel Instance => new CommentListDesignModel();

        public CommentListDesignModel()
        {
            List = new ObservableCollection<CommentListItemViewModel>
            {
                new CommentListItemDesignModel
                {
                    UserName = "Obi",
                    Date = "Today",
                    Text = "Hello there"
                },
                new CommentListItemDesignModel()
                {
                    UserName = "User",
                    Date = "12 hours ago",
                    Text = "What a nice application"
                }
            };
        }
    }
}
