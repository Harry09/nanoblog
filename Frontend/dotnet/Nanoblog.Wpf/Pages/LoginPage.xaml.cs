using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;
using Nanoblog.Wpf.Controls.AppBar;

namespace Nanoblog.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : AppBarPage, INavigable<LoginPageViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
