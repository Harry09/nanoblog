using Nanoblog.Common.Dto;
using Nanoblog.AppCore.Navigation;
using Nanoblog.AppCore.ViewModels.Pages;

namespace Nanoblog.AppCore
{
    public static class App
    {
        static public UserDto CurrentUser { get; set; }

        static public void Init(IPageNavigator pageNavigator)
        {
            pageNavigator.Navigate<LoginPageViewModel>();
        }
    }
}
