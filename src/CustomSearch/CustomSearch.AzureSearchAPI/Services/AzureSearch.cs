using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.API.Services
{
    public class AzureSearch : IAzureSearch
    {
        public String ServiceName { get; set; }
        public String ApiKey { get; set; }

        public String IndexName { get; set; }

        private ISearchServiceClient azureSearchService;
        private ISearchIndexClient indexClient;

        public AzureSearch(String serviceName, String apiKey, string indexName)
        {

            ServiceName = serviceName;
            ApiKey = apiKey;
            IndexName = indexName;

            azureSearchService = new SearchServiceClient(ServiceName, new SearchCredentials(ApiKey));
            indexClient = azureSearchService.Indexes.GetClient(IndexName);
        }

        private async Task<DocumentSearchResult<Document>> Search(ISearchIndexClient indexClient, string searchText, string filter = null, List<string> order = null, List<string> facets = null, SearchMode searchMode = SearchMode.All)
        {
            // Execute search based on search text and optional filter
            var sp = new SearchParameters();

            // Add Filter
            if (!String.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
            }

            // Order
            if (order != null && order.Count > 0)
            {
                sp.OrderBy = order;
            }

            // facets
            if (facets != null && facets.Count > 0)
            {
                sp.Facets = facets;
            }

            sp.SearchMode = searchMode;
            sp.Top = 100;

            DocumentSearchResult<Document> response = await indexClient.Documents.SearchAsync<Document>(searchText, sp);

            foreach (SearchResult<Document> result in response.Results)
            {
                Console.WriteLine(result.Document + " - Score: " + result.Score);
            }

            if (response.Facets != null)
            {
                foreach (var facet in response.Facets)
                {
                    Console.WriteLine("\n Facet Name: " + facet.Key);
                    foreach (var value in facet.Value)
                    {
                        Console.WriteLine("Value :" + value.Value + " - Count: " + value.Count);
                    }
                }
            }

            return response;

        }

        public async Task<DocumentSearchResult<Document>> SearchAsync(string criteria)
        {
            var result = await Search(indexClient, criteria);

            return result;
        }

        public async Task<DocumentSearchResult<Document>> SearchAsync(string criteria, string filter)
        {
            var result = await Search(indexClient, criteria, filter);

            return result;
        }

        public async Task<DocumentSearchResult<Document>> SearchAsync(string criteria, SearchMode searchMode)
        {
            var result = await Search(indexClient, criteria, null, null, null, searchMode);

            return result;
        }

    }


}
