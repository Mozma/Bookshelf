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
        public bool IsOpen { get; set; } = false;

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

            List<ShelfBind> shelfBindItems = Entity.ShelfBinds;

            BookStore bookStore;

            foreach (var shelfBind in shelfBindItems)
            {
                bookStore = new BookStore();
                var bookview = new BookViewModel(shelfBind.Book, bookStore);
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
                Navigation.SetCurrentOverlayViewModel(new AddBookViewModel(this, _bookStore));
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


        private void OnBookChanged(Book obj)
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                Entity = context.Set<Shelf>()
                                .Where(s => s.Id == Entity.Id)
                                .FirstOrDefault();
            }
            LoadView();
        }

        private void BindEvents()
        {
            _shelfStore.EntityChanged += OnShelfViewModelChanged;
            _bookStore.EntityCreated += OnBookChanged;
        }


        private void UnbindEvents()
        {
            _shelfStore.EntityChanged -= OnShelfViewModelChanged;
            _bookStore.EntityCreated -= OnBookChanged;
        }


        public override void Dispose()
        {
            UnbindEvents();
            base.Dispose();
        }

    }
}
