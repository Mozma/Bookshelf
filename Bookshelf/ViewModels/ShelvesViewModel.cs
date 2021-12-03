using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelvesViewModel : BaseViewModel
    {
        private ObservableCollection<ShelfViewModel> items;
        public ObservableCollection<ShelfViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand AddShelfCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public ShelvesViewModel()
        {
            SetupCommands();

            LoadViewCommand.Execute(this);
        }

        private void SetupCommands()
        {
            AddShelfCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new AddShelfWindow());
                LoadViewCommand.Execute(this);
            });

            LoadViewCommand = new RelayCommand(async o =>
            {
                SetupView();
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
            });
        }

        public void SetupView()
        {
            Items = new ObservableCollection<ShelfViewModel>();

            IDataService<Shelf> shelfService = new DataService<Shelf>(new DataContextFactory());

            List<Shelf> shelfItems = shelfService.GetAll().Result.ToList();

            foreach (var item in shelfItems)
            {
                Items.Add(new ShelfViewModel(item.Id));
            }
        }
    }
}
