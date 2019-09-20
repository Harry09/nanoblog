using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Services.Karma;
using Nanoblog.Common.Dto;
using Nanoblog.Common.Exception;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanoblog.Api.Services
{
    public class EntryService : IEntryService
    {
        readonly AppDbContext _dbContext;
        readonly ICommentService _commentService;
        readonly IKarmaService _karmaService;
        readonly IMapper _mapper;

        public EntryService(AppDbContext appDbContext, ICommentService commentService, IEntryKarmaService karmaService, IMapper mapper)
        {
            _dbContext = appDbContext;
            _commentService = commentService;
            _mapper = mapper;
            _karmaService = karmaService;
        }

        public async Task<EntryDto> GetAsync(string id)
        {
            return await GetWithKarmaActionAsync(id, null);
        }

        public async Task<EntryDto> GetWithKarmaActionAsync(string id, string userId)
        {
            var entry = await FindEntryAsync(id);

            if (entry != null && entry.Deleted)
            {
                return null;
            }

            return await GetEntryDto(entry, userId);
        }

        public async Task<IEnumerable<EntryDto>> GetNewestAsync()
        {
            return await GetNewestithKarmaActionAsync(null);
        }

        public async Task<IEnumerable<EntryDto>> GetNewestithKarmaActionAsync(string userId)
        {
            var entries = _dbContext.Entries
                            .Include(x => x.Author)
                            .Where(x => x.Deleted == false)
                            .OrderByDescending(x => x.CreateTime);

            var entriesDto = new List<EntryDto>(entries.Count());

            foreach (var entry in entries)
            {
                entriesDto.Add(await GetEntryDto(entry, userId));
            }

            return entriesDto;
        }

        public async Task<EntryDto> AddAsync(string text, string authorId)
        {
            var user = await _dbContext.Users.FindAsync(authorId);
            var entry = new Entry(user, text);

            await _dbContext.Entries.AddAsync(entry);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Entry, EntryDto>(entry);
        }

        public async Task RemoveAsync(string id)
        {
            var entry = await FindEntryAsync(id);

            entry.Delete();

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, string text)
        {
            var entry = await FindEntryAsync(id);

            entry.SetText(text);

            await _dbContext.SaveChangesAsync();
        }

        private async Task<EntryDto> GetEntryDto(Entry entry, string userId)
        {
            var entryDto = _mapper.Map<Entry, EntryDto>(entry);

            var comments = await _commentService.GetCommentsAsync(entry.Id);

            entryDto.CommentsCount = comments.Count();
            entryDto.KarmaCount = await _karmaService.CountKarmaAsync(entry.Id);
            entryDto.UserVote = await _karmaService.GetUserVoteAsync(userId, entry.Id);

            return entryDto;
        }

        private async Task<Entry> FindEntryAsync(string id)
        {
            var entry = await _dbContext.Entries
                .Include(x => x.Author)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entry is null)
            {
                throw new ApiException($"Entry with id {id} doesn't exist!");
            }

            return entry;
        }
    }
}
