using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomSearch.Api.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace CustomSearch.Api.Repositories
{
    public class BingSearchRepository : ISearchRepository
    {
        const string url = "https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search";

        public BingSearchRepository(string subscriptionKey, string customConfigId)
        {

        }

        public List<SearchResult> Results { get; private set; }

        public Task<SearchResultCollection> SearchAsync(string query)
        {
            var subscriptionKey = AppConfig.Configuration["BingSearch:SubscriptionKey"];
            var customConfigId = AppConfig.Configuration["BingSearch:CustomConfigId"];
            var searchTerm = query;

            var url = "https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search?" +
                "q=" + searchTerm +
                "&customconfig=" + customConfigId;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            var httpResponseMessage = client.GetAsync(url).Result;
            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;

            BingCustomSearchResponse response = JsonConvert.DeserializeObject<BingCustomSearchResponse>(responseContent);


            Results = new List<SearchResult>();


            for (int i = 0; i < response.webPages.value.Length; i++)
            {
                var webPage = response.webPages.value[i];
                Results.Add(new SearchResult() { Title = webPage.name, Description = webPage.snippet, Link = webPage.url, Category = "" });

            }


            var result = new SearchResultCollection();

            result.Results = Results;

            return Task.FromResult(result);
        }

  

    }
            
    
}
