using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.ViewModels.Controls.EntryList.Design
{
    public class EntryListItemDesignModel : EntryListItemViewModel
    {
        public static EntryListItemDesignModel Instance => new EntryListItemDesignModel();

        public EntryListItemDesignModel()
        {
            UserName = "User";
            Date = "12-07-2019 16:53:10";
            Text = "What a nice application you have here";
        }
    }
}
