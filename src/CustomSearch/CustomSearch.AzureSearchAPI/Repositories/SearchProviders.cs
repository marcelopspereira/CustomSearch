using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSearch.Api.Repositories
{
    public class SearchProviders
    {
        Dictionary<string, ISearchRepository> _registeredSearches;

        public SearchProviders(IOptions<ApplicationConfiguration> options)
        {
            ApplicationConfiguration appConfig = options.Value;

            this._registeredSearches = RegisterSearchRepositories(appConfig);
            this.CurrentProvider = "memory";
        }

        public IEnumerable<string> AvailableProviders => _registeredSearches.Keys.AsEnumerable();

        public string CurrentProvider { get; private set; }

        public void SetCurrentProvider(string provider)
        {
            if (!_registeredSearches.ContainsKey(provider))
                throw new ArgumentOutOfRangeException($"search provider [{provider}] is not available");

            this.CurrentProvider = provider;
        }

        public ISearchRepository GetCurrentRepository()
        {
            return _registeredSearches[CurrentProvider];
        }

        Dictionary<string, ISearchRepository> RegisterSearchRepositories(ApplicationConfiguration appConfig)
        {
            var searches = new Dictionary<string, ISearchRepository>();

            var bing = CreateBing(appConfig.BingSearch);
            var azure = CreateAzureSearch(appConfig.AzureSearch);
            var memory = CreateInMemory();

            if (bing != null)
                searches.Add("bing", bing);

            if (azure != null)
                searches.Add("azure", azure);

            searches.Add("memory", memory);

            return searches;
        }

        ISearchRepository CreateBing(ApplicationConfiguration.BingSearchConfiguration settings)
        {
            if (String.IsNullOrEmpty(settings.SubscriptionKey) || String.IsNullOrEmpty(settings.CustomConfigId))
                return null;

            return new BingSearchRepository(
                        settings.SubscriptionKey,
                        settings.CustomConfigId);
        }

        ISearchRepository CreateAzureSearch(ApplicationConfiguration.AzureSearchConfiguration settings)
        {
            if (String.IsNullOrEmpty(settings.SearchServiceName) || 
                String.IsNullOrEmpty(settings.SearchServiceAdminApiKey) || 
                String.IsNullOrEmpty(settings.SearchServiceIndex))
                    return null;

            return new AzureSearchRepository(
                        settings.SearchServiceName,
                        settings.SearchServiceAdminApiKey,
                        settings.SearchServiceIndex);
        }

        ISearchRepository CreateInMemory()
        {
            return new InMemorySearchRepository();
        }
        
    }
}
