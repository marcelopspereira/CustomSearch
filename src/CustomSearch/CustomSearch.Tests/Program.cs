using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CustomSearch.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var bing = new BingSearchTest();

            bing.SimpleTest();


            //Test Bing Custom Search API
            //BingCustomSearchTest();

        }

        private static void BingCustomSearchTest()
        {
            var term = Console.ReadLine();

            var runQuery = new BingSearch();

            runQuery.ExecuteSearch(term);

            Console.ReadLine();
        }
    }
}
