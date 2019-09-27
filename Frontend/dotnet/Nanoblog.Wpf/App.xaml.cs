using Autofac;
using Nanoblog.AppCore.Navigation;
using Nanoblog.Wpf.Controls.AppBar;
using System.Data;
using System.Linq;
using System.Reflection;
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

            var types = Assembly.GetAssembly(typeof(AppBarPage))
                .GetTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface)
                .Select(
                    x => (
                            x,
                            x.GetInterfaces().FirstOrDefault(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(INavigable<>))
                         )
                )
                .Where(x => x.Item2 != null);

            foreach ((var view, var navigable) in types)
            {
                var viewModel = navigable.GenericTypeArguments.First();

                pageNavigator.Register(view, viewModel);
            }

            AppCore.App.Init(pageNavigator);

            _mainWindow.ShowDialog();
        }
    }
}
