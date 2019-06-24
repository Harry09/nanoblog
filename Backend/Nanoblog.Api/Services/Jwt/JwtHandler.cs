using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nanoblog.Api.Settings;
using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Extensions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nanoblog.Api.Services
{
    public class JwtHandler : IJwtHandler
    {
        readonly JwtSettings _jwtSettings;
        readonly SigningCredentials _signingCredentials;

        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256);
        }

        public JwtDto CreateToken(string id, string role)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(), ClaimValueTypes.Integer64)
            };


            var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Expires = expires.ToTimestamp(),
                Token = token
            };
        }
    }
}
