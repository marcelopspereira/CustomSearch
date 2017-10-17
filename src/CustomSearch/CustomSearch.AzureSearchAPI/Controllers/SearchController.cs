﻿using System;
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
        [HttpGet("")]
        public IEnumerable<SearchResult> Search([FromQuery]string q)
        {
            string query = q;

            InMemorySearchRepository search = new InMemorySearchRepository();

            var searchResults = search.Search(query);

            return searchResults;            
        }
    }
}
