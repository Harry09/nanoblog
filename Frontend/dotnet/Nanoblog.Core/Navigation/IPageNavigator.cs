using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Navigation
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
