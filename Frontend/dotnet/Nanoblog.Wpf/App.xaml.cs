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
        MainWindow _mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO: Replace with Autofac's Container
            _mainWindow = new MainWindow();

            var pageNavigator = PageNavigator.Instance;
            pageNavigator.SetMainWindow(_mainWindow);

            pageNavigator.Register<LoginPage, LoginPageViewModel>();
            pageNavigator.Register<EntryListPage, EntryListPageViewModel>();
            pageNavigator.Register<EntryDetailPage, EntryDetailPageViewModel>();
            pageNavigator.Register<AddPage, AddPageViewModel>();

            Core.App.Init(pageNavigator);

            _mainWindow.ShowDialog();
        }
    }
}
