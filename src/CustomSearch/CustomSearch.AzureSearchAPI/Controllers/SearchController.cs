using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomSearch.Api.Models;
using CustomSearch.Api.Repositories;
using Microsoft.Extensions.Options;
using CustomSearch.Api;

namespace CustomSearch.Web.Controllers
{
    [Route("api/search")]
    public class SearchController : Controller
    {
        static ISearchRepository _searchRepositorySingleton = new InMemorySearchRepository();

        ApplicationConfiguration _appConfig;

        public SearchController(IOptions<ApplicationConfiguration> options)
        {
            _appConfig = options.Value;
        }

        [HttpGet("")]
        public async Task<SearchResultCollection> Search([FromQuery]string q)
        {
            string query = q;

            var searchResults = await _searchRepositorySingleton.SearchAsync(query);

            return searchResults;
        }


        [HttpGet("/setting/provider")]
        public string GetProvider()
        {
            string provider = null;

            if (_searchRepositorySingleton is BingSearchRepository)
                provider = "bing";

            if (_searchRepositorySingleton is AzureSearchRepository)
                provider = "azure";

            if (_searchRepositorySingleton is InMemorySearchRepository)
                provider = "memory";

            return $"Current provider is [{provider}]";
        }

        [HttpGet("/setting/provider/{provider}")]
        public string Provider([FromRoute]string provider)
        {
            string setProvider = provider.ToLower();

            switch (setProvider)
            {
                case "bing":
                    _searchRepositorySingleton = new BingSearchRepository(
                        _appConfig.BingSearch.SubscriptionKey, 
                        _appConfig.BingSearch.CustomConfigId);

                    break;

                case "azure":
                    _searchRepositorySingleton = new AzureSearchRepository(
                        _appConfig.AzureSearch.SearchServiceName, 
                        _appConfig.AzureSearch.SearchServiceAdminApiKey, 
                        _appConfig.AzureSearch.SearchServiceIndex);

                    break;

                case "memory":
                    _searchRepositorySingleton = new InMemorySearchRepository();
                    break;

                default:
                    setProvider = null;
                    break;
            }

            if( setProvider == null )
            {
                return $"Invalid provider [{provider}]. Choose between [bing|azure|image]";
            }

            return setProvider;
        }
    }
}
