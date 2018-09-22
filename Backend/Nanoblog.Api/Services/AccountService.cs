using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Nanoblog.Api.Common;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Dto;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Encryption;

namespace Nanoblog.Api.Services
{
	public class AccountService : IAccountService
	{
		readonly AppDbContext _appDbContext;
		readonly IMapper _mapper;

		public AccountService(AppDbContext appDbContext, IMapper mapper)
		{
			_appDbContext = appDbContext;
			_mapper = mapper;
		}

		public void AddUser(string email, string userName, string password)
		{
			if (_appDbContext.Users.Any(x => x.Email == email || x.UserName == userName))
			{
				throw new Exception("This user already exists!");
			}

			var salt = Hasher.GetSalt();
			var hash = Hasher.GetHash(password, salt);

			var user = new User
			{
				Email = email,
				UserName = userName,
				Salt = salt,
				Password = hash,
				Role = Roles.UserRole
			};

			_appDbContext.Users.Add(user);
			_appDbContext.SaveChanges();
		}


		public void DisableAccount(string id)
		{
		}

		public void EnableAccount(string id)
		{
		}

		public UserDto GetUser(string id)
		{
			var user = _appDbContext.Users.FirstOrDefault(x => x.Id == id);

			return _mapper.Map<UserDto>(user);
		}

		public UserDto GetUser(string email, string password)
		{
			var user = _appDbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == Hasher.GetHash(password, x.Salt));

			return _mapper.Map<UserDto>(user);
		}

		public void UpdateUser(string id, string email, string userName, string password)
		{
		}
	}
}
