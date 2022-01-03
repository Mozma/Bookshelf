using Microsoft.EntityFrameworkCore;
namespace Bookshelf.Models.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookBind> BookBinds { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<ShelfBind> ShelfBinds { get; set; }

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
        }
    }
}
