using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning disable CS0067

        public RelayCommand(Func<T, bool> canExecute, Action<T> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public RelayCommand(Action<T> execute)
        {
            _canExecute = (_) => true;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) == true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke((T)parameter);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action _execute;

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning disable CS0067

        public RelayCommand(Action execute)
        {
            _canExecute = (_) => true;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }
}
