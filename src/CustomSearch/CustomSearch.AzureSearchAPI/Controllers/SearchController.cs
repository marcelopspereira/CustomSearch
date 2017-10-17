using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using Search.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.API.Controllers
{
    public class SearchController : Controller
    {

        private readonly IAzureSearch azureSearch;

        public SearchController(IAzureSearch AzureSearch)
        {
            azureSearch = AzureSearch;
        }


        [HttpGet]
        public async Task<JsonResult> Exams(string criteria, string filter = null)
        {
            if (!String.IsNullOrEmpty(criteria))
            {
                var result = await azureSearch.SearchAsync(criteria, filter);
            return Json(JsonConvert.SerializeObject(result));
            }
            else
                return Json("Criteria is Empty");
        }

        [HttpGet]
        public async Task<JsonResult> ExamsMode(string criteria, int searchmode)
        {
            if (!String.IsNullOrEmpty(criteria))
            {
                SearchMode searchMode = SearchMode.Any;
                Enum.TryParse(searchmode.ToString(), out searchMode);

                var result = await azureSearch.SearchAsync(criteria, searchMode);
                return Json(JsonConvert.SerializeObject(result));
            }
            else
                return Json("Criteria is Empty");
        }

    }
}
