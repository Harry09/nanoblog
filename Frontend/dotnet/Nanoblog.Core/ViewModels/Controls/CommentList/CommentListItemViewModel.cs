namespace Nanoblog.Core.ViewModels.Controls.CommentList
{
    public class CommentListItemViewModel : BaseViewModel
    {
        private string _userName;
        private string _date;
        private string _text;

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
    }
}
