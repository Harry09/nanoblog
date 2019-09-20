using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Services;
using Nanoblog.Api.Services.Karma;
using Nanoblog.Common;
using Nanoblog.Common.Dto;

namespace Nanoblog.Api.Controllers
{
    [Route("api/karma")]
    [ApiController]
    public class KarmaController : ControllerBase
    {
        IKarmaService entryKarmaService;
        IKarmaService commentKarmaService;
        IEntryService entryService;
        ICommentService commentService;

        public KarmaController(
            IEntryKarmaService entryKarmaService,
            ICommentKarmaService commentKarmaService,
            IEntryService entryService,
            ICommentService commentService
            )
        {
            this.entryKarmaService = entryKarmaService;
            this.commentKarmaService = commentKarmaService;
            this.entryService = entryService;
            this.commentService = commentService;
        }

        // GET: api/karma/entry/upvote/3 
        // or 
        // GET: api/karma/comment/upvote/3
        [HttpGet("{service}/upvote/{itemId}"), Authorize]
        public async Task<IActionResult> UpVote(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await IsUserAuthorOfItem(service, userId, itemId))
                return Forbid();

            await karmaService.GiveKarmaAsync(userId, itemId, KarmaValue.Plus);

            return Ok();
        }

        // GET: api/karma/entry/downvote/3 
        // or 
        // GET: api/karma/comment/downvote/3
        [HttpGet("{service}/downvote/{itemId}"), Authorize]
        public async Task<IActionResult> DownVote(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await IsUserAuthorOfItem(service, userId, itemId))
                return Forbid();

            await karmaService.GiveKarmaAsync(userId, itemId, KarmaValue.Minus);

            return Ok();
        }

        // DELETE: api/karma/entry/remove/3 
        // or 
        // DELETE: api/karma/comment/remove/3
        [HttpDelete("{service}/remove/{itemId}"), Authorize]
        public async Task<IActionResult> RemoveVote(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await IsUserAuthorOfItem(service, userId, itemId))
                return Forbid();

            await karmaService.RemoveKarmaAsync(userId, itemId);

            return Ok();
        }

        // GET: api/karma/entry/count/3
        // or
        // GET: api/karma/comment/count/3
        [HttpGet("{service}/count/{itemId}")]
        public async Task<ActionResult<int>> CountKarma(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            return await karmaService.CountKarmaAsync(itemId);
        }

        // GET: api/karma/entry/?userId=3&itemId=4
        // or
        // GET: api/karma/comment/?userId=3&itemId=4
        [HttpGet("{service}")]
        public async Task<ActionResult<KarmaValue>> GetUserVote(string service, [FromQuery] string userId, [FromQuery] string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            return await karmaService.GetUserVoteAsync(userId, itemId);
        }

        private async Task<bool> IsUserAuthorOfItem(string service, string userId, string itemId)
        {
            switch (service)
            {
                case "entry":
                {
                    var entry = await entryService.GetAsync(itemId);

                    return entry?.Author?.Id == userId;
                }
                case "comment":
                    var comment = await commentService.GetAsync(itemId);

                    return comment?.Author?.Id == userId;
                default:
                    return false;
            }
        }

        private IKarmaService GetKarmaService(string service)
        {
            switch (service)
            {
                case "entry":
                    return entryKarmaService;
                case "comment":
                    return commentKarmaService;
                default:
                    return null;
            }
        }
    }
}
