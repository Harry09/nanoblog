using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;
using Nanoblog.Wpf.Controls.AppBar;

namespace Nanoblog.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddPage : AppBarPage, INavigable<AddPageViewModel>
    {
        public AddPage()
        {
            InitializeComponent();
        }
    }
}
