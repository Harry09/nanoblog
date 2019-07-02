using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Core.Data;
using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Data.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Services.Karma
{
    public class CommentKarmaService : ICommentKarmaService
    {
        AppDbContext dbContext;
        IMapper mapper;

        public CommentKarmaService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<KarmaDto> GetKarma(string itemId)
        {
            var karma = dbContext.CommentKarma
                .Include(x => x.Author)
                .Include(x => x.Comment)
                .Where(x => x.Comment.Id == itemId);

            return mapper.Map<IEnumerable<KarmaDto>>(karma);
        }

        public async Task<int> CountKarmaAsync(string itemId)
        {
            return await dbContext.CommentKarma
                .Include(x => x.Comment)
                .Where(x => x.Comment.Id == itemId)
                .SumAsync(x => (int)x.Value);
        }

        public async Task GiveKarmaAsync(string authorId, string itemId, KarmaValue value)
        {
            var karma = await FindCommentKarmaAsync(authorId, itemId);

            if (karma is null)
            {
                var user = await dbContext.Users.FindAsync(authorId);
                var comment = await dbContext.Comments.FindAsync(itemId);
                karma = new CommentKarma(user, comment, value);

                await dbContext.CommentKarma.AddAsync(karma);
            }
            else
            {
                karma.SetValue(value);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveKarmaAsync(string authorId, string itemId)
        {
            var karma = await FindCommentKarmaAsync(authorId, itemId);

            if (karma is null)
            {
                throw new ApiException("Cannot find karma!");
            }

            dbContext.CommentKarma.Remove(karma);
            await dbContext.SaveChangesAsync();
        }

        public async Task<KarmaValue> GetUserVoteAsync(string authorId, string commentId)
        {
            var karma = await FindCommentKarmaAsync(authorId, commentId);

            if (karma is null)
            {
                return KarmaValue.None;
            }

            return karma.Value;
        }

        private async Task<CommentKarma> FindCommentKarmaAsync(string authorId, string commentId)
        {
            return await dbContext.CommentKarma
                .Include(x => x.Author)
                .Include(x => x.Comment)
                .SingleOrDefaultAsync(x => x.Author.Id == authorId && x.Comment.Id == commentId);
        }
    }
}
