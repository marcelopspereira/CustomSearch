using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSearch.Crawler
{
    class CrawlerDb : Crawler
    {
        private readonly string _connectionString;

        public CrawlerDb(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void Process(Page page)
        {
            string title = page.Title;
            string description = page.TextContent;
            string link = page.Url;

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("INSERT WebPages([Title], [Description], [Link]) VALUES (@title, @description, @link)", conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@link", link);

                cmd.ExecuteNonQuery();
            }

            //base.Process(page);
        }
    }
}
