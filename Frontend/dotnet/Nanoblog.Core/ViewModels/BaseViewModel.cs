using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nanoblog.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _busy = false;

        public bool Busy
        {
            get => _busy;
            set => Update(ref _busy, value);
        }

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
