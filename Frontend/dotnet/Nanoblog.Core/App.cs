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

        public IViewNavigator ViewNavigator { get; set; }

        public App(IMainWindow mainWindow, IViewNavigator viewNavigator)
        {
            _mainWindow = mainWindow;

            ViewNavigator = viewNavigator;


            ViewNavigator.NavigateToLoginPage();
            //ViewNavigator = new ViewNavigator()
        }
    }
}
