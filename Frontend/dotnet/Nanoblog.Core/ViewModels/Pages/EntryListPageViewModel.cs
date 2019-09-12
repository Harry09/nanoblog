using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class EntryListPageViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => Update(ref _name, value);
        }

        public ICommand BackCommand { get; }

        public EntryListPageViewModel(int name)
        {
            Name = name.ToString();

            BackCommand = new Command((_) => PageNavigator.Pop());
        }
    }
}
