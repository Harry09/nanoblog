using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.ViewModels.Controls.CommentList.Design
{
    public class CommentListItemDesignModel : CommentListItemViewModel
    {
        public static CommentListItemDesignModel Instance => new CommentListItemDesignModel();

        public CommentListItemDesignModel()
        {
            UserName = "User";
            Date = "12-07-2019 16:53:10";
            Text = "What a nice application you have here";
        }
    }
}
