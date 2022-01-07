using Bookshelf.Commands;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Repositories;
using Bookshelf.Stores;
using System.Collections.ObjectModel;
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
        public ICommand AddBookByIsbnCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public string Name { get; set; }

        private BookStore _bookStore;

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
        public bool IsOpen { get; set; }

        public ShelfViewModel(Shelf shelf, ShelfStore shelfStore)
        {
            _bookStore = new BookStore();
            Entity = shelf;
            _shelfStore = shelfStore;

            SetupCommands();
            LoadView();

            BindEvents();
        }

        private void LoadView()
        {
            Name = Entity.Name;


            var items = new ObservableCollection<BookViewModel>();


            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                var books = unitOfWork.Shelves.GetBooks(ShelfId);

                foreach (var book in books)
                {
                    var bookview = new BookViewModel(book, _bookStore);
                    items.Add(bookview);
                }
            }

            Items = items;
            IsOpen = Items.Count > 0 ? true : false;
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
                Navigation.SetCurrentOverlayViewModel(new AddBookViewModel(this, _bookStore));
            });

            LoadViewCommand = new RelayCommand(o =>
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

            AddBookByIsbnCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new AddBookViewModel(this, _bookStore));
            });

            DeleteCommand = new DeleteShelfCommand(this, _shelfStore);
        }

        private void ClearShelf()
        {
            Items.Clear();

            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                unitOfWork.Shelves.RemoveBooksFromShelf(ShelfId);
            }
        }


        private void OnBookChanged(Book obj)
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Entity = unitOfWork.Shelves.Get(Entity.Id);
            }

            LoadView();
        }

        private void BindEvents()
        {
            _shelfStore.EntityChanged += OnShelfViewModelChanged;
            _bookStore.EntityCreated += OnBookChanged;
            _bookStore.EntityDeleted += OnBookChanged;
        }

        private void UnbindEvents()
        {
            _shelfStore.EntityChanged -= OnShelfViewModelChanged;
            _bookStore.EntityCreated -= OnBookChanged;
            _bookStore.EntityDeleted -= OnBookChanged;
        }

        public override void Dispose()
        {
            UnbindEvents();
            base.Dispose();
        }
    }
}
