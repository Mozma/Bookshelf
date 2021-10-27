using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshelf.Models
{
    public class BookBindConfiguration : IEntityTypeConfiguration<BookBind>
    {
        public void Configure(EntityTypeBuilder<BookBind> builder)
        {
            builder.Navigation(o => o.Author).AutoInclude();
            builder.Navigation(o => o.Book).AutoInclude();
            builder.Navigation(o => o.Shelf).AutoInclude();
        }
    }
}