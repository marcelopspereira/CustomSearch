using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSearch.Crawler
{
    class ProcessorDb : IProcessor
    {
        private readonly string _connectionString;

        public ProcessorDb(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void Process(Page page)
        {
            string title = page.Title;
            string description = page.TextContent;
            string link = page.Url;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                
                //
                //  CREATE PROC procAddSearchWebPages(@title NVARCHAR(1000), @description NVARCHAR(MAX), @link VARCHAR(1000))
                //  AS
                //  SET NOCOUNT ON
                //  UPDATE SearchWebPages SET[Title] = @title, [Description]=@description WHERE[Link] = @link;
                //  IF @@ROWCOUNT = 0 
                //      INSERT SearchWebPages([Title], [Description], [Link]) VALUES(@title, @description, @link);
                //
                var cmd = new SqlCommand("procAddSearchWebPages", conn);
                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@link", link);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
