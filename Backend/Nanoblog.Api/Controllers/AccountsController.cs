using Microsoft.AspNetCore.Mvc;
using Nanoblog.Api.Services;
using Nanoblog.Common.Data.Commands.Account;
using Nanoblog.Common.Data.Dto;
using System.Threading.Tasks;

namespace Nanoblog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : Controller
    {
        readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
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

        // GET: api/accounts/user/byId/5
        [HttpGet("user/byId/{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var user = await _accountService.GetUserByIdAsync(userId);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/accounts/user/byEmail/{email}
        [HttpGet("user/byEmail/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _accountService.GetUserByEmailAsync(email);

            if (user is null)
            {
                return NotFound();
            }

            return user;
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
