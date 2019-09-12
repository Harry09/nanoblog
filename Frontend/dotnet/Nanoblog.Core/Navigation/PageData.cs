using Nanoblog.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Navigation
{
    public class PageData
    {
        public object Page { get; set; }

        public BaseViewModel ViewModel { get; set; }
    }
}
