using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nanoblog.Core.Data.Dto;

namespace Nanoblog.Api.Services
{
    public interface IAccountService : IService
    {
		Task RegisterAsync(string email, string userName, string password);

		Task<JwtDto> LoginAsync(string email, string password);

		Task<UserDto> GetUserByIdAsync(string id);

        Task<UserDto> GetUserByEmailAsync(string email);

		JwtDto RefreshAccessToken(string token);

		Task RevokeRefreshTokenAsync(string token);

		Task DisableAccountAsync(string id);

		Task EnableAccountAsync(string id);

		Task UpdateUserAsync(string id, string email, string userName, string password);
    }
}
