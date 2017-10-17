using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.API.Services
{
    public interface IAzureSearch
    {
        Task<DocumentSearchResult<Document>> SearchAsync(String criteria);
        Task<DocumentSearchResult<Document>> SearchAsync(String criteria, string filter);
        Task<DocumentSearchResult<Document>> SearchAsync(string criteria,  SearchMode searchMode);
    }
}
