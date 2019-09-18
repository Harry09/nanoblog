using Nanoblog.Common.Data.Dto;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core
{
    public static class App
    {
        static public UserDto CurrentUser { get; set; }

        static public void Init(IPageNavigator pageNavigator)
        {
            pageNavigator.Navigate<LoginPageViewModel>();
        }
    }
}
