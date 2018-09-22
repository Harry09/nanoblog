using System;

using Nanoblog.Api.Data.Dto;

namespace Nanoblog.Api.Services
{
    public interface IEntryService : IService
    {
		void Add(string text, string authorId);

		void Remove(string id);
		
		void Update(string id, string text);
		
		EntryDto Get(string id);
    }
}
