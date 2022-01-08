using Bookshelf.Models;
using Bookshelf.Repositories;
using Bookshelf.Stores;
using Bookshelf.ViewModels;

namespace Bookshelf.Commands
{
    internal class CreateBookFromInfoCommand : CommandBase
    {
        private readonly AddBookByIsbnViewModel _viewModel;
        private readonly BookStore _bookStore;

        public CreateBookFromInfoCommand(AddBookByIsbnViewModel viewModel, BookStore bookStore)
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

            if (string.IsNullOrWhiteSpace(_viewModel.Title))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.Author))
            {
                return false;
            }

            using (var unitOfWork = new UnitOfWork())
            {
                if (unitOfWork.BookBinds.CheckIfLinkExist(_viewModel.Title.Trim(), _viewModel.Author.Trim()))
                {
                    return false;
                }
            }
            return true;
        }

        private void AddBook()
        {

            var book = new Book();

            if (_viewModel.ISBN != null)
            {
                book.ISBN = _viewModel.ISBN;
            }

            if (!string.IsNullOrWhiteSpace(_viewModel.Title))
            {
                book.Title = _viewModel.Title;
            }

            if (!string.IsNullOrWhiteSpace(_viewModel.Year)
                && int.TryParse(_viewModel.Year, out int year))
            {
                book.Year = year;
            }

            if (!string.IsNullOrWhiteSpace(_viewModel.PagesNumber)
                 && int.TryParse(_viewModel.PagesNumber, out int pagesNumber))
            {
                book.PagesNumber = pagesNumber;
            }

            using (var unitOfWork = new UnitOfWork())
            {
                var author = CreateAuthor(unitOfWork);
                var publisher = CreatPublisher(unitOfWork);

                book.PublisherId = publisher.Id;

                book = CreateBook(unitOfWork, book);
                unitOfWork.Complete();

                CreateBookBind(unitOfWork, book, author);

            }

            _bookStore.CreateEntity(book);
        }

        private void CreateBookBind(UnitOfWork unitOfWork, Book book, Author author)
        {
            unitOfWork.BookBinds.CreateIfNotExist(book, author);
        }

        private Book CreateBook(UnitOfWork unitOfWork, Book book)
        {
            return unitOfWork.Books.AddAndReturn(book);
        }

        private Publisher CreatPublisher(UnitOfWork unitOfWork)
        {
            return unitOfWork.Publishers.GetOrCreatePublisherWithName(_viewModel.Entity.Publisher);
        }

        private Author CreateAuthor(UnitOfWork unitOfWork)
        {
            return unitOfWork.Authors.GetOrCreateAuthorWithName(_viewModel.Entity.Author);
        }
    }
}