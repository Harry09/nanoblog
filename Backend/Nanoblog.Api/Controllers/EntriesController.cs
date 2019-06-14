using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using AutoMapper;
using Nanoblog.Core.Data.Dto;
using Nanoblog.Core.Data.Commands.Entry;
using Nanoblog.Core.Data.Exception;
using Nanoblog.Core.Extensions;
using Nanoblog.Api.Services;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;

namespace Nanoblog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/entries")]
    public class EntriesController : Controller
    {
        private readonly AppDbContext _context;

		private readonly IMapper _mapper;

		private readonly IEntryService _entryService;

		private readonly IAccountService _accountService;

        public EntriesController(AppDbContext context, IMapper mapper, IEntryService entryService, IAccountService accountService)
        {
            _context = context;
			_mapper = mapper;
			_entryService = entryService;
			_accountService = accountService;
		}

		// GET: entries
		[HttpGet]
        public IActionResult GetEntries()
        {
			var entries = _context.Entries.Include(x => x.Author);

			return Json(entries.Select(x => _mapper.Map<EntryDto>(x)).OrderByDescending(x => x.CreateTime));
		}

		// GET: entries/5
		[HttpGet("{id}")]
        public IActionResult GetEntry([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
		
			return Ok(_entryService.Get(id));
		}

		// POST: entries
		[HttpPost, Authorize]
		public IActionResult AddEntry([FromBody] AddEntry entry)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId is null)
			{
				return BadRequest(new ErrorDto("No user data!"));
			}

            return Ok(_entryService.Add(entry.Text, userId));
		}

		// DELETE: entries/5
		[HttpDelete("{id}"), Authorize]
        public IActionResult DeleteEntry([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var entry = _entryService.Get(id);

			if (entry is null)
			{
				return NotFound(new ApiException("Cannot find entry!"));
			}

			if (entry.AuthorId != userId)
			{
				return BadRequest(new ApiException("You are not author of this post!"));
			}

			_entryService.Remove(id);

			return Ok();
        }
    }
}
