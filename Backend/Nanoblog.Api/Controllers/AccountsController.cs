using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Nanoblog.Api.Services;
using Nanoblog.Api.Data.Commands.Account;
using Nanoblog.Api.Data.Dto;

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

		[Route("register")]
		public IActionResult Register([FromBody] RegisterUser data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_accountService.Register(data.Email, data.UserName, data.Password);
			}
			catch (Exception ex)
			{
				return BadRequest(new ExceptionDto(ex.Message));
			}

			return Ok();
		}

		[Route("login")]
		public IActionResult Login([FromBody] LoginUser data)
		{
			try
			{
				return Ok(_accountService.Login(data.Email, data.Password));
			}
			catch (Exception ex)
			{
				return BadRequest(new ExceptionDto(ex.Message));
			}
		}

		[Route("user/{userId}")]
		public IActionResult GetUser(string userId)
		{
			try
			{
				return Json(_accountService.GetUser(userId));
			}
			catch (Exception ex)
			{
				return BadRequest(new ExceptionDto(ex.Message));
			}
		}

		[Route("tokens/refresh/{token}")]
		public IActionResult RefreshAccessToken(string token)
		{
			try
			{
				return Json(_accountService.RefreshAccessToken(token));
			}
			catch (Exception ex)
			{
				return BadRequest(new ExceptionDto(ex.Message));
			}
		}

		[HttpPost("tokens/revoke/{token}")]
		public IActionResult RevokeRefreshToken(string token)
		{
			try
			{
				_accountService.RevokeRefreshToken(token);
			}
			catch (Exception ex)
			{
				return BadRequest(new ExceptionDto(ex.Message));
			}

			return Ok();
		}
	}
}
