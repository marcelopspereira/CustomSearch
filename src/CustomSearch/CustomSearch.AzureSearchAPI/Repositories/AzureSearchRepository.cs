using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomSearch.Api.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace CustomSearch.Api.Repositories
{
    public class AzureSearchRepository : ISearchRepository
    {
        ISearchIndexClient _client;

        public AzureSearchRepository(string searchServiceName, string adminApiKey, string indexName)
        {
            _client = CreateSearchServiceClient(searchServiceName, adminApiKey, indexName);
        }

        public Task<SearchResultCollection> SearchAsync(string query)
        {
            var documentList = _client.Documents.Search<AzureSearchModel>(query);

            var results = from d in documentList.Results
                          select new Models.SearchResult()
                          {
                              Title = d.Document.Title ?? "No title available",
                              Category = d.Document.Category ?? "No category",
                              Description = d.Document.Description ?? "",
                              Link = d.Document.Link
                          };

            var searchResultCollection = new SearchResultCollection() { Results = results.ToList() };

            return Task.FromResult(searchResultCollection);
        }

        private static ISearchIndexClient CreateSearchServiceClient(string searchServiceName, string adminApiKey, string indexName)
        {
            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);

            return indexClient;
        }        
    }
}
