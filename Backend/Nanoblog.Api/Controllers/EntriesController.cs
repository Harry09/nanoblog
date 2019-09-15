using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nanoblog.Api.Data;
using Nanoblog.Api.Services;
using Nanoblog.Common.Data;
using Nanoblog.Common.Data.Commands;
using Nanoblog.Common.Data.Commands.Entry;
using Nanoblog.Common.Data.Dto;
using Nanoblog.Common.Data.Exception;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nanoblog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/entries")]
    [ApiController]
    public class EntriesController : Controller
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        private readonly IEntryService _entryService;

        public EntriesController(AppDbContext context, IMapper mapper, IEntryService entryService)
        {
            _context = context;
            _mapper = mapper;
            _entryService = entryService;
        }

        // GET: api/entries/newest
        [HttpGet("newest")]
        public async Task<ActionResult<IEnumerable<EntryDto>>> GetNewestEntries([FromQuery] PagedQuery pagedQuery)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var entries = await _entryService.GetNewestithKarmaActionAsync(userId);

            if (pagedQuery.LimitPerPage > 0)
            {
                var pagedResult = PagedResult<EntryDto>.Create(pagedQuery, entries);

                entries = pagedResult.Items;
            }

            return entries.ToList();
        }

        // GET: api/entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntryDto>> GetEntry(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _entryService.GetWithKarmaActionAsync(id, userId);
        }

        // POST: api/entries
        [HttpPost, Authorize]
        public async Task<ActionResult<EntryDto>> AddEntry(AddEntry entry)
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

            return await _entryService.AddAsync(entry.Text, userId);
        }

        // DELETE: api/entries/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteEntry(string id)
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

            var entry = await _entryService.GetAsync(id);

            if (entry.Author.Id != userId)
            {
                return BadRequest(new ApiException("You are not author of this post!"));
            }

            await _entryService.RemoveAsync(id);

            return Ok();
        }
    }
}
