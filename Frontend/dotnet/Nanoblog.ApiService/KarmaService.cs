using Nanoblog.Common;
using Nanoblog.ApiService.Api;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nanoblog.ApiService
{
    public class KarmaService
    {
        public enum VoteFor
        {
            Entry,
            Comment
        }

        static public KarmaService Instance { get; set; } = new KarmaService();

        private readonly IKarmaAPI _karmaApi;

        private KarmaService()
        {
            _karmaApi = RestService.For<IKarmaAPI>(
                new HttpClient(new AuthenticatedHttpClientHandler())
                {
                    BaseAddress = new Uri($"{Config.Address}/api/karma")
                }
            );
        }

        public async Task UpVote(VoteFor vote, string itemId)
        {
            await _karmaApi.UpVote(vote.ToString().ToLower(), itemId);
        }

        public async Task DownVote(VoteFor vote, string itemId)
        {
            await _karmaApi.DownVote(vote.ToString().ToLower(), itemId);
        }

        public async Task RemoveVote(VoteFor vote, string itemId)
        {
            await _karmaApi.RemoveVote(vote.ToString().ToLower(), itemId);
        }

        public async Task<int> CountKarma(VoteFor vote, string itemId)
        {
            return await _karmaApi.CountKarma(vote.ToString().ToLower(), itemId);
        }

        public async Task<KarmaValue> GetUserVote(VoteFor vote, GetUserVoteParams param)
        {
            return await _karmaApi.GetUserVote(vote.ToString().ToLower(), param);
        }
    }
}
