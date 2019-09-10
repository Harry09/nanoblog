using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core
{
    public interface IViewNavigator
    {
        void Initialize(IMainWindow mainWindow);

        void NavigateToLoginPage();

        void NavigateToEntryListPage();

        // TODO: pass entry's data
        void NavigateToEntryPage();
    }
}
