using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomSearch
{
    static class AppConfig
    {
        public static IConfigurationRoot Configuration;

        static AppConfig()
        {
            var builder = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddJsonFile("config.secrets.json");


            Configuration = builder.Build();
        }
    }
}
