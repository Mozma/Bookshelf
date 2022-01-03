using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshelf.Models
{
    public class ShelfBindConfiguration : IEntityTypeConfiguration<ShelfBind>
    {
        public void Configure(EntityTypeBuilder<ShelfBind> builder)
        {
            builder.Navigation(o => o.Book).AutoInclude();
            builder.Navigation(o => o.Shelf).AutoInclude();
        }
    }
}