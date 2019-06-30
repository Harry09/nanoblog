using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Services.Karma;
using Nanoblog.Core.Data;
using Nanoblog.Core.Data.Dto;

namespace Nanoblog.Api.Controllers
{
    [Route("api/karma")]
    [ApiController]
    public class KarmaController : ControllerBase
    {
        IKarmaService entryKarmaService;
        IKarmaService commentKarmaService;

        public KarmaController(IEntryKarmaService entryKarmaService, ICommentKarmaService commentKarmaService)
        {
            this.entryKarmaService = entryKarmaService;
            this.commentKarmaService = commentKarmaService;
        }

        [Route("{service}/{itemId}")]
        public ActionResult<IEnumerable<KarmaDto>> GetEntryKarma(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            return karmaService.GetKarma(itemId).ToList();
        }

        [Route("{service}/upvote/{itemId}"), Authorize]
        public async Task<IActionResult> UpVoteEntry(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await karmaService.GiveKarmaAsync(userId, itemId, KarmaValue.Plus);

            return Ok();
        }

        [Route("{service}/downvote/{itemId}"), Authorize]
        public async Task<IActionResult> DownVoteEntry(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await karmaService.GiveKarmaAsync(userId, itemId, KarmaValue.Minus);

            return Ok();
        }

        [HttpDelete("{service}/remove/{itemId}"), Authorize]
        public async Task<IActionResult> RemoveVoteEntry(string service, string itemId)
        {
            var karmaService = GetKarmaService(service);

            if (karmaService is null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await karmaService.RemoveKarmaAsync(userId, itemId);

            return Ok();
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
