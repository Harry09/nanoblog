using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Navigation
{
    public class PageNavigator : IPageNavigator
    {
        Dictionary<Type, Type> _types = new Dictionary<Type, Type>();

        IMainWindow _mainWindow;

        Stack<PageData> _pageStack = new Stack<PageData>();

        public PageData CurrentPage { get; set; }

        public PageNavigator(IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void Register<TPage, TPageViewModel>()
        {
            _types.Add(typeof(TPageViewModel), typeof(TPage));
        }

        public void Navigate<TPageViewModel>()
        {
            if (TryCreatePage(typeof(TPageViewModel), out PageData output))
            {
                CurrentPage = output;
                _mainWindow.SetPageData(output.Page, output.ViewModel);
            }
        }

        public void Navigate<TPageViewModel, TParameter>(TParameter parameter)
        {
            if (TryCreatePage(typeof(TPageViewModel), parameter, out PageData output))
            {
                CurrentPage = output;
                _mainWindow.SetPageData(output.Page, output.ViewModel);
            }
        }

        public void Push<TPageViewModel>()
        {
            if (TryCreatePage(typeof(TPageViewModel), out PageData output))
            {
                _pageStack.Push(CurrentPage);
                CurrentPage = output;
                _mainWindow.SetPageData(output.Page, output.ViewModel);
            }
        }

        public void Push<TPageViewModel, TParameter>(TParameter parameter)
        {
            if (TryCreatePage(typeof(TPageViewModel), parameter, out PageData output))
            {
                _pageStack.Push(CurrentPage);
                CurrentPage = output;
                _mainWindow.SetPageData(output.Page, output.ViewModel);
            }
        }

        public void Pop()
        {
            if (_pageStack.Count > 0)
            {
                var pageData = _pageStack.Pop();

                CurrentPage = pageData;
                _mainWindow.SetPageData(pageData.Page, pageData.ViewModel);
            }
        }

        bool TryCreatePage(Type viewModelType, out PageData output)
        {
            if (_types.TryGetValue(viewModelType, out Type pageType))
            {
                var page = Activator.CreateInstance(pageType);
                var pageViewModel = (BaseViewModel)Activator.CreateInstance(viewModelType);
                pageViewModel.SetPageNavigator(this);

                output = new PageData { Page = page, ViewModel = pageViewModel };
                return true;
            }

            output = null;
            return false;
        }

        bool TryCreatePage<TParameter>(Type viewModelType, TParameter parameter, out PageData output)
        {
            if (_types.TryGetValue(viewModelType, out Type pageType))
            {
                var page = Activator.CreateInstance(pageType);
                var pageViewModel = (BaseViewModel)Activator.CreateInstance(viewModelType, parameter);
                pageViewModel.SetPageNavigator(this);

                output = new PageData { Page = page, ViewModel = pageViewModel };
                return true;
            }

            output = null;
            return false;
        }
    }
}
