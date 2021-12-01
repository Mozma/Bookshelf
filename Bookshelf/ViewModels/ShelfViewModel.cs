using Bookshelf.Models;
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
        public ICommand LoadViewCommand { get; set; }

        public string Name { get; set; }
        public ObservableCollection<BookViewModel> Items { get; set; }

        public ShelfViewModel(Shelf entity)
        {
            Entity = entity;
            Name = entity.Name;
            SetupCommands();

            if (Entity != null)
            {
                LoadViewCommand.Execute(this);
            }
        }

        private void LoadView()
        {

            Items = new ObservableCollection<BookViewModel>();

            List<ShelfBind> shelfBindItems = Entity.ShelfBinds;

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
                LoadView();
            });

            LoadViewCommand = new RelayCommand(o =>
           {
               LoadView();
           });

        }
    }
}
