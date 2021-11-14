using Bookshelf.Models;

using Bookshelf.Services;
using System.Collections.Generic;
using System.Linq;

namespace Bookshelf.ViewModels
{
    public class ShelvesViewModel : BaseViewModel
    {
        public List<ShelfViewModel> Items { get; set; }

        public ShelvesViewModel()
        {
            SetupView();
        }

        public void SetupView()
        {
            var shelvesRepository = new Repository<Shelf>();
            var bookBindRepository = new Repository<BookBind>();
            var shelfBindRepository = new Repository<ShelfBind>();

            List<Shelf> shelfItems = shelvesRepository.GetAll().ToList();
            List<BookBind> bookBindItems = bookBindRepository.GetAll().ToList();
            List<ShelfBind> shelfBindItems = shelfBindRepository.GetAll().ToList();

            Items = new List<ShelfViewModel>();

            foreach (var item in shelfItems)
            {
                var books = new List<BookViewModel>() { };

                shelfBindItems = shelfBindRepository.GetAll()
                    .Where(o => o.Shelf.Id.Equals(item.Id))
                    .ToList();

                foreach (var shelfBind in shelfBindItems)
                {
                    books.Add(new BookViewModel
                    {
                        Title = shelfBind.Book.Title,
                        Author = bookBindItems.Where(o => o.Book.Id == shelfBind.Book.Id).Select(o => o.Author.FullName).First()
                    });
                }

                Items.Add(new ShelfViewModel(item.Name, books));
            }
        }
    }
}
