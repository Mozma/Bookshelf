using Bookshelf.Models;
using Bookshelf.Navigation;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelvesViewModel : BaseViewModel
    {
        public List<ShelfViewModel> Items { get; set; }


        private NavigationStore navigationStore;
        public ICommand OpenShelfCommand { get; set; }

        public ShelvesViewModel(NavigationStore navigationStore) 
        {
            SetupView();

            this.navigationStore = navigationStore;
            
            OpenShelfCommand = new RelayCommand(o =>
            {
                this.navigationStore.CurrentViewModel = new ShelfViewModel(navigationStore);
            });
        }

        public void SetupView()
        {
            var bookRepository = new Repository<Book>();
            var shelvesRepository = new Repository<Shelf>();
            var bookBindRepository = new Repository<BookBind>();

            List<Book> bookItems = bookRepository.GetAll().ToList();
            List<Shelf> shelfItems = shelvesRepository.GetAll().ToList();

            Items = new List<ShelfViewModel>();

            foreach (var item in shelfItems)
            {
                var books = new List<BookListItemViewModel>() { };

                List<BookBind> bookBindItems = bookBindRepository.GetAll()
                    .Where(o => o.Shelf.Id == item.Id)
                    .ToList();

                foreach (var bookBind in bookBindItems)
                {
                    books.Add(new BookListItemViewModel()
                    {
                        Title = bookBind.Book.Title,
                        Author = bookBind.Author.FullName
                    });
                }

                Items.Add(
                    new ShelfViewModel()
                    {
                        Name = item.Name,
                        Items = books
                    });
            }
        }
    }
}
