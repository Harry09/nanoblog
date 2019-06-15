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

		public EntryDto Add(string text, string authorId)
		{
			if (text is null || text.Empty())
			{
				throw new ApiException("Text cannot be empty!");
			}

			var user = _dbContext.Users.FirstOrDefault(x => x.Id == authorId);

			if (user is null)
			{
				throw new ApiException("Cannot find this user!");
			}

			var entry = new Entry
			{
				Text = text,
				Author = user
			};

			_dbContext.Entries.Add(entry);
			_dbContext.SaveChanges();

            return _mapper.Map<Entry, EntryDto>(entry);
		}

		public EntryDto Get(string id)
		{
			var entry = _dbContext.Entries.SingleOrDefault(x => x.Id == id);

			if (entry is null)
			{
				throw new ApiException($"Entry with id {id} doesn't exist!");
			}

			return _mapper.Map<Entry, EntryDto>(entry);
		}

		public void Remove(string id)
		{
			var entry = _dbContext.Entries.SingleOrDefault(x => x.Id == id);

			if (entry is null)
			{
				throw new ApiException($"Entry with id {id} doesn't exist!");
			}

			_dbContext.Entries.Remove(entry);
			_dbContext.SaveChanges();
		}

		public void Update(string id, string text)
		{
			var entry = _dbContext.Entries.SingleOrDefault(x => x.Id == id);

			if (entry is null)
			{
				throw new ApiException($"Entry with id {id} doesn't exist!");
			}

			entry.Text = text;

			_dbContext.SaveChanges();
		}
	}
}
