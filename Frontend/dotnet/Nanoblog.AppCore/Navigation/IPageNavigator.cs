namespace Nanoblog.AppCore.Navigation
{
    public interface IPageNavigator
    {
        void Register<TPage, TPageViewModel>();


        void Navigate<TPageViewModel>();

        void Navigate<TPageViewModel, TParameter>(TParameter parameter);


        void Push<TPageViewModel>();

        void Push<TPageViewModel, TParameter>(TParameter parameter);

        void Pop();
    }
}
