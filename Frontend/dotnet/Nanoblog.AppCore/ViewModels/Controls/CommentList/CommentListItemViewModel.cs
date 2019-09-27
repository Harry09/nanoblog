using Nanoblog.Common;
using Nanoblog.Common.Dto;
using Nanoblog.ApiService;

namespace Nanoblog.AppCore.ViewModels.Controls.CommentList
{
    public class CommentListItemViewModel : ItemListItemViewModel
    {
        private CommentDto _commentData;


        public CommentListItemViewModel(CommentDto commentDto)
        {
            DeleteCommand = new RelayCommand(OnDelete);
            ChangeVoteCommand = new RelayCommand<KarmaValue>(OnChangeVote);

            _commentData = commentDto;

            UserName = _commentData.Author.UserName;
            Date = _commentData.CreateTime.ToString();
            Text = _commentData.Text;
            KarmaCount = _commentData.KarmaCount;
            UserVote = _commentData.UserVote;

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

        async void OnChangeVote(KarmaValue karmaValue)
        {
            await OnChangeVote(KarmaService.VoteFor.Comment, karmaValue, _commentData.Id);
        }
    }
}
