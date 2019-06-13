using Nanoblog.Core.Data.Dto;

namespace Nanoblog.Api.Services
{
    public interface IEntryService : IService
    {
        EntryDto Add(string text, string authorId);

		void Remove(string id);
		
		void Update(string id, string text);
		
		EntryDto Get(string id);
    }
}
