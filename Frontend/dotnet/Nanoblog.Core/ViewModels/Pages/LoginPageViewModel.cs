using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }

        IViewNavigator _viewNavigator;

        public LoginPageViewModel(IViewNavigator viewNavigator)
        {
            LoginCommand = new Command(Login);
            _viewNavigator = viewNavigator;
        }

        void Login(object obj)
        {
            _viewNavigator.NavigateToEntryListPage();
        }
    }
}
