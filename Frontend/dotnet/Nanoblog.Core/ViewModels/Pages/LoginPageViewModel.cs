using Nanoblog.Core.Navigation;
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
        public ICommand LoginCommand { get; }


        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        void Login(object obj)
        {
            PageNavigator.Instance.Push<EntryListPageViewModel, int>(1234);
        }
    }
}
