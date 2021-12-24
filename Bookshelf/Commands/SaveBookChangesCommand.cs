using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using Bookshelf.ViewModels;
using System.Linq;

namespace Bookshelf.Commands
{
    public class SaveBookChangesCommand : CommandBase
    {
        private readonly EditBookViewModel _viewModel;
        private readonly BookStore _bookStore;
        private Book _book;

        public SaveBookChangesCommand(EditBookViewModel viewModel, BookStore bookStore, Book book)
        {
            _viewModel = viewModel;
            _bookStore = bookStore;
            _book = book;
        }

        public override void Execute(object parameter)
        {
            if (Validate())
            {
                SaveEntity();
            }
        }

        private bool Validate()
        {
            return true;
        }

        private void SaveEntity()
        {
            bool isDirty = false;

            if (!string.IsNullOrWhiteSpace(_viewModel.ISBN))
            {
                _viewModel.Entity.ISBN = _viewModel.ISBN;
                isDirty = true;
            }

            if (!string.IsNullOrWhiteSpace(_viewModel.Title))
            {
                _viewModel.Entity.Title = _viewModel.Title;
                isDirty = true;
            }


            if (!string.IsNullOrWhiteSpace(_viewModel.PagesNumber) && int.TryParse(_viewModel.PagesNumber, out int pagesNumber))
            {
                _viewModel.Entity.PagesNumber = pagesNumber;
                isDirty = true;
            }

            if (!string.IsNullOrWhiteSpace(_viewModel.PagesRead) && int.TryParse(_viewModel.PagesRead, out int pagesRead))
            {
                _viewModel.Entity.PagesRead = pagesRead;
                isDirty = true;
            }

            if (!string.IsNullOrWhiteSpace(_viewModel.Year) && int.TryParse(_viewModel.Year, out int year))
            {
                _viewModel.Entity.Year = year;
                isDirty = true;
            }

            using (var context = new DataContextFactory().CreateDbContext())
            {

                var image = context.Set<Image>().FirstOrDefault(i => i.Base64Data.Equals(_viewModel.Cover.BitmapToBase64String()));
                if (image == null)
                {
                    image = context.Set<Image>().Add(new Image
                    {
                        Base64Data = _viewModel.Cover.BitmapToBase64String()
                    }).Entity;
                }

                var publisher = context.Set<Publisher>().FirstOrDefault(p => p.Name.Equals(_viewModel.Publisher.Trim()));
                if (publisher == null)
                {
                    publisher = context.Set<Publisher>().Add(new Publisher
                    {
                        Name = _viewModel.Publisher.Trim()
                    }).Entity;
                }

                var author = context.Set<Author>().FirstOrDefault(p => p.FullName.Equals(_viewModel.Author.Trim()));
                if (author == null)
                {
                    author = context.Set<Author>().Add(new Author
                    {
                        FullName = _viewModel.Author.Trim()
                    }).Entity;
                }

                context.SaveChanges();

                _viewModel.Entity.ImageId = image.Id;
                _viewModel.Entity.PublisherId = publisher.Id;
                _viewModel.Entity.BookBinds[0].AuthorId = author.Id;

                isDirty = true;
            }

            if (isDirty)
            {
                using (var context = new DataContextFactory().CreateDbContext())
                {
                    context.Entry(_viewModel.Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.Entry(_viewModel.Entity.BookBinds[0]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChangesAsync();
                }

                _bookStore.ChangeEntity(_viewModel.Entity);
            }

            _viewModel.CloseCommand.Execute(this);
        }
    }
}
