using Nanoblog.Common.Commands.Comment;
using Nanoblog.Common.Dto;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nanoblog.ApiService.Api
{
    public interface ICommentAPI
    {
        [Get("/{id}")]
        [Headers("Authorization: Bearer")]
        Task<CommentDto> Get(string id);

        [Get("/entry/{entryId}")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<CommentDto>> GetComments(string entryId);

        [Post("")]
        [Headers("Authorization: Bearer")]
        Task<CommentDto> Add([Body] AddComment comment);

        [Delete("/{id}")]
        [Headers("Authorization: Bearer")]
        Task Delete(string id);
    }
}
