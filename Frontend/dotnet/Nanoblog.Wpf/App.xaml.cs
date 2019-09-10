using Autofac;
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
        ViewNavigator _viewNavigator;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO: Replace with Autofac's Container
            _mainWindow = new MainWindow();
            _viewNavigator = new ViewNavigator();
            _viewNavigator.Initialize(_mainWindow);

            _app = new Core.App(_mainWindow, _viewNavigator);

            _mainWindow.ShowDialog();
        }
    }
}
