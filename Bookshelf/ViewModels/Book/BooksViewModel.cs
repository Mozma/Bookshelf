using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
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

            BookStore bookStore;

            var bookServise = new DataService<Book>(new DataContextFactory());

            var books = bookServise.GetAll().Result;

            foreach (var book in books)
            {
                bookStore = new BookStore();
                var bookview = new BookViewModel(book, bookStore);
                items.Add(bookview);
            }

            Items = items;
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

            LoadViewCommand = new RelayCommand(async o =>
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
        }

        private void UnbindEvents()
        {
            _bookStore.EntityCreated -= OnBookChanged;
            _bookStore.EntityChanged -= OnBookChanged;
        }

        public override void Dispose()
        {
            UnbindEvents();
            base.Dispose();
        }

    }
}
