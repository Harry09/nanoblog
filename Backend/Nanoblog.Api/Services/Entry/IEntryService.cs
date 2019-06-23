using Nanoblog.Core.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nanoblog.Api.Services
{
    public interface IEntryService : IService
    {
        Task<EntryDto> AddAsync(string text, string authorId);

		Task RemoveAsync(string id);
		
		Task UpdateAsync(string id, string text);
		
		Task<EntryDto> GetAsync(string id);

        Task<IEnumerable<EntryDto>> GetNewestAsync();
    }
}
