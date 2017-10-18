using CustomSearch.Api.Repositories;
using System;
using System.Linq;
using Xunit;

namespace CustomSearch.Tests
{
    public class BingSearchTest
    {
        [Fact]
        public void Test()
        {
            var repo = new BingSearchRepository();

            var result = repo.SearchAsync("teste");

            Assert.NotNull(result.Result.Results);

            Assert.True(result.Result.Results.Count() > 5);
        }
    }
}
