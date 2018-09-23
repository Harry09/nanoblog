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

using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Data.Dto;
using Nanoblog.Api.Services;
using Nanoblog.Api.Data.Commands.Entry;
using Nanoblog.Api.Extensions;

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

			try
			{
				var entryDto = _entryService.Get(id);

				return Ok(entryDto);
			}
			catch (Exception ex)
			{
				return BadRequest(new ExceptionDto(ex.Message));
			}
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

			if (entry.Text is null || entry.Text.Empty())
			{
				return BadRequest(new ExceptionDto("Text cannot be empty!"));
			}

			if (userId is null)
			{
				return BadRequest(new ExceptionDto("No user data!"));
			}

			try
			{
				_entryService.Add(entry.Text, userId);

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		// PUT: entries/5
		[HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateEntry([FromRoute] string id, [FromBody] Entry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entry.Id)
            {
                return BadRequest();
            }

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Entries.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

		// DELETE: entries/5
		[HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteEntry([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var entry = _entryService.Get(id);

			if (entry is null)
			{
				return NotFound("Cannot find entry!");
			}

			if (entry.Author.Id != userId)
			{
				return BadRequest("You are not author of this post!");
			}

			try
			{
				_entryService.Remove(id);
			}
			catch (Exception ex)
			{
				return BadRequest(new ExceptionDto(ex.Message));
			}

			return Ok();
        }
    }
}
