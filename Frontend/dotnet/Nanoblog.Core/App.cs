using Nanoblog.Common.Dto;
using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;

namespace Nanoblog.Core
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
