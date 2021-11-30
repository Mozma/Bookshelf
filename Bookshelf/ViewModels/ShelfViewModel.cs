using Bookshelf.Models;
using Bookshelf.Models.Data;
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

            using (var context = new DataContextFactory().CreateDbContext())
            {
                List<ShelfBind> shelfBindItems = context.Set<ShelfBind>().ToList();

                shelfBindItems = context.Set<ShelfBind>()
                    .Where(o => o.Shelf.Id.Equals(Entity.Id))
                    .ToList();

                foreach (var shelfBind in shelfBindItems)
                {
                    Items.Add(new BookViewModel(shelfBind.Book));
                }
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
