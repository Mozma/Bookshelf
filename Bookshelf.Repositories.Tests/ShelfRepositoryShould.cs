using Bookshelf.Models;
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
                new Book()
                {
                    Id = 1,
                    Title = "book1"
                },
                new Book()
                {
                    Id = 2,
                    Title = "book2"
                },
                new Book()
                {
                    Id = 3,
                    Title = "book3"
                });
            context.Shelves.Add(new Shelf()
            {
                Id = shelfId,
                Name = "shelf1"
            });
            context.Shelves.Add(new Shelf()
            {
                Id = 4,
                Name = "shelf2"
            });
            context.SaveChanges();
            context.ShelfBinds.AddRange(

                    new ShelfBind()
                    {
                        BookId = 1,
                        ShelfId = shelfId
                    },
                    new ShelfBind()
                    {
                        BookId = 2,
                        ShelfId = shelfId
                    },
                    new ShelfBind()
                    {
                        BookId = 3,
                        ShelfId = 4
                    });
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
