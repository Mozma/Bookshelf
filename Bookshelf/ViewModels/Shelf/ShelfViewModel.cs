using Bookshelf.Commands;
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
    public class ShelfViewModel : BaseViewModel
    {
        private readonly ShelfStore _shelfStore;

        public Shelf Entity { get; set; }
        public int ShelfId => Entity.Id;

        public ICommand OpenShelfCommand { get; set; }
        public ICommand AddBookCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public string Name { get; set; }


        public ObservableCollection<BookViewModel> items;
        public ObservableCollection<BookViewModel> Items
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
        public bool IsOpen { get; set; } = false;

        public ShelfViewModel(Shelf shelf, ShelfStore shelfStore)
        {
            Entity = shelf;
            _shelfStore = shelfStore;

            SetupCommands();
            LoadView();

            _shelfStore.ShelfChanged += OnShelfViewModelChanged;
        }

        private async void LoadView()
        {
            Name = Entity.Name;

            var items = new ObservableCollection<BookViewModel>();

            List<ShelfBind> shelfBindItems = Entity.ShelfBinds;

            foreach (var shelfBind in shelfBindItems)
            {
                var bookview = new BookViewModel(shelfBind.Book);
                items.Add(bookview);
            }

            Items = items;
        }
        private void OnShelfViewModelChanged(Shelf shelf)
        {
            Entity = shelf;
            Name = shelf.Name;
        }

        private void SetupCommands()
        {
            OpenShelfCommand = new RelayCommand(o =>
            {
                Navigation.SetView(this);
                LoadView();
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
            });

            AddBookCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new AddBookViewModel(this));
            });

            LoadViewCommand = new RelayCommand(async o =>
            {
                LoadView();
            });

            EditCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new EditShelfViewModel(this, _shelfStore));
            });

            ClearCommand = new RelayCommand(o =>
            {
                ClearShelf();
            });

            DeleteCommand = new DeleteShelfCommand(this, _shelfStore);
        }

        private void ClearShelf()
        {
            Items.Clear();

            var shelfBindService = new DataService<ShelfBind>(new DataContextFactory());

            var shelfBinds = shelfBindService.GetAll().Result.Where(s => s.ShelfId == Entity.Id);

            foreach (var item in shelfBinds)
            {
                shelfBindService.Delete(item.Id);
            }
        }

        public override void Dispose()
        {
            _shelfStore.ShelfChanged -= OnShelfViewModelChanged;
            base.Dispose();
        }
    }
}
