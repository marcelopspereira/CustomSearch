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
        private readonly SearchProviders _searchProviders;

        public SearchController(IOptions<ApplicationConfiguration> options, SearchProviders searchProviders)
        {
            this._searchProviders = searchProviders;
        }

        [HttpGet("")]
        public async Task<SearchResultCollection> Search([FromQuery]string q)
        {
            string query = q;

            var searchRepository = _searchProviders.GetCurrentRepository();

            var searchResults = await searchRepository.SearchAsync(query);

            return searchResults;
        }


        [HttpGet("/setting/provider")]
        public string GetProvider()
        {
            string provider = _searchProviders.CurrentProvider;
            string availableProviders = String.Join('|', _searchProviders.AvailableProviders);

            return $"The current provider is [{provider}]. Available providers: [{availableProviders}]";
        }

        [HttpGet("/setting/provider/{provider}")]
        public string Provider([FromRoute]string provider)
        {
            string setProvider = provider.ToLower();
            
            if( setProvider == null )
            {
                string availableProviders = String.Join('|', _searchProviders.AvailableProviders);

                return $"Invalid provider [{provider}]. Choose between [{availableProviders}]";
            }

            _searchProviders.SetCurrentProvider(provider);
            
            return $"The current provider is [{_searchProviders.CurrentProvider}]";
        }
    }
}
