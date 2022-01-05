using Bookshelf.Models;
using Bookshelf.Tests;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Bookshelf.Repositories.Tests
{
    public class ShelfRepositoryShould : RepositoryTestBase
    {
        [Test]
        public void RemoveBookFromShelf()
        {
            // Arrange
            var shelfId = 1;

            context.Books.AddRange(
                Given.Book.WithId(1),
                Given.Book.WithId(2),
                Given.Book.WithId(3)
                );
            
            context.Shelves.AddRange(
                Given.Shelf.WithId(shelfId),
                Given.Shelf.WithId(4)
                );
            context.SaveChanges();
            
            context.ShelfBinds.AddRange(
                Given.ShelfBind.WithBookId(1).WithShelfId(shelfId),
                Given.ShelfBind.WithBookId(2).WithShelfId(shelfId),
                Given.ShelfBind.WithBookId(3).WithShelfId(4)
                );
            context.SaveChanges();

            var repository = new ShelfRepository(context);

            // Act
            repository.RemoveBooksFromShelf(shelfId);

            // Assert
            List<ShelfBind> shelfList = context.ShelfBinds.ToList();

            Assert.That(shelfList.Count(o => o.ShelfId == shelfId), Is.EqualTo(0));
            Assert.That(shelfList.Count(), Is.EqualTo(1));
        }
    }
}
