namespace Nanoblog.AppCore.Navigation
{
    public interface IPageNavigator
    {
        void Register<TPage, TPageViewModel>();


        void Navigate<TPageViewModel>();

        void Navigate<TPageViewModel>(TPageViewModel viewModel);


        void Push<TPageViewModel>();

        void Push<TPageViewModel>(TPageViewModel viewModel);

        void Pop();
    }
}
