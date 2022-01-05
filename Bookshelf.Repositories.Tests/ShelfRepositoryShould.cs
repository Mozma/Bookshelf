using Bookshelf.Models;
using Bookshelf.Models.Data;
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

            DataContext context = Given.DataContext
                    .WithShelves(
                        Given.Shelf.WithId(shelfId),
                        Given.Shelf.WithId(4)
                    ).WithBooks(
                        Given.Book.WithId(1),
                        Given.Book.WithId(2),
                        Given.Book.WithId(3)
                    ).WithShelfBinds(
                        Given.ShelfBind.WithBookId(1).WithShelfId(shelfId),
                        Given.ShelfBind.WithBookId(2).WithShelfId(shelfId),
                        Given.ShelfBind.WithBookId(3).WithShelfId(4)
                    );
            var repository = new ShelfRepository(context);
            
            // Act
            repository.RemoveBooksFromShelf(shelfId);

            // Assert
            List<ShelfBind> shelfList = context.ShelfBinds.ToList();

            Assert.That(shelfList.Count(o => o.ShelfId == shelfId), Is.EqualTo(0));
            Assert.That(shelfList.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ReturnAllBooksForShelf()
        {
            // Arrange
            var shelfId = 1;

            DataContext context = Given.DataContext
                    .WithShelves(
                        Given.Shelf.WithId(shelfId),
                        Given.Shelf.WithId(4)
                    ).WithBooks(
                        Given.Book.WithId(1),
                        Given.Book.WithId(2),
                        Given.Book.WithId(3)
                    ).WithShelfBinds(
                        Given.ShelfBind.WithBookId(1).WithShelfId(shelfId),
                        Given.ShelfBind.WithBookId(2).WithShelfId(shelfId),
                        Given.ShelfBind.WithBookId(3).WithShelfId(4)
                    );
            var repository = new ShelfRepository(context);
         
            // Act
            IEnumerable<Book> books = repository.GetBooks(shelfId);
            
            // Assert
            Assert.That(books.Count(), Is.EqualTo(2));
            Assert.That(books.Count(o=>o.Id == 3), Is.EqualTo(0));
        }

        [Test]
        public void ReturnAllBooksForShelfInRightAscendingOrder()
        {
            // Arrange
            var shelfId = 1;

            DataContext context = Given.DataContext
                    .WithShelves(
                        Given.Shelf.WithId(shelfId)
                    ).WithBooks(
                        Given.Book.WithId(1).WithTitle("Second"),
                        Given.Book.WithId(2).WithTitle("First"),
                        Given.Book.WithId(3).WithTitle("Third")
                    ).WithShelfBinds(
                        Given.ShelfBind.WithBookId(1).WithShelfId(shelfId),
                        Given.ShelfBind.WithBookId(2).WithShelfId(shelfId),
                        Given.ShelfBind.WithBookId(3).WithShelfId(shelfId)
                    );
            var repository = new ShelfRepository(context);

            // Act
            IEnumerable<Book> books = repository.GetBooks(shelfId);

            // Assert
            Assert.That(books.First().Id, Is.EqualTo(2));
        }

        [Test]
        public void ReturnSimpleShelfInfo()
        {
            // Arrange
            DataContext context = Given.DataContext
                    .WithShelves(
                        Given.Shelf.WithId(11).WithName("Shelf2"),
                        Given.Shelf.WithId(22).WithName("Shelf1")
                    ).WithBooks(
                        Given.Book.WithId(1),
                        Given.Book.WithId(2),
                        Given.Book.WithId(3),
                        Given.Book.WithId(4)
                    ).WithShelfBinds(
                        Given.ShelfBind.WithBookId(1).WithShelfId(11),
                        Given.ShelfBind.WithBookId(2).WithShelfId(11),
                        Given.ShelfBind.WithBookId(3).WithShelfId(11),
                        Given.ShelfBind.WithBookId(4).WithShelfId(22)
                    );
            var repository = new ShelfRepository(context);

            // Act
            IEnumerable<ShelfInfoSimple> shelvesSimpleInfo = repository.GetShelvesNamesAndAmountOfBooks();

            // Assert
            Assert.That(shelvesSimpleInfo.Count(), Is.EqualTo(2));
            Assert.That(shelvesSimpleInfo.First().Id, Is.EqualTo(11));
        }

    }
}
