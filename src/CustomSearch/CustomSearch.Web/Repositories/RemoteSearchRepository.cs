using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CustomSearch.Web.Models;
using Newtonsoft.Json;

namespace CustomSearch.Web.Repositories
{
    public class RemoteSearchRepository : ISearchRepository
    {
        HttpClient _client;
        readonly string _baseAddress;
        const string API_SEARCH_REQUEST = "/api/search";

        public RemoteSearchRepository(string remoteUrl)
        {
            _client = new HttpClient();
            _baseAddress = remoteUrl;
        }

        public async Task<SearchResultCollection> SearchAsync(string query)
        {
            UriBuilder builder = new UriBuilder(_baseAddress);
            builder.Path = API_SEARCH_REQUEST;
            builder.Query = "q=" + query;

            string result = await _client.GetStringAsync(builder.Uri);

            return JsonConvert.DeserializeObject<SearchResultCollection>(result);
        }
    }
}
