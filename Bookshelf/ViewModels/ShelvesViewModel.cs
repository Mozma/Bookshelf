using Bookshelf.Models;
using Bookshelf.Models.Data;
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
                OnPropertyChanged("Items");
            }
        }

        public ICommand AddShelfCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }


        public ShelvesViewModel()
        {
            //SetupView();
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
        }


        public void SetupView()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                List<Shelf> shelfItems = context.Set<Shelf>().ToList();

                Items = new ObservableCollection<ShelfViewModel>();

                foreach (var item in shelfItems)
                {
                    Items.Add(new ShelfViewModel(item));
                }
            }
        }
    }
}
