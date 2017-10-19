using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot.Crawler;
using Abot.Poco;
using System.Net;

namespace CustomSearch.Crawler
{
    class Crawler
    {
        public Crawler()
        {

        }

        public void Start(string entrypoint)
        {
            PoliteWebCrawler crawler = new PoliteWebCrawler();

            //crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            //crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            //crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

            CrawlResult result = crawler.Crawl(new Uri(entrypoint));

            // Log("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
            if (result.ErrorOccurred)
                throw new CrawlerException(result.ErrorException.Message);

            Log($"Crawl of {entrypoint} completed without error.");
        }

        void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            Log($"About to crawl link {e.PageToCrawl.Uri.AbsoluteUri} which was found on page {e.PageToCrawl.ParentUri.AbsoluteUri}");
        }

        void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var crawledPage = e.CrawledPage;
            string pageUri = crawledPage.Uri.AbsoluteUri;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                Log($"Crawl of page failed {pageUri}");
                return;
            }
            
            if (string.IsNullOrEmpty(crawledPage.Content.Text))
            {
                Log($"Page had no content {pageUri}");
                return;
            }

            // Parse the document
            var htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser

            var page = Page.CreateFrom(crawledPage);

            Parse(page);
        }

        void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            Log($"Did not crawl the links on page {e.CrawledPage.Uri.AbsoluteUri} due to {e.DisallowedReason}");
        }

        void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            Log($"Did not crawl page {e.PageToCrawl.Uri.AbsoluteUri} due to {e.DisallowedReason}");
        }

        void Log(string text)
        {
            Console.WriteLine(text);
        }

        void Parse(Page page)
        {
            Log($"Crawl of page succeeded {page.Url} ({page.Host} | {page.Path} | {page.Query})");
        }
    }
}
