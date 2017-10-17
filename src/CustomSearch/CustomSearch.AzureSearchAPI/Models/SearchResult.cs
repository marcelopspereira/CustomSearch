using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSearch.Api.Models
{
    public class SearchResultCollection
    {
        public IEnumerable<SearchResult> Results;
    }

    public class SearchResult
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
}
