using Nanoblog.Common.Dto;
using Nanoblog.Core.Services;
using System.Windows.Input;

namespace Nanoblog.Core.ViewModels.Controls.CommentList
{
    public class CommentListItemViewModel : BaseViewModel
    {
        private CommentDto _commentData;

        private string _userName;
        private string _date;
        private string _text;
        private bool _isDeletable;
        private bool _deleted;

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

        public bool IsDeletable
        {
            get => _isDeletable;
            set => Update(ref _isDeletable, value);
        }

        public bool Deleted
        {
            get => _deleted;
            set => Update(ref _deleted, value);
        }

        public ICommand DeleteCommand { get; set; }

        public CommentListItemViewModel(CommentDto commentDto)
        {
            DeleteCommand = new RelayCommand(OnDelete);

            _commentData = commentDto;

            _userName = _commentData.Author.UserName;
            _date = _commentData.CreateTime.ToString();
            _text = _commentData.Text;

            IsDeletable = commentDto.Author.Id == App.CurrentUser.Id;
        }

        async void OnDelete()
        {
            if (IsDeletable)
            {
                await CommentService.Instance.Delete(_commentData.Id);

                Deleted = true;
            }
        }
    }
}
