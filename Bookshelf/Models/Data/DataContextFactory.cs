using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bookshelf.Models.Data
{
    public static class DataContextFactory
    {

        public static DataContext GetDataContext(DbContextOptionsBuilder options) 
        {
            if (!options.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Default");
                options.UseSqlite(connectionString);
            }

            return new DataContext(options);
        }
    }
}
