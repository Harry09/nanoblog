using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Services.Karma;
using Nanoblog.Common.Dto;
using Nanoblog.Common.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanoblog.Api.Services
{
    public class CommentService : ICommentService
    {
        readonly AppDbContext _dbContext;
        readonly IMapper _mapper;
        readonly IKarmaService _karmaService;

        public CommentService(AppDbContext appDbContext, ICommentKarmaService karmaService, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
            _karmaService = karmaService;
        }

        public async Task<CommentDto> GetAsync(string id)
        {
            return await GetWithKarmaActionAsync(id, null);
        }

        public async Task<CommentDto> GetWithKarmaActionAsync(string id, string userId)
        {
            var comment = await FindCommentAsync(id);

            if (comment != null && comment.Deleted)
            {
                return null;
            }

            return await GetCommentDto(comment, userId);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAsync(string entryId)
        {
            return await GetCommentsWithKarmaActionAsync(entryId, null);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsWithKarmaActionAsync(string entryId, string userId)
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
                .OrderBy(x => x.CreateTime)
                .ToList();

            var commentsDto = new List<CommentDto>(comments.Count());

            foreach (var comment in comments)
            {
                commentsDto.Add(await GetCommentDto(comment, userId));
            }

            return commentsDto;
        }

        public async Task<CommentDto> AddAsync(string text, string authorId, string entryId)
        {
            var user = await _dbContext.Users.FindAsync(authorId);
            var entry = await _dbContext.Entries.FindAsync(entryId);

            var comment = new Comment(Guid.NewGuid().ToString(), user, entry, text);

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CommentDto>(comment);
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

        private async Task<CommentDto> GetCommentDto(Comment comment, string userId)
        {
            var commentDto = _mapper.Map<Comment, CommentDto>(comment);

            commentDto.KarmaCount = await _karmaService.CountKarmaAsync(commentDto.Id);
            commentDto.UserVote = await _karmaService.GetUserVoteAsync(userId, commentDto.Id);

            return commentDto;
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
