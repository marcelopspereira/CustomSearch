using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSearch.Api
{
    public class ApplicationConfiguration
    {
        public AzureSearchConfiguration AzureSearch { get; set; }
        public BingSearchConfiguration BingSearch { get; set; }

        public class AzureSearchConfiguration
        {
            public string SearchServiceName { get; set; }
            public string SearchServiceAdminApiKey { get; set; }
            public string SearchServiceIndex { get; set; }
        }

        public class BingSearchConfiguration
        {
            public string SubscriptionKey { get; set; }
            public string CustomConfigId { get; set; }
        }
    }
}
