using Nanoblog.Common.Commands.Account;
using Nanoblog.Common.Dto;
using Nanoblog.ApiService.Api;
using Refit;
using System.Threading.Tasks;

namespace Nanoblog.ApiService
{
    public class AccountService
    {
        static public AccountService Instance { get; set; } = new AccountService();

        IAccountAPI _accountApi;

        public AccountService()
        {
            _accountApi = RestService.For<IAccountAPI>($"{Config.Address}/api/accounts");
        }

        public async Task<JwtDto> Login(LoginUser data)
        {
            return await _accountApi.Login(data);
        }

        public async Task Register(RegisterUser data)
        {
            await _accountApi.Register(data);
        }

        public async Task<UserDto> GetUserById(string id)
        {
            return await _accountApi.GetUserById(id);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            return await _accountApi.GetUserByEmail(email);
        }

        public async Task<JwtDto> RefreshAccessToken(string refreshToken)
        {
            return await _accountApi.RefreshAccessToken(refreshToken);
        }

        public async Task RevokeAccessToken(string refreshToken)
        {
            await _accountApi.RevokeAccessToken(refreshToken);
        }
    }
}
