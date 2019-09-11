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


        public LoginPageViewModel()
        {
            LoginCommand = new Command(Login);
        }

        void Login(object obj)
        {
            PageNavigator.Navigate<EntryListPageViewModel>();
        }
    }
}
