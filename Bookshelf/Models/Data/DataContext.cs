using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.Configuration;
using System.IO;

namespace Bookshelf.Models.Data
{
    public class DataContext : DbContext 
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Shelf> Shelves { get; set; }
        DbSet<BookBind> BookBinds { get; set; }

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlite(connectionString);
            }
        }

    }
}
