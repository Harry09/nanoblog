using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Nanoblog.Api.Data.Dto;
using Nanoblog.Api.Settings;
using Nanoblog.Api.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Nanoblog.Api.Services
{
	public class JwtHandler : IJwtHandler
	{
		JwtSettings _jwtSettings;

		public JwtHandler(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
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

			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)), 
				SecurityAlgorithms.HmacSha256);

			var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);

			var jwt = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				claims: claims,
				notBefore: now,
				expires: expires,
				signingCredentials: signingCredentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new JwtDto {
				Expires = expires.ToTimestamp(),
				Token = token
			};
		}
	}
}
