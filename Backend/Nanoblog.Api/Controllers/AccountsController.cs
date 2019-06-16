using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Nanoblog.Core.Data.Commands.Account;
using Nanoblog.Api.Services;
using Nanoblog.Core.Data.Dto;

namespace Nanoblog.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/accounts")]
    [ApiController]
	public class AccountsController : Controller
    {
		readonly IAccountService _accountService;
		readonly IJwtHandler _jwtHandler;

		public AccountsController(IAccountService accountService, IJwtHandler jwtHandler)
		{
			_accountService = accountService;
			_jwtHandler = jwtHandler;
		}

        // POST: api/accounts/register
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUser data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _accountService.RegisterAsync(data.Email, data.UserName, data.Password);

			return Ok();
		}

        // POST: api/accounts/login
		[HttpPost("login")]
		public async Task<ActionResult<JwtDto>> Login([FromBody] LoginUser data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return await _accountService.LoginAsync(data.Email, data.Password);
		}

        // GET: api/accounts/user/5
		[HttpGet("user/{userId}")]
		public async Task<ActionResult<UserDto>> GetUser(string userId)
		{
			return await _accountService.GetUserAsync(userId);
		}

        // GET: api/accounts/tokens/refresh/5
        [HttpGet("tokens/refresh/{refreshToken}")]
		public ActionResult<JwtDto> RefreshAccessToken(string refreshToken)
		{
			return _accountService.RefreshAccessToken(refreshToken);
		}

        // GET: api/accounts/tokens/revoke/5
		[HttpGet("tokens/revoke/{refreshToken}")]
		public async Task<IActionResult> RevokeRefreshToken(string refreshToken)
		{
			await _accountService.RevokeRefreshTokenAsync(refreshToken);

			return Ok();
		}
	}
}
