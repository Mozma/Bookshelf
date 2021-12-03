using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bookshelf.Models.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args = null)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlite(connectionString);
            }

            var context = new DataContext(optionsBuilder.Options);

            return context;
        }
    }
}
