using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshelf.Models
{
    public class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
        }
    }
}