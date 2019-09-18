using Nanoblog.Common.Data.Commands.Entry;
using Nanoblog.Common.Data.Dto;
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
    public class EntryService
    {
        static public EntryService Instance { get; set; } = new EntryService();

        private readonly IEntryAPI _entryApi;

        private EntryService()
        {
            _entryApi = RestService.For<IEntryAPI>(
                new HttpClient(new AuthenticatedHttpClientHandler())
                {
                    BaseAddress = new Uri($"http://{Consts.ServerIp}:{Consts.ServerPort}/api/entries")
                }
                );
        }

        public async Task<IEnumerable<EntryDto>> Newest()
        {
            return await _entryApi.Newest();
        }

        public async Task<EntryDto> Get(string id)
        {
            return await _entryApi.Get(id);
        }

        public async Task<EntryDto> Add(AddEntry entry)
        {
            return await _entryApi.Add(entry);
        }

        public async Task Delete(string id)
        {
            await _entryApi.Delete(id);
        }
    }
}
