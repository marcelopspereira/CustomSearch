using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSearch.Crawler
{
    public interface IProcessor
    {
        void Process(Page page);
    }
}
