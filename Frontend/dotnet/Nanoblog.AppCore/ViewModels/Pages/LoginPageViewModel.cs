using Nanoblog.Common.Commands.Account;
using Nanoblog.Common.Dto;
using Nanoblog.AppCore.Navigation;
using Nanoblog.ApiService;
using Nanoblog.AppCore.Extensions;
using Refit;
using System;
using System.Windows.Input;
using System.Net;

namespace Nanoblog.AppCore.ViewModels.Pages
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string _formEmail = "";
        private string _errorMessage;

        public ICommand LoginCommand { get; }

        public string FormEmail
        {
            get => _formEmail;
            set => Update(ref _formEmail, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Update(ref _errorMessage, value);
        }

        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand<IHavePassword>(OnLogin);
        }

        async void OnLogin(IHavePassword havePassword)
        {
            if (FormEmail.Length == 0)
            {
                ErrorMessage = "Enter the email";
                return;
            }

            string password = havePassword.Password.Unsecure();

            if (password.Length == 0)
            {
                ErrorMessage = "Enter the password";
                return;
            }

            Busy = true;

            try
            {
                var jwt = await AccountService.Instance.Login(new LoginUser { Email = FormEmail, Password = password });
                JwtService.Instance.SetJwt(jwt);

                var user = await AccountService.Instance.GetUserByEmail(FormEmail);

                if (user is { })
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
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            Busy = false;
        }
    }
}
