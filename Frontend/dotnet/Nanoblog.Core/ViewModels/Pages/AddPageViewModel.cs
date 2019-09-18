
using System.Windows.Input;
using Nanoblog.Core.Navigation;

namespace Nanoblog.Core.ViewModels.Pages
{
    public class AddPageViewModel : BaseViewModel
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => Update(ref _text, value);
        }

        public bool Cancelled { get; set; } = false;

        public ICommand AddCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public AddPageViewModel()
        {
            AddCommand = new RelayCommand(OnAdd);
            CancelCommand = new RelayCommand(OnCancel);
        }

        void OnAdd(object _)
        {
            PageNavigator.Instance.Pop();
        }

        void OnCancel(object _)
        {
            Cancelled = true;
            PageNavigator.Instance.Pop();
        }
    }
}