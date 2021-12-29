using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {

        public ICommand AddBookCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }

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

        public BooksViewModel()
        {
            _bookStore = new BookStore();

            SetupCommands();
            LoadView();

            BindEvents();
        }

        private void LoadView()
        {
            var items = new ObservableCollection<BookViewModel>();

            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                var books = unitOfWork.Books.GetAll();

                foreach (var book in books)
                {
                    var bookview = new BookViewModel(book, _bookStore);
                    items.Add(bookview);
                }

                Items = items;
            }
        }

        private void SetupCommands()
        {
            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
            });

            AddBookCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new AddBookViewModel(_bookStore));
            });

            LoadViewCommand = new RelayCommand(o =>
            {
                LoadView();
            });
        }

        private void OnBookChanged(Book obj)
        {
            LoadView();
        }

        private void BindEvents()
        {
            _bookStore.EntityCreated += OnBookChanged;
            _bookStore.EntityChanged += OnBookChanged;
            _bookStore.EntityDeleted += OnBookChanged;

        }

        private void UnbindEvents()
        {
            _bookStore.EntityCreated -= OnBookChanged;
            _bookStore.EntityChanged -= OnBookChanged;
            _bookStore.EntityDeleted -= OnBookChanged;
        }

        public override void Dispose()
        {
            UnbindEvents();
            base.Dispose();
        }

    }
}
