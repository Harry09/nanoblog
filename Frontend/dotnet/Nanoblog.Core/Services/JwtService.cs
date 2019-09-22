using Nanoblog.Common.Dto;
using System;
using System.Threading.Tasks;

namespace Nanoblog.Core.Services
{
    public class JwtService
    {
        static public JwtService Instance { get; set; } = new JwtService();

        JwtDto _jwt;

        public void SetJwt(JwtDto jwt)
        {
            _jwt = jwt;
        }

        public async Task<JwtDto> GetJwtAsync()
        {
            if (_jwt is null)
                return null;

            if (IsExpired())
            {
                _jwt = await AccountService.Instance.RefreshAccessToken(_jwt.RefreshToken);
            }

            return _jwt;
        }

        public async Task ResetAsync()
        {
            if (_jwt != null)
            {
                await AccountService.Instance.RevokeAccessToken(_jwt.RefreshToken);

                _jwt = null;
            }
        }

        private bool IsExpired()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() > _jwt.Expires;
        }
    }
}
