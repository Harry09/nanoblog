using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Nanoblog.Core.Data.Commands.Account;
using Nanoblog.Api.Services;

namespace Nanoblog.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/accounts")]
	public class AccountsController : Controller
	{
		readonly IAccountService _accountService;
		readonly IJwtHandler _jwtHandler;

		public AccountsController(IAccountService accountService, IJwtHandler jwtHandler)
		{
			_accountService = accountService;
			_jwtHandler = jwtHandler;
		}

		[Route("register"), HttpPost]
		public IActionResult Register([FromBody] RegisterUser data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_accountService.Register(data.Email, data.UserName, data.Password);

			return Ok();
		}

		[Route("login"), HttpPost]
		public IActionResult Login([FromBody] LoginUser data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(_accountService.Login(data.Email, data.Password));
		}

		[Route("user/{userId}")]
		public IActionResult GetUser(string userId)
		{
			return Json(_accountService.GetUser(userId));
		}

		[Route("tokens/refresh/{refreshToken}")]
		public IActionResult RefreshAccessToken(string refreshToken)
		{
			return Json(_accountService.RefreshAccessToken(refreshToken));
		}

		[HttpPost("tokens/revoke/{refreshToken}")]
		public IActionResult RevokeRefreshToken(string refreshToken)
		{
			_accountService.RevokeRefreshToken(refreshToken);

			return Ok();
		}
	}
}
