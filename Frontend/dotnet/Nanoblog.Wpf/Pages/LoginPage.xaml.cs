using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels;
using Nanoblog.Core.ViewModels.Pages;
using Nanoblog.Wpf.Controls.AppBar;
using System.Security;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nanoblog.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : AppBarPage, INavigable<LoginPageViewModel>, IHavePassword
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public SecureString Password => PasswordBox.SecurePassword;

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = DataContext as LoginPageViewModel;

                vm.LoginCommand.Execute(this);
            }
        }
    }
}
