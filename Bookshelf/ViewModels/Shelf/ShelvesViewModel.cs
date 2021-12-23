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
        private readonly BookStore _bookStore;
        public ShelvesViewModel()
        {
            _shelfStore = new ShelfStore();
            _bookStore = new BookStore();

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
                var shelfViewModel = new ShelfViewModel(item, _shelfStore, _bookStore);

                Items.Add(shelfViewModel);
            }
        }


        private void BindEvents()
        {
            _shelfStore.EntityCreated += OnShelfChanged;
            _shelfStore.EntityDeleted += OnShelfChanged;
            _shelfStore.EntityChanged += OnShelfChanged;

            //_bookStore.EntityCreated += OnBookChanged;
        }
        private void UnbindEvents()
        {
            _shelfStore.EntityCreated -= OnShelfChanged;
            _shelfStore.EntityDeleted -= OnShelfChanged;
            _shelfStore.EntityChanged -= OnShelfChanged;

            //     _bookStore.EntityCreated -= OnBookChanged;
        }


        private void OnViewModelChanged()
        {
            LoadViewCommand.Execute(this);
        }

        private void OnShelfChanged(Shelf obj)
        {
            OnViewModelChanged();
        }
        private void OnBookChanged(Book obj)
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
