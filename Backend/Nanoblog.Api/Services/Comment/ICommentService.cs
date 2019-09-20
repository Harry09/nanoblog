using Nanoblog.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nanoblog.Api.Services
{
    public interface ICommentService
    {
        Task<CommentDto> GetAsync(string id);

        Task<CommentDto> GetWithKarmaActionAsync(string id, string userId);

        Task<IEnumerable<CommentDto>> GetCommentsAsync(string entryId);

        Task<IEnumerable<CommentDto>> GetCommentsWithKarmaActionAsync(string entryId, string userId);

        Task<CommentDto> AddAsync(string text, string authorId, string entryId);

        Task RemoveAsync(string id);

        Task UpdateAsync(string id, string text);
    }
}
