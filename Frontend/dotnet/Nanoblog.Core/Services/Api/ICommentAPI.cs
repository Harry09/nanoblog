using Nanoblog.Common.Commands.Comment;
using Nanoblog.Common.Dto;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Services.Api
{
    public interface ICommentAPI
    {
        [Get("/{id}")]
        Task<CommentDto> Get(string id);

        [Get("/entry/{entryId}")]
        Task<IEnumerable<CommentDto>> GetComments(string entryId);

        [Post("")]
        [Headers("Authorization: Bearer")]
        Task<CommentDto> Add([Body] AddComment comment);

        [Delete("/{id}")]
        [Headers("Authorization: Bearer")]
        Task Delete(string id);
    }
}
