using Autofac;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;
using Nanoblog.Wpf.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Nanoblog.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Core.App _app;
        MainWindow _mainWindow;
        IPageNavigator _pageNavigator;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO: Replace with Autofac's Container
            _mainWindow = new MainWindow();
            _pageNavigator = new PageNavigator(_mainWindow);

            _pageNavigator.Register<LoginPage, LoginPageViewModel>();
            _pageNavigator.Register<EntryListPage, EntryListPageViewModel>();

            _app = new Core.App(_mainWindow, _pageNavigator);

            _mainWindow.ShowDialog();
        }
    }
}
