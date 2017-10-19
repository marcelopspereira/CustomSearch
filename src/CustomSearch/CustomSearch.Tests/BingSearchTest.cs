using CustomSearch.Api.Repositories;
using System;
using System.Linq;
using Xunit;

namespace CustomSearch.Tests
{
    public class BingSearchTest
    {
        [Fact]
        public void SimpleTest()
        {
            var subscriptionKey = AppConfig.Configuration["BingSearch:SubscriptionKey"];
            var customConfigId = AppConfig.Configuration["BingSearch:CustomConfigId"];

            var repo = new BingSearchRepository(subscriptionKey, customConfigId);

            var result = repo.SearchAsync("credito");

            Assert.NotNull(result.Result.Results);

            Assert.True(result.Result.Results.Count() > 2);
        }
    }
}
