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

        public IPageNavigator ViewNavigator { get; set; }

        public App(IMainWindow mainWindow, IPageNavigator viewNavigator)
        {
            _mainWindow = mainWindow;

            ViewNavigator = viewNavigator;


            ViewNavigator.Navigate<LoginPageViewModel>();
            //ViewNavigator = new ViewNavigator()
        }
    }
}
