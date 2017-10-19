using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSearch.Crawler
{
    class CrawlerException : Exception
    {
        public CrawlerException(string message) : base(message)
        {
        }
    }
}
