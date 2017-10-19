using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSearch.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CustomSearch.Crawler");

            if( args.Length != 1 )
            {
                Console.WriteLine("Error: Invalid number of parameters");
                return;
            }

            string entrypoint = args[0];

            Console.WriteLine($"Entrypoint: {entrypoint}");

            var crawler = new Crawler();

            crawler.Start(entrypoint);
        }
    }
}
