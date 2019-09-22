using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanoblog.Common;
using Refit;

namespace Nanoblog.Core.Services.Api
{
    public class GetUserVoteParams
    {
        [AliasAs("userId")]
        public string UserId { get; set; }

        [AliasAs("itemId")]
        public string ItemId { get; set; }
    }

    public interface IKarmaAPI
    {
        [Get("/{service}/upvote/{itemId}")]
        [Headers("Authorization: Bearer")]
        Task UpVote(string service, string itemId);

        [Get("/{service}/downvote/{itemId}")]
        [Headers("Authorization: Bearer")]
        Task DownVote(string service, string itemId);

        [Delete("/{service}/remove/{itemId}")]
        [Headers("Authorization: Bearer")]
        Task RemoveVote(string service, string itemId);

        [Get("/{service}/count/{itemId}")]
        Task<int> CountKarma(string service, string itemId);

        [Get("/{service}")]
        Task<KarmaValue> GetUserVote(string service, [Query] GetUserVoteParams param);
    }
}
