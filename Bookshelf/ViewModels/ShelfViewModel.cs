using Bookshelf.Models;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        public Shelf Entity { get; set; }

        public ICommand OpenShelfCommand { get; set; }
        public ICommand AddNewBookCommand { get; set; }
        public ICommand GoToShelvesCommand { get; set; }

        public string Name { get; set; }
        public ObservableCollection<BookViewModel> Items { get; set; }

        public ShelfViewModel(Shelf entity)
        {
            Entity = entity;
            Name = entity.Name;

            if (Entity != null)
            {
                SetupView();
            }

            SetupCommands();
        }

        private void SetupView()
        {
            Items = new ObservableCollection<BookViewModel>();

            var shelvesRepository = new Repository<Shelf>();
            var shelfBindRepository = new Repository<ShelfBind>();

            List<Shelf> shelfItems = shelvesRepository.GetAll().ToList();
            List<ShelfBind> shelfBindItems = shelfBindRepository.GetAll().ToList();

            shelfBindItems = shelfBindRepository.GetAll()
                .Where(o => o.Shelf.Id.Equals(Entity.Id))
                .ToList();

            foreach (var shelfBind in shelfBindItems)
            {
                Items.Add(new BookViewModel(shelfBind.Book));
            }
        }

        private void SetupCommands()
        {
            OpenShelfCommand = new RelayCommand(o =>
            {
                Navigation.SetView(this);
            });

            GoToShelvesCommand = new RelayCommand(o =>
            {
                Navigation.SetView(new ShelvesViewModel());
            });

            AddNewBookCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new AddNewBookWindow());
                SetupView();
            });

        }

    }
}
