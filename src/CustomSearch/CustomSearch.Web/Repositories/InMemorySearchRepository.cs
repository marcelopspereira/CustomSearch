using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomSearch.Web.Models;

namespace CustomSearch.Web.Repositories
{
    public class InMemorySearchRepository : ISearchRepository
    {
        public Task<SearchResultCollection> SearchAsync(string query)
        {
            var result = new SearchResultCollection()
            {
                Results = new List<SearchResult>()
                {
                    new SearchResult { Title = "API: Resultado Square", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", Link = "https://www.bing.com/search?q=square"},
                    new SearchResult { Title = "API: Resultado Circle", Description = "Quam elementum pulvinar etiam non. Vel turpis nunc eget lorem dolor sed viverra.", Link = "https://www.bing.com/search?q=circle"},
                    new SearchResult { Title = "API: Resultado Line", Description = "Sed velit dignissim sodales ut eu sem integer vitae justo. Adipiscing vitae proin sagittis nisl rhoncus.", Link = "https://www.bing.com/search?q=line"}
                }
            };

            return Task.FromResult(result);
        }
    }
}
