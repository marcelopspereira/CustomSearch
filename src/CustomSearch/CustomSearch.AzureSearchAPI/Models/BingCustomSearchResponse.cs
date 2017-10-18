using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSearch.Api.Models
{
    public class BingCustomSearchResponse
    {
        public string _type { get; set; }
        public BingWebPages webPages { get; set; }
    }
}