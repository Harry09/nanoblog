using Nanoblog.Core.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Services
{
    public interface ICommentService
    {
        Task<CommentDto> AddAsync(string text, string authorId, string entryId);

        Task RemoveAsync(string id);

        Task UpdateAsync(string id, string text);

        Task<CommentDto> GetAsync(string id);

        Task<IEnumerable<CommentDto>> GetCommentsAsync(string entryId);
    }
}
