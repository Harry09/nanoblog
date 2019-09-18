using Nanoblog.Common.Data.Commands.Entry;
using Nanoblog.Common.Data.Dto;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Nanoblog.Core.Services.Api
{
    public interface IEntryAPI
    {
        [Get("/newest")]
        Task<IEnumerable<EntryDto>> Newest();

        [Get("/{id]")]
        Task<EntryDto> Get(string id);

        [Post("")]
        [Headers("Authorization: Bearer")]
        Task<EntryDto> Add([Body] AddEntry entry);

        [Delete("/{id}")]
        [Headers("Authorization: Bearer")]
        Task Delete(string id);
    }
}
