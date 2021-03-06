﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomSearch.Web.Models;

namespace CustomSearch.Web.Repositories
{
    public interface ISearchRepository
    {
        Task<SearchResultCollection> SearchAsync(string query);
    }
}
