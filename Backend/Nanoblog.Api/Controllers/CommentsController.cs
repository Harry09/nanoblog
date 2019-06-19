﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanoblog.Api.Data;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Services;
using Nanoblog.Core.Data.Commands.Comment;
using Nanoblog.Core.Data.Dto;

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
            return await _commentService.GetAsync(id);
        }

        // GET: api/comments/entry/5
        [HttpGet("entry/{entryId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(string entryId)
        {
            var comments = await _commentService.GetCommentsAsync(entryId);

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