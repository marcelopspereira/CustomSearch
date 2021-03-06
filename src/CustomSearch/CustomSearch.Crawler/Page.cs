﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot.Poco;

namespace CustomSearch.Crawler
{
    public class Page
    {
        public string Url { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }

        internal static Page CreateFrom(CrawledPage crawledPage)
        {
            return new Page()
            {
                Host = crawledPage.Uri.Host,
                Path = crawledPage.Uri.AbsolutePath,
                Query = crawledPage.Uri.Query,
                Url = crawledPage.Uri.AbsoluteUri,
                Title = crawledPage.AngleSharpHtmlDocument.Title,
                TextContent = crawledPage.Content.Text
            };
        }
    }
}
