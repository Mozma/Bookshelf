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

        public DataContext(DbContextOptionsBuilder options)
        {


            base.OnConfiguring(options);
        }
    }
}
