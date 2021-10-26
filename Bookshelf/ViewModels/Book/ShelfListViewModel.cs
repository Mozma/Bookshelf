
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

            IDataManagerService<Book> bookService = new DataManagerService<Book>();
            IDataManagerService<Author> authorService = new DataManagerService<Author>();
            IDataManagerService<Shelf> shelvesService = new DataManagerService<Shelf>();
            IDataManagerService<BookBind> bookBindService = new DataManagerService<BookBind>();


            List<Book> bookItems = bookService.GetAll().Result.ToList();
            List<Shelf> shelfItems = shelvesService.GetAll().Result.ToList();

            Items = new List<ShelfListItemViewModel>();


            foreach (var item in shelfItems)
            {

                List<BookBind> bookBindItems = bookBindService.GetAll().Result
                    .Where(o => o.Shelf.Id == item.Id)
                    .ToList();

                var books = new List<BookListItemViewModel>() { };

                foreach (var bookBind in bookBindItems)
                {
                    books.Add(new BookListItemViewModel()
                    {
                        Title = bookService.Get(bookBind.Book.Id).Result.Title,
                        Author = authorService.Get(bookBind.Author.Id).Result.FullName
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
