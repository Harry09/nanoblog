using Nanoblog.AppCore.Navigation;
using Nanoblog.AppCore.ViewModels.Pages;
using Nanoblog.Wpf.Controls.AppBar;

namespace Nanoblog.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for EntryListPage.xaml
    /// </summary>
    public partial class EntryListPage : AppBarPage, INavigable<EntryListPageViewModel>
    {
        public EntryListPage()
        {
            InitializeComponent();
        }
    }
}
