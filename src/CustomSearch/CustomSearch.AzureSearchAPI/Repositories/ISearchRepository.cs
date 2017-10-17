using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomSearch.Api.Models;

namespace CustomSearch.Api.Repositories
{
    public interface ISearchRepository
    {
        Task<SearchResultCollection> SearchAsync(string query);
    }
}
