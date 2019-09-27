using Nanoblog.AppCore.Navigation;
using Nanoblog.ApiService;
using Nanoblog.AppCore.ViewModels.Pages;
using Refit;
using System.Windows.Input;

namespace Nanoblog.AppCore.ViewModels.Controls.AppBar
{
    public class UserAppBarViewModel : BaseViewModel
    {
        private string _navBarMessage;

        public ICommand LogOutCommand { get; set; }

        public string NavBarMessage
        {
            get => _navBarMessage;
            set => Update(ref _navBarMessage, value);
        }

        public UserAppBarViewModel()
        {
            NavBarMessage = $"Logged as {App.CurrentUser.UserName}";

            LogOutCommand = new RelayCommand(OnLogOut);
        }

        async void OnLogOut()
        {
            try
            {
                await JwtService.Instance.ResetAsync();
            }
            catch (ApiException)
            {

            }

            App.CurrentUser = null;

            PageNavigator.Instance.Navigate<LoginPageViewModel>();
        }
    }
}
