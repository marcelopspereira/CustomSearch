using CustomSearch.Api.Repositories;
using System;
using System.Linq;
using Xunit;

namespace CustomSearch.Tests
{
    public class AzureSearchTest
    {
        [Fact]
        public void SimpleTest()
        {            
            string searchServiceName = AppConfig.Configuration["AzureSearch:SearchServiceName"];
            string adminApiKey = AppConfig.Configuration["AzureSearch:SearchServiceAdminApiKey"];
            string indexName = AppConfig.Configuration["AzureSearch:SearchServiceIndex"];
            
            var repo = new AzureSearchRepository(searchServiceName, adminApiKey, indexName);

            var result = repo.SearchAsync("house");

            Assert.NotNull(result.Result.Results);

            Assert.True(result.Result.Results.Count() > 5);
        }
    }
}
