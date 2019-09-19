using Nanoblog.Core.ViewModels;
using System;
using System.Collections.Generic;

namespace Nanoblog.Core.Navigation
{
    public class PageNavigator : IPageNavigator
    {
        static public PageNavigator Instance { get; set; } = new PageNavigator();

        Dictionary<Type, Type> _types = new Dictionary<Type, Type>();

        IMainWindow _mainWindow;

        Stack<PageData> _pageStack = new Stack<PageData>();

        Dictionary<int, Action<object>> _popAction = new Dictionary<int, Action<object>>();

        public PageData CurrentPage { get; set; }

        public void SetMainWindow(IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void Register<TPage, TPageViewModel>()
        {
            _types.Add(typeof(TPageViewModel), typeof(TPage));
        }

        public void Navigate<TPageViewModel>()
        {
            _pageStack.Clear();

            var pageData = CreatePageData(typeof(TPageViewModel));

            SetPageData(pageData);
        }

        public void Navigate<TPageViewModel, TParameter>(TParameter parameter)
        {
            _pageStack.Clear();

            var pageData = CreatePageData(typeof(TPageViewModel), parameter);

            SetPageData(pageData);
        }

        public void Push<TPageViewModel>()
        {
            var pageData = CreatePageData(typeof(TPageViewModel));

            SetPageData(pageData);
        }

        public void Push<TPageViewModel, TParameter>(TParameter parameter)
        {
            var pageData = CreatePageData(typeof(TPageViewModel), parameter);

            SetPageData(pageData);
        }

        public void Push<TPageViewModel>(Action<TPageViewModel> popAction)
        {
            _popAction.Add(_pageStack.Count, (object m) => popAction((TPageViewModel)m));

            Push<TPageViewModel>();
        }

        public void Push<TPageViewModel, TParameter>(TParameter parameter, Action<TPageViewModel> popAction)
        {
            _popAction.Add(_pageStack.Count, (object m) => popAction((TPageViewModel)m));

            Push<TPageViewModel, TParameter>(parameter);
        }

        public void Pop()
        {
            if (_pageStack.Count > 1)
            {
                var popPageData = _pageStack.Pop();

                if (_popAction.TryGetValue(_pageStack.Count, out var action))
                {
                    action?.Invoke(popPageData.ViewModel);
                    _popAction.Remove(_pageStack.Count);
                }

                var pageData = _pageStack.Peek();
                
                CurrentPage = pageData;
                _mainWindow.SetPageData(pageData);
            }
        }

        void SetPageData(PageData pageData)
        {
            _pageStack.Push(pageData);

            CurrentPage = pageData;
            _mainWindow.SetPageData(pageData);
        }

        PageData CreatePageData(Type viewModelType)
        {
            Type pageType = _types[viewModelType];

            var page = Activator.CreateInstance(pageType);
            var pageViewModel = (BaseViewModel)Activator.CreateInstance(viewModelType);

            return new PageData { Page = page, ViewModel = pageViewModel };
        }

        PageData CreatePageData<TParameter>(Type viewModelType, TParameter parameter)
        {
            Type pageType = _types[viewModelType];

            var page = Activator.CreateInstance(pageType);
            var pageViewModel = (BaseViewModel)Activator.CreateInstance(viewModelType, parameter);

            return new PageData { Page = page, ViewModel = pageViewModel };
        }
    }
}
