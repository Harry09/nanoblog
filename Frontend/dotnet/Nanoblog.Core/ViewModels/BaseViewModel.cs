using Nanoblog.Core.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName] string propertyName = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update<T>(ref T variable, T value, [CallerMemberName] string propertyName = default)
        {
            if (!Equals(variable, value))
            {
                variable = value;
                Notify(propertyName);
            }
        }
    }
}
