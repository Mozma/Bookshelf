using Bookshelf.Navigation;
using System.Collections.Generic;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        private NavigationStore navigationStore;

        public ICommand OpenShelfCommand { get; set; }
        public ICommand AddNewBookCommand { get; set; }
        public ICommand GoToShelvesCommand { get; set; }

        public string Name { get; set; }
        public List<BookListItemViewModel> Items { get; set; }


        public ShelfViewModel(NavigationStore navigationStore, string name, List<BookListItemViewModel> items)
            : this(navigationStore)
        {
            Name = name;
            Items = items;
        }

        public ShelfViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;

            SetupCommands();
        }

        private void SetupCommands() 
        {
            OpenShelfCommand = new RelayCommand(o =>
            {
                this.navigationStore.CurrentViewModel = this;

            });

            GoToShelvesCommand = new RelayCommand(o =>
            {
                this.navigationStore.CurrentViewModel = new ShelvesViewModel(navigationStore);

            });
        }

    }
}
