using Bookshelf.Dialogs;
using System.Collections.Generic;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        public ICommand OpenShelfCommand { get; set; }
        public ICommand AddNewBookCommand { get; set; }
        public ICommand GoToShelvesCommand { get; set; }

        public string Name { get; set; }
        public List<BookListItemViewModel> Items { get; set; }

        public ShelfViewModel(string name, List<BookListItemViewModel> items) : this()
        {
            Name = name;
            Items = items;
        }

        public ShelfViewModel()
        {
            SetupCommands();
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
                IoC.UI.ShowMessage(new MessageBoxDialogViewModel() { Title = "Заголовко диалога" });
            });

        }

    }
}
