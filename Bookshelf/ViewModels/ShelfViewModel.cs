using Bookshelf.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        public Shelf Entity { get; set; }

        public ICommand OpenShelfCommand { get; set; }
        public ICommand AddNewBookCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }

        public string Name { get; set; }

        private ObservableCollection<BookViewModel> items;
        public ObservableCollection<BookViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

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

            //var shelfBindService = new DataService<ShelfBind>(new Models.Data.DataContextFactory());

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

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
            });

            AddNewBookCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new AddNewBookWindow());
                LoadViewCommand.Execute(this);
            });

            LoadViewCommand = new RelayCommand(async o =>
            {
                LoadView();
            });

        }
    }
}
