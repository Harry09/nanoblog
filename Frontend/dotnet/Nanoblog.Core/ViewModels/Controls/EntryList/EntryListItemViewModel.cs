using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels.Controls.EntryList
{
    public class EntryListItemViewModel : BaseViewModel
    {
        private string _userName;
        private string _date;
        private string _text;
        private int _commentsCount;

        public string UserName
        {
            get => _userName;
            set => Update(ref _userName, value);
        }

        public string Date
        {
            get => _date;
            set => Update(ref _date, value);
        }

        public string Text
        {
            get => _text;
            set => Update(ref _text, value);
        }

        public int CommentsCount
        {
            get => _commentsCount;
            set => Update(ref _commentsCount, value);
        }

        public ICommand ShowCommentsCommand { get; set; }

        public EntryListItemViewModel()
        {
            ShowCommentsCommand = new RelayCommand(OnShowComments);
        }

        void OnShowComments(object _)
        {
            PageNavigator.Instance.Push<EntryDetailPageViewModel, EntryListItemViewModel>(this);
        }
    }
}
