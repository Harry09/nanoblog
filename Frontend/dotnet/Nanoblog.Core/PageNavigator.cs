using Nanoblog.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core
{
    public class PageNavigator : IPageNavigator
    {
        Dictionary<Type, Type> _types = new Dictionary<Type, Type>();

        IMainWindow _mainWindow;

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
            if (_types.TryGetValue(typeof(TPageViewModel), out Type pageType))
            {
                var page = Activator.CreateInstance(pageType);
                var pageViewModel = (BaseViewModel)Activator.CreateInstance(typeof(TPageViewModel));
                pageViewModel.SetPageNavigator(this);

                _mainWindow.SetPageData(page, pageViewModel);
            }
        }

        public void Navigate<TPageViewModel, TParameter>(TParameter parameter)
        {
            if (_types.TryGetValue(typeof(TPageViewModel), out Type pageType))
            {
                var page = Activator.CreateInstance(pageType);
                var pageViewModel = (BaseViewModel)Activator.CreateInstance(typeof(TPageViewModel), parameter);
                pageViewModel.SetPageNavigator(this);

                _mainWindow.SetPageData(page, pageViewModel);
            }
        }

        public void Push<TPageViewModel>()
        {
            throw new NotImplementedException();
        }

        public void Push<TPageViewModel, TParameter>(TParameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}
