using Microsoft.EntityFrameworkCore;
namespace Bookshelf.Models.Data
{
    public class DataContext : DbContext
    {
        DbSet<Image> Images { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<BookBind> BookBinds { get; set; }
        DbSet<Publisher> Publishers { get; set; }
        DbSet<Shelf> Shelves { get; set; }
        DbSet<ShelfBind> ShelfBinds { get; set; }
        DbSet<Status> Statuses { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuthorConfiguration());

            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new BookBindConfiguration());
            builder.ApplyConfiguration(new PublisherConfiguration());
            builder.ApplyConfiguration(new ShelfConfiguration());
            builder.ApplyConfiguration(new ShelfBindConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());
        }
    }
}
