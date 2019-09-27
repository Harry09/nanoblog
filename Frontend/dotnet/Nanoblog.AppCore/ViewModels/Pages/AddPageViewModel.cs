using Nanoblog.AppCore.Navigation;
using System.Windows.Input;

namespace Nanoblog.AppCore.ViewModels.Pages
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

        void OnAdd()
        {
            PageNavigator.Instance.Pop();
        }

        void OnCancel()
        {
            Cancelled = true;
            PageNavigator.Instance.Pop();
        }
    }
}