using Nanoblog.Common.Commands.Account;
using Nanoblog.Common.Dto;
using Refit;
using System.Threading.Tasks;

namespace Nanoblog.Core.Services.Api
{
    public interface IAccountAPI
    {
        [Post("/register")]
        Task Register([Body] RegisterUser data);

        [Post("/login")]
        Task<JwtDto> Login([Body] LoginUser data);

        [Get("/user/byId/{id}")]
        Task<UserDto> GetUserById(string id);

        [Get("/user/byEmail/{email}")]
        Task<UserDto> GetUserByEmail(string email);

        [Get("/token/refresh/{refreshToken}")]
        Task<JwtDto> RefreshAccessToken(string refreshToken);

        [Get("/token/revoke/{refreshToken}")]
        Task RevokeAccessToken(string refreshToken);
    }
}
