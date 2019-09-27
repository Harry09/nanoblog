using Nanoblog.AppCore;
using Nanoblog.AppCore.Navigation;
using System.Windows;

namespace Nanoblog.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetPageData(PageData pageData)
        {
            PageHost.Content = pageData.Page;
            PageHost.DataContext = pageData.ViewModel;
        }
    }
}
