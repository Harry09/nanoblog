using Nanoblog.Common.Commands.Comment;
using Nanoblog.Common.Dto;
using Nanoblog.Core.Services.Api;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Core.Services
{
    public class CommentService
    {
        static public CommentService Instance { get; set; } = new CommentService();

        private readonly ICommentAPI _entryApi;

        public CommentService()
        {
            _entryApi = RestService.For<ICommentAPI>(
                new HttpClient(new AuthenticatedHttpClientHandler())
                {
                    BaseAddress = new Uri($"http://{Consts.ServerIp}:{Consts.ServerPort}/api/comments")
                }
                );
        }

        public async Task<CommentDto> Add(AddComment comment)
        {
            return await _entryApi.Add(comment);
        }

        public async Task Delete(string id)
        {
            await _entryApi.Delete(id);
        }

        public async Task<CommentDto> Get(string id)
        {
            return await _entryApi.Get(id);
        }

        public async Task<IEnumerable<CommentDto>> GetComments(string entryId)
        {
            return await _entryApi.GetComments(entryId);
        }
    }
}
