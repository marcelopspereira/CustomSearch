using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomSearch.Api.Models;
using CustomSearch.Api.Repositories;

namespace CustomSearch.Web.Controllers
{
    [Route("api/search")]
    public class SearchController : Controller
    {
        static ISearchRepository _searchRepositorySingleton = new InMemorySearchRepository();

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
                    _searchRepositorySingleton = new BingSearchRepository("", "");
                    break;

                case "azure":
                    _searchRepositorySingleton = new AzureSearchRepository();
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
