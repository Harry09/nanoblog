using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Data.Exception;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var user = await _dbContext.Users.FindAsync(authorId);
            var entry = await _dbContext.Entries.FindAsync(entryId);

            var comment = new Comment(user, entry, text);

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> GetAsync(string id)
        {
            var comment = await FindCommentAsync(id);

            if (comment != null && comment.Deleted)
            {
                return null;
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

            var comments = _dbContext.Comments
                .Include(x => x.Author)
                .Include(x => x.Parent)
                .Where(x => x.Parent.Id == entryId)
                .Where(x => x.Deleted == false)
                .OrderBy(x => x.CreateTime);

            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);
        }

        public async Task RemoveAsync(string id)
        {
            var comment = await FindCommentAsync(id);

            comment.Delete();

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, string text)
        {
            var comment = await FindCommentAsync(id);

            comment.SetText(text);

            await _dbContext.SaveChangesAsync();
        }

        private async Task<Comment> FindCommentAsync(string id)
        {
            var comment = await _dbContext.Comments
                .Include(x => x.Author)
                .Include(x => x.Parent)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (comment is null)
            {
                throw new ApiException($"Comment with id {id} doesn't exist!");
            }

            return comment;
        }
    }
}
