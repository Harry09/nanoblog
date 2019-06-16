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
			if (text is null || text.Empty())
			{
				throw new ApiException("Text cannot be empty!");
			}

            var user = await _dbContext.Users.FindAsync(authorId);

			if (user is null)
			{
				throw new ApiException("Cannot find this user!");
			}

			var entry = new Entry
			{
				Text = text,
				Author = user
			};

			await _dbContext.Entries.AddAsync(entry);
			await _dbContext.SaveChangesAsync();

            return _mapper.Map<Entry, EntryDto>(entry);
		}

		public async Task<EntryDto> GetAsync(string id)
		{
            var entry = await FindEntryAsync(id);

            return _mapper.Map<Entry, EntryDto>(entry);
		}

		public async Task RemoveAsync(string id)
		{
            var entry = await FindEntryAsync(id);

            _dbContext.Entries.Remove(entry);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(string id, string text)
		{
            var entry = await FindEntryAsync(id);

			entry.Text = text;

			await _dbContext.SaveChangesAsync();
		}

        private async Task<Entry> FindEntryAsync(string id)
        {
            var entry = await _dbContext.Entries.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id);

            if (entry is null)
            {
                throw new ApiException($"Entry with id {id} doesn't exist!");
            }

            return entry;
        }
	}
}
