using Nanoblog.Common.Data.Commands.Account;
using Nanoblog.Common.Data.Dto;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.Services;
using Refit;
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
        private string _formEmail;
        private string _formPassword;
        private string _errorMessage;

        public ICommand LoginCommand { get; }

        public string FormEmail
        {
            get => _formEmail;
            set => Update(ref _formEmail, value);
        }

        public string FormPassword
        {
            get => _formPassword;
            set => Update(ref _formPassword, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Update(ref _errorMessage, value);
        }

        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin);
        }

        async void OnLogin(object obj)
        {
            Busy = true;

            try
            {
                var jwt = await AccountService.Instance.Login(new LoginUser { Email = FormEmail, Password = FormPassword });
                JwtService.Instance.SetJwt(jwt);

                var user = await AccountService.Instance.GetUserByEmail(FormEmail);

                if (user != null)
                {
                    App.CurrentUser = user;
                }

                PageNavigator.Instance.Navigate<EntryListPageViewModel>();
            }
            catch (ApiException ex)
            {
                var data = await ex.GetContentAsAsync<ErrorDto>();

                ErrorMessage = data.Message;
            }

            Busy = false;
        }
    }
}
