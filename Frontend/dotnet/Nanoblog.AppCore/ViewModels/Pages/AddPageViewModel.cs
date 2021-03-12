using Nanoblog.AppCore.Navigation;
using System;
using System.Windows.Input;

namespace Nanoblog.AppCore.ViewModels.Pages
{
    /// <summary>
    /// General purpose page with textbox and two buttons
    /// </summary>
    public class AddPageViewModel : BaseViewModel
    {
        private string _text;

        /// <summary>
        /// Content of textbox
        /// </summary>
        public string Text
        {
            get => _text;
            set => Update(ref _text, value);
        }

        /// <summary>
        /// Occurs when client clicks the Add button
        /// If not set, default action is PageNavigator.Instance.Pop();
        /// </summary>
        public Action<AddPageViewModel> OnAdd { get; set; }

        /// <summary>
        /// Occurs when client clicks the Cancel button
        /// If not set, default action is PageNavigator.Instance.Pop();
        /// </summary>
        public Action<AddPageViewModel> OnCancel { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public AddPageViewModel()
        {
            AddCommand = new RelayCommand(DefaultOnAdd);
            CancelCommand = new RelayCommand(DefaultOnCancel);
        }

        void DefaultOnAdd()
        {
            if (OnAdd != null)
            {
                OnAdd(this);
            }
            else
            {
                PageNavigator.Instance.Pop();
            }
        }

        void DefaultOnCancel()
        {
            if (OnCancel != null)
            {
                OnCancel(this);
            }
            else
            {
                PageNavigator.Instance.Pop();
            }
        }
    }
}