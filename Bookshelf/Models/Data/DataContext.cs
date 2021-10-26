using Microsoft.EntityFrameworkCore;
namespace Bookshelf.Models.Data
{
    public class DataContext : DbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Shelf> Shelves { get; set; }
        DbSet<BookBind> BookBinds { get; set; }
        DbSet<Image> Images {  get; set; }  
        DbSet<Status> Statuses { get; set; } 

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
