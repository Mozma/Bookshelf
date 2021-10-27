using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshelf.Models
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(o => o.Year).IsRequired(false);
            builder.Property(o => o.BookImageId).IsRequired(false);
            builder.Property(o => o.PagesNumber).IsRequired(false);
            
        }
    }
}