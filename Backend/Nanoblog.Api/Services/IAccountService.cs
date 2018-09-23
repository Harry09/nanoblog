using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nanoblog.Api.Data.Dto;

namespace Nanoblog.Api.Services
{
    public interface IAccountService : IService
    {
		void Register(string email, string userName, string password);

		JwtDto Login(string email, string password);

		UserDto GetUser(string id);

		JwtDto RefreshAccessToken(string token);

		void RevokeRefreshToken(string token);

		void DisableAccount(string id);

		void EnableAccount(string id);

		void UpdateUser(string id, string email, string userName, string password);
    }
}
