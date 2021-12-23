using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Bookshelf.Stores;
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
            get => items;
            set
            {
                if (items == value)
                {
                    return;
                }

                items = value;

                OnPropertyChanged(nameof(items));
            }
        }

        public ICommand AddShelfCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }
        public ICommand GoBackCommand { get; set; }


        private readonly ShelfStore _shelfStore;
        public ShelvesViewModel()
        {
            _shelfStore = new ShelfStore();

            SetupCommands();
            BindEvents();

            LoadViewCommand.Execute(this);
        }



        private void SetupCommands()
        {
            AddShelfCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new AddShelfViewModel(_shelfStore));
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
                var shelfViewModel = new ShelfViewModel(item, _shelfStore);

                Items.Add(shelfViewModel);
            }
        }


        private void BindEvents()
        {
            _shelfStore.ShelfCreated += OnShelfCreated;
            _shelfStore.ShelfDeleted += OnShelfDeleted;
            _shelfStore.ShelfChanged += OnShelfChanged;
        }
        private void UnbindEvents()
        {
            _shelfStore.ShelfCreated -= OnShelfCreated;
            _shelfStore.ShelfDeleted -= OnShelfDeleted;
            _shelfStore.ShelfChanged -= OnShelfChanged;
        }

        private void OnViewModelChanged()
        {
            LoadViewCommand.Execute(this);
        }

        private void OnShelfCreated(Shelf shelf)
        {
            OnViewModelChanged();
        }

        private void OnShelfChanged(Shelf obj)
        {
            OnViewModelChanged();
        }

        private void OnShelfDeleted(Shelf obj)
        {
            OnViewModelChanged();
        }
        public override void Dispose()
        {
            UnbindEvents();
            base.Dispose();
        }
    }
}
