using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomSearch.Api.Models;

namespace CustomSearch.Api.Repositories
{
    public class BingSearchRepository : ISearchRepository
    {
        const string url = "https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search";

        public BingSearchRepository(string subscriptionKey, string customConfigId)
        {

        }

        public Task<SearchResultCollection> SearchAsync(string query)
        {
            return ExampleAsync(query);
        }

        Task<SearchResultCollection> ExampleAsync(string query)
        {
            var result = new SearchResultCollection()
            {
                Results = new List<SearchResult>()
                {
                    new SearchResult { Title = "BingSearchRepository: Resultado Square", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", Link = "https://www.bing.com/search?q=square"},
                    new SearchResult { Title = "BingSearchRepository: Resultado Circle", Description = "Quam elementum pulvinar etiam non. Vel turpis nunc eget lorem dolor sed viverra.", Link = "https://www.bing.com/search?q=circle"},
                    new SearchResult { Title = "BingSearchRepository: Resultado Line", Description = "Sed velit dignissim sodales ut eu sem integer vitae justo. Adipiscing vitae proin sagittis nisl rhoncus.", Link = "https://www.bing.com/search?q=line"}
                }
            };

            return Task.FromResult(result);
        }
    }
}
