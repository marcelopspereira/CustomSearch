using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CustomSearch.Tests
{
    public class BingSearch
    {
        public void ExecuteSearch(string term)
        {
            var subscriptionKey = AppConfig.Configuration["BingSearch:SubscriptionKey"];
            var customConfigId = AppConfig.Configuration["BingSearch:CustomConfigId"];
            var searchTerm = term;

            var url = "https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search?" +
                "q=" + searchTerm +
                "&customconfig=" + customConfigId;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            var httpResponseMessage = client.GetAsync(url).Result;
            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            BingCustomSearchResponse response = JsonConvert.DeserializeObject<BingCustomSearchResponse>(responseContent);

            for (int i = 0; i < response.webPages.value.Length; i++)
            {
                var webPage = response.webPages.value[i];

                Console.WriteLine("name: " + webPage.name);
                Console.WriteLine("url: " + webPage.url);
                Console.WriteLine("displayUrl: " + webPage.displayUrl);
                Console.WriteLine("snippet: " + webPage.snippet);
                Console.WriteLine("dateLastCrawled: " + webPage.dateLastCrawled);
                Console.WriteLine();
            }
        }

    }

    public class BingCustomSearchResponse
    {
        public string _type { get; set; }
        public WebPages webPages { get; set; }
    }

    public class WebPages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public WebPage[] value { get; set; }
    }

    public class WebPage
    {
        public string name { get; set; }
        public string url { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
        public DateTime dateLastCrawled { get; set; }
        public string cachedPageUrl { get; set; }
        public OpenGraphImage openGraphImage { get; set; }
    }

    public class OpenGraphImage
    {
        public string contentUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }




}
