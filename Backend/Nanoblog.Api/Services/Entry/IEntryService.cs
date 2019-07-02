using Nanoblog.Core.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nanoblog.Api.Services
{
    public interface IEntryService : IService
    {
        Task<EntryDto> GetAsync(string id);

        Task<EntryDto> GetWithKarmaActionAsync(string id, string userId);

        Task<IEnumerable<EntryDto>> GetNewestAsync();

        Task<IEnumerable<EntryDto>> GetNewestithKarmaActionAsync(string userId);

        Task<EntryDto> AddAsync(string text, string authorId);

        Task RemoveAsync(string id);

        Task UpdateAsync(string id, string text);
    }
}
