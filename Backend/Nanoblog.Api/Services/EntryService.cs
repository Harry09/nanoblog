using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Dto;
using Nanoblog.Api.Data.Models;

namespace Nanoblog.Api.Services
{
	public class EntryService : IEntryService
	{
		readonly AppDbContext _dbContext;
		readonly IMapper _mapper;

		readonly IAccountService _accountService;

		public EntryService(AppDbContext appDbContext, IMapper mapper, IAccountService accountService)
		{
			_dbContext = appDbContext;
			_mapper = mapper;
			_accountService = accountService;
		}

		public void Add(string text, string authorId)
		{
			var user = _dbContext.Users.FirstOrDefault(x => x.Id == authorId);

			if (user is null)
			{
				throw new Exception("Cannot find this user!");
			}

			var entry = new Entry
			{
				Text = text,
				Author = user
			};

			_dbContext.Entries.Add(entry);
			_dbContext.SaveChanges();
		}

		public EntryDto Get(string id)
		{
			var entry = _dbContext.Entries.Include(x => x.Author).SingleOrDefault(x => x.Id == id);

			if (entry is null)
			{
				throw new Exception($"Entry with id {id} isn't exists!");
			}

			return _mapper.Map<Entry, EntryDto>(entry);
		}

		public void Remove(string id)
		{
			var entry = _dbContext.Entries.SingleOrDefault(x => x.Id == id);

			if (entry is null)
			{
				throw new Exception($"Entry with id {id} isn't exists!");
			}

			_dbContext.Entries.Remove(entry);
			_dbContext.SaveChanges();
		}

		public void Update(string id, string text)
		{
			var entry = _dbContext.Entries.SingleOrDefault(x => x.Id == id);

			if (entry is null)
			{
				throw new Exception($"Entry with id {id} isn't exists!");
			}

			entry.Text = text;
			entry.UpdateTime = DateTime.Now;

			_dbContext.SaveChanges();
		}
	}
}
