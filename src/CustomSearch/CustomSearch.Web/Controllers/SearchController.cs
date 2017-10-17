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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Results([FromQuery]string q)
        {
            string query = q;

            InMemorySearchRepository search = new InMemorySearchRepository();

            var searchResults = search.Search(query);

            ViewBag.query = query;
            ViewBag.searchResults = searchResults;

            return View();
        }
    }
}
