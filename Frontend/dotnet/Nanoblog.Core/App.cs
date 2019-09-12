using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core
{
    public class App
    {
        IMainWindow _mainWindow;

        public IPageNavigator PageNavigator { get; set; }

        public App(IMainWindow mainWindow, IPageNavigator pageNavigator)
        {
            _mainWindow = mainWindow;

            PageNavigator = pageNavigator;

            PageNavigator.Navigate<LoginPageViewModel>();
        }
    }
}
