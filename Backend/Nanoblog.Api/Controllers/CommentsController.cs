using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nanoblog.Api.Data;
using Nanoblog.Api.Services;
using Nanoblog.Core.Data;
using Nanoblog.Core.Data.Commands;
using Nanoblog.Core.Data.Commands.Comment;
using Nanoblog.Core.Data.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nanoblog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public CommentsController(AppDbContext context, IMapper mapper, ICommentService commentService)
        {
            _context = context;
            _mapper = mapper;
            _commentService = commentService;
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetComment(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _commentService.GetWithKarmaActionAsync(id, userId);
        }

        // GET: api/comments/entry/5
        [HttpGet("entry/{entryId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(string entryId, [FromQuery] PagedQuery pagedQuery)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comments = await _commentService.GetCommentsWithKarmaActionAsync(entryId, userId);

            if (pagedQuery.LimitPerPage > 0)
            {
                var pagedResult = PagedResult<CommentDto>.Create(pagedQuery, comments);

                comments = pagedResult.Items;
            }

            return comments.ToList();
        }

        // POST: api/comments
        [HttpPost, Authorize]
        public async Task<ActionResult<CommentDto>> AddComment(AddComment comment)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _commentService.AddAsync(comment.Text, userId, comment.EntryId);
        }

        // DELETE: api/comments/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.RemoveAsync(id);

            return Ok();
        }
    }
}
