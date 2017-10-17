using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomSearch.Web.Models;
using CustomSearch.Web.Repositories;

namespace CustomSearch.Web.Controllers
{
    public class SearchController : Controller
    {
        ISearchRepository _search;

        public SearchController(ISearchRepository search)
        {
            _search = search;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Results([FromQuery]string q)
        {
            string query = q;
            
            var searchResults = await _search.SearchAsync(query);

            ViewBag.query = query;
            ViewBag.searchResults = searchResults.Results;

            return View();
        }
    }
}
