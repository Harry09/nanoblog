using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public RelayCommand(Action<object> execute)
        {
            _canExecute = (_) => true;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) == true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
