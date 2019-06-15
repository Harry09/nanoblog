using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Data.Exception;
using Nanoblog.Core.Extensions;

namespace Nanoblog.Api.Services
{
    public class CommentService : ICommentService
    {
        readonly AppDbContext _dbContext;
        readonly IMapper _mapper;

        public CommentService(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<CommentDto> AddAsync(string text, string authorId, string entryId)
        {
            if (text is null || text.Empty())
            {
				throw new ApiException("Text cannot be empty!");
            }

            var user = await _dbContext.Users.FindAsync(authorId);

            if (user is null)
            {
                throw new ApiException("Cannot find this user!");
            }

            var entry = await _dbContext.Entries.FindAsync(entryId);

            if (entry is null)
            {
                throw new ApiException("Cannot find entry!");
            }

            var comment = new Comment
            {
                Author = user,
                Parent = entry,
                Text = text
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> GetAsync(string id)
        {
            var comment = await _dbContext.Comments.FindAsync(id);

            if (comment is null)
            {
                throw new ApiException($"Comment with id {id} doesn't exist!");
            }

            return _mapper.Map<Comment, CommentDto>(comment);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAsync(string entryId)
        {
            var entry = await _dbContext.Entries.FindAsync(entryId);

            if (entry is null)
            {
                throw new ApiException($"Cannot find entry with id: {entryId}");
            }

            var comments = _dbContext.Comments.Include(x => x.Parent).Where(x => x.Parent.Id == entryId);

            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);
        }

        public async Task RemoveAsync(string id)
        {
            var comment = await _dbContext.Comments.FindAsync(id);

            if (comment is null)
            {
                throw new ApiException($"Comment with id {id} doesn't exist!");
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, string text)
        {
            var comment = await _dbContext.Comments.FindAsync(id);

            if (comment is null)
            {
                throw new ApiException($"Comment with id {id} doesn't exist!");
            }

            comment.Text = text;

            await _dbContext.SaveChangesAsync();
        }
    }
}
