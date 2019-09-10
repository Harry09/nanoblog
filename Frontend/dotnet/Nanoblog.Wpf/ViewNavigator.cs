using Nanoblog.Core;
using Nanoblog.Core.ViewModels;
using Nanoblog.Core.ViewModels.Pages;
using Nanoblog.Wpf.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Nanoblog.Wpf
{
    public class ViewNavigator : IViewNavigator
    {
        IMainWindow _mainWindow;

        public void Initialize(IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void NavigateToLoginPage()
        {
            _mainWindow.SetPageData(new LoginPage(), new LoginPageViewModel(this));
        }

        public void NavigateToEntryListPage()
        {
            _mainWindow.SetPageData(new EntryListPage(), new EntryListPageViewModel());
        }

        public void NavigateToEntryPage() => throw new NotImplementedException();
    }
}
