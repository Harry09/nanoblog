using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Nanoblog.Core.Extensions;
using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Data.Exception;

using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;

namespace Nanoblog.Api.Services
{
	public class EntryService : IEntryService
	{
		readonly AppDbContext _dbContext;
		readonly IMapper _mapper;

		public EntryService(AppDbContext appDbContext, IMapper mapper)
		{
			_dbContext = appDbContext;
			_mapper = mapper;
		}

		public async Task<EntryDto> AddAsync(string text, string authorId)
		{
            var user = await _dbContext.Users.FindAsync(authorId);
            var entry = new Entry(user, text);

			await _dbContext.Entries.AddAsync(entry);
			await _dbContext.SaveChangesAsync();

            return _mapper.Map<Entry, EntryDto>(entry);
		}

		public async Task<EntryDto> GetAsync(string id)
		{
            var entry = await FindEntryAsync(id);

            if (entry != null && entry.Deleted)
            {
                return null;
            }

            return await GetEntryDto(entry);
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

        private async Task<EntryDto> GetEntryDto(Entry entry)
        {
            var entryDto = _mapper.Map<Entry, EntryDto>(entry);

            var comments = await _commentService.GetCommentsAsync(entry.Id);

            entryDto.CommentsCount = comments.Count();

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
