using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using Bookshelf.ViewModels;
using System.Linq;

namespace Bookshelf.Commands
{
    internal class CreateBookCommand : CommandBase
    {
        private readonly AddBookViewModel _viewModel;
        private readonly BookStore _bookStore;

        public CreateBookCommand(AddBookViewModel viewModel, BookStore bookStore)
        {
            _viewModel = viewModel;
            _bookStore = bookStore;
        }

        public override void Execute(object parameter)
        {

            if (Validate())
            {
                AddBook();

                _viewModel.CloseCommand.Execute(this);
            }
        }

        private bool Validate()
        {
            //TODO: Написать проверку и показ диалога с ошибкой

            if (string.IsNullOrWhiteSpace(_viewModel.BookTitle))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.AuthorName))
            {
                return false;
            }

            if (_viewModel.SelectedShelf is null)
            {
                return false;
            }

            return true;
        }

        private void AddBook()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                var author = CreateAuthor(context);
                var book = CreateBook(context);

                CreateShelfBind(context, book);
                CreateBookBind(context, book, author);

                _bookStore.CreateEntity(book);
            }
        }

        private void CreateBookBind(DataContext context, Book book, Author author)
        {
            if (context.Set<BookBind>().FirstOrDefault(b => b.BookId == book.Id && b.AuthorId == author.Id) == null)
            {
                context.Set<BookBind>().Add(new BookBind
                {
                    BookId = book.Id,
                    AuthorId = author.Id
                });
                context.SaveChanges();
            }

            context.SaveChanges();
        }

        private void CreateShelfBind(DataContext context, Book book)
        {
            var shelf = context.Set<Shelf>().FirstOrDefault(s => s.Name == _viewModel.SelectedShelf.Name);
            if (IsNewShelfBind(shelf, book, context))
            {
                context.Set<ShelfBind>().Add(new ShelfBind
                {
                    BookId = book.Id,
                    ShelfId = shelf.Id
                });
                context.SaveChanges();
            }
        }

        private bool IsNewShelfBind(Shelf shelf, Book book, DataContext context)
        {
            return context.Set<ShelfBind>().FirstOrDefault(s => s.BookId == book.Id && s.ShelfId == shelf.Id) == null;
        }

        private Book CreateBook(DataContext context)
        {
            var book = context.Set<Book>().FirstOrDefault(b => b.Title == _viewModel.BookTitle);

            var image = CreateImage(context);

            if (book == null)
            {
                book = context.Set<Book>().Add(new Book
                {
                    Title = _viewModel.BookTitle,
                    ImageId = image.Id,
                }).Entity;
                context.SaveChanges();
            }

            return book;
        }

        private Author CreateAuthor(DataContext context)
        {
            var author = context.Set<Author>().FirstOrDefault(a => a.FullName == _viewModel.AuthorName.Trim());

            if (author == null)
            {
                author = context.Set<Author>().Add(new Author { FullName = _viewModel.AuthorName }).Entity;
                context.SaveChanges();
            }

            return author;
        }

        private Image CreateImage(DataContext context)
        {
            var image = context.Set<Image>().FirstOrDefault(i => i.Base64Data.Equals(_viewModel.Cover.BitmapToBase64String()));

            if (image == null)
            {
                image = context.Set<Image>().Add(new Image
                {
                    Base64Data = _viewModel.Cover.BitmapToBase64String()
                }).Entity;
                context.SaveChanges();
            }

            return image;
        }
    }
}