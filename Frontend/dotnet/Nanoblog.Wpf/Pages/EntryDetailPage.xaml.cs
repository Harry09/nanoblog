using Nanoblog.AppCore.Navigation;
using Nanoblog.AppCore.ViewModels.Pages;
using Nanoblog.Wpf.Controls.AppBar;

namespace Nanoblog.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for EntryDetailPage.xaml
    /// </summary>
    public partial class EntryDetailPage : AppBarPage, INavigable<EntryDetailPageViewModel>
    {
        public EntryDetailPage()
        {
            InitializeComponent();
        }
    }
}
