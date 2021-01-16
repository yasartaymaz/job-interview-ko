using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        public static bool IsDevelopment()
        {
            var settings = GetConfig();

            return (settings["Environment"] != null && settings["Environment"] == "Development") ? true : false;
        }

        public static string GetDbContext(string contextName)
        {
            var settings = GetConfig();
            string connectionString = settings["ConnectionStrings:" + contextName];

            return connectionString;
        }
    }
}
