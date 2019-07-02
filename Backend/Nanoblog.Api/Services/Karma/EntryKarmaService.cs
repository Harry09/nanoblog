using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Core.Data;
using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Data.Exception;

namespace Nanoblog.Api.Services.Karma
{
    public class EntryKarmaService : IEntryKarmaService
    {
        AppDbContext dbContext;
        IMapper mapper;

        public EntryKarmaService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<KarmaDto> GetKarma(string itemId)
        {
            var karma = dbContext.EntryKarma
                .Include(x => x.Author)
                .Include(x => x.Entry)
                .Where(x => x.Entry.Id == itemId);

            return mapper.Map<IEnumerable<KarmaDto>>(karma);
        }

        public async Task<int> CountKarmaAsync(string itemId)
        {
            return await dbContext.EntryKarma
                .Include(x => x.Entry)
                .Where(x => x.Entry.Id == itemId)
                .SumAsync(x => (int)x.Value);
        }

        public async Task GiveKarmaAsync(string authorId, string itemId, KarmaValue value)
        {
            var karma = await FindEntryKarmaAsync(authorId, itemId);

            if (karma is null)
            {
                var user = await dbContext.Users.FindAsync(authorId);
                var entry = await dbContext.Entries.FindAsync(itemId);
                karma = new EntryKarma(user, entry, value);

                await dbContext.EntryKarma.AddAsync(karma);
            }
            else
            {
                karma.SetValue(value);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveKarmaAsync(string authorId, string itemId)
        {
            var karma = await FindEntryKarmaAsync(authorId, itemId);

            if (karma is null)
            {
                throw new ApiException("Cannot find karma!");
            }

            dbContext.EntryKarma.Remove(karma);
            await dbContext.SaveChangesAsync();
        }

        public async Task<KarmaValue> GetUserVoteAsync(string authorId, string itemId)
        {
            var karma = await FindEntryKarmaAsync(authorId, itemId);

            if (karma is null)
            {
                return KarmaValue.None;
            }

            return karma.Value;
        }

        private async Task<EntryKarma> FindEntryKarmaAsync(string authorId, string entryId)
        {
            return await dbContext.EntryKarma
                .Include(x => x.Author)
                .Include(x => x.Entry)
                .SingleOrDefaultAsync(x => x.Author.Id == authorId && x.Entry.Id == entryId);
        }
    }
}
