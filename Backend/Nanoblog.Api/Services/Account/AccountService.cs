using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Data.Exception;
using Nanoblog.Api.Common;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;

namespace Nanoblog.Api.Services
{
	public class AccountService : IAccountService
	{
		readonly AppDbContext _appDbContext;
		readonly IJwtHandler _jwtHandler;
		readonly IMapper _mapper;
		readonly IPasswordHasher<User> _passwordHasher;

		public AccountService(AppDbContext appDbContext, IJwtHandler jwtHandler, IMapper mapper, IPasswordHasher<User> passwordHasher)
		{
			_appDbContext = appDbContext;
			_jwtHandler = jwtHandler;
			_mapper = mapper;
			_passwordHasher = passwordHasher;
		}

		public void Register(string email, string userName, string password)
		{
			if (_appDbContext.Users.Any(x => x.Email == email || x.UserName == userName))
			{
				throw new ApiException("This user already exists!");
			}

			var user = new User
			{
				Email = email,
				UserName = userName,
				Role = Roles.UserRole
			};

			var hash = _passwordHasher.HashPassword(user, password);

			user.Password = hash;

			_appDbContext.Users.Add(user);
			_appDbContext.SaveChanges();
		}

		public JwtDto Login(string email, string password)
		{
			var user = _appDbContext.Users.SingleOrDefault(x => x.Email == email);

			if (user is null)
				throw new ApiException("Email or password is incorrect!");

			if (_passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Failed)
				throw new ApiException("Email or password is incorrect!");

			var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

			var refreshToken = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
				.Replace("+", string.Empty)
				.Replace("=", string.Empty)
				.Replace("/", string.Empty);

			jwt.RefreshToken = refreshToken;

			// revoke other tokens for this user
			foreach (var token in _appDbContext.RefreshTokens.Where(x => x.User == user))
			{
				token.Revoked = true;
			}

			_appDbContext.RefreshTokens.Add(new RefreshToken { User = user, Token = refreshToken });
			_appDbContext.SaveChanges();

			return jwt;
		}

		public JwtDto RefreshAccessToken(string token)
		{
			var refreshToken = GetRefreshToken(token);

			if (refreshToken == null)
			{
				throw new ApiException("Refresh token was not found.");
			}

			if (refreshToken.Revoked)
			{
				throw new ApiException("Refresh token was revoked");
			}

			var jwt = _jwtHandler.CreateToken(refreshToken.User.Id, refreshToken.User.Role);
			;
			jwt.RefreshToken = refreshToken.Token;

			return jwt;
		}

		public void RevokeRefreshToken(string token)
		{
			var refreshToken = GetRefreshToken(token);

			if (refreshToken == null)
			{
				throw new ApiException("Refresh token was not found.");
			}

			if (refreshToken.Revoked)
			{
				throw new ApiException("Refresh token was already revoked.");
			}

			refreshToken.Revoked = true;
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

		public void UpdateUser(string id, string email, string userName, string password)
		{
		}

		private RefreshToken GetRefreshToken(string token)
		{
			return _appDbContext.RefreshTokens.Include(x => x.User).ToList().SingleOrDefault(x => x.Token == token);

		}
	}
}
