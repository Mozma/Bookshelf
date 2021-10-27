
using Bookshelf.Models;
using Bookshelf.Models.Interfaces;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Linq;

namespace Bookshelf.ViewModels
{
    public class ShelfListViewModel : BaseViewModel
    {
        public static ShelfListViewModel Instance => new ShelfListViewModel();

        public List<ShelfListItemViewModel> Items { get; set; }

        public ShelfListViewModel()
        {

            SetupView();
        }

        public void SetupView()
        {

            IRepository<Book> bookService = new Repository<Book>();
            IRepository<Author> authorService = new Repository<Author>();
            IRepository<Shelf> shelvesService = new Repository<Shelf>();
            IRepository<BookBind> bookBindService = new Repository<BookBind>();


            List<Book> bookItems = bookService.GetAll().ToList();
            List<Shelf> shelfItems = shelvesService.GetAll().ToList();

            Items = new List<ShelfListItemViewModel>();


            foreach (var item in shelfItems)
            {

                List<BookBind> bookBindItems = bookBindService.GetAll()
                    .Where(o => o.Shelf.Id == item.Id)
                    .ToList();

                var books = new List<BookListItemViewModel>() { };

                foreach (var bookBind in bookBindItems)
                {
                    books.Add(new BookListItemViewModel()
                    {
                        Title = bookService.Get(bookBind.Book.Id).Title,
                        Author = authorService.Get(bookBind.Author.Id).FullName
                    });
                }

                Items.Add(
                    new ShelfListItemViewModel()
                    {
                        Name = item.Name,
                        Items = books
                    });
            }
        }
    }
}
