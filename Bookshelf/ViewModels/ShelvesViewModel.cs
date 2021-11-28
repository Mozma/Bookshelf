using Bookshelf.Models;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelvesViewModel : BaseViewModel
    {
        public List<ShelfViewModel> Items { get; set; }

        public ICommand AddShelfCommand { get; set; }

        public ShelvesViewModel()
        {
            SetupView();
            SetupCommands();
        }

        private void SetupCommands()
        {
            AddShelfCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new AddShelfWindow());
                Navigation.SetView(this);
            });
        }

        public void SetupView()
        {
            var shelvesRepository = new Repository<Shelf>();
            var shelfBindRepository = new Repository<ShelfBind>();

            List<Shelf> shelfItems = shelvesRepository.GetAll().ToList();
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
                    books.Add(new BookViewModel(shelfBind.Book));
                }

                Items.Add(new ShelfViewModel(item.Name, books));
            }
        }
    }
}
