using Nanoblog.Common.Data.Commands.Account;
using Nanoblog.Common.Data.Dto;
using Nanoblog.Core.Services.Api;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Services
{
    public class AccountService
    {
        static public AccountService Instance { get; set; } = new AccountService();

        IAccountAPI _accountApi;

        public AccountService()
        {
            _accountApi = RestService.For<IAccountAPI>($"http://{Consts.ServerIp}:{Consts.ServerPort}/api/accounts");
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
