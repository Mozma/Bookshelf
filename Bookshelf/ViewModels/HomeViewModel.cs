using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Repositories;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<ShelfInfoSimple> shelvesItems;
        public ObservableCollection<ShelfInfoSimple> Shelves
        {
            get => shelvesItems;
            set
            {
                if (shelvesItems == value)
                {
                    return;
                }

                shelvesItems = value;

                OnPropertyChanged(nameof(shelvesItems));
            }
        }

        private ShelfInfoSimple selectedShelf;
        public ShelfInfoSimple SelectedShelf
        {
            get { return selectedShelf; }

            set
            {
                selectedShelf = value;
                OpenShelf();
                OnPropertyChanged(nameof(SelectedShelf));
            }
        }

        private ObservableCollection<BookInfoSimple> booksItems;
        public ObservableCollection<BookInfoSimple> Books
        {
            get => booksItems;
            set
            {
                if (booksItems == value)
                {
                    return;
                }

                booksItems = value;

                OnPropertyChanged(nameof(booksItems));
            }
        }

        private BookInfoSimple selectedBook;
        public BookInfoSimple SelectedBook
        {
            get { return selectedBook; }

            set
            {
                selectedBook = value;
                OpenBook();
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public SeriesCollection Series { get; set; }
        public HomeViewModel()
        {

            LoadView();
        }

        private void LoadView()
        {
            LoadSeries();
            LoadShelves();
            LoadBooks();
        }

        private void LoadBooks()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Books = new ObservableCollection<BookInfoSimple>(unitOfWork.Books.GetBooksSimpleInfo(12));
            }
        }
        private void OpenBook()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Navigation.SetView(new BookViewModel(unitOfWork.Books.Get(SelectedBook.Id), new Stores.BookStore()));
                selectedBook = null;
            }
        }

        private void OpenShelf()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Navigation.SetView(new ShelfViewModel(unitOfWork.Shelves.Get(SelectedShelf.Id), new Stores.ShelfStore()));
                selectedShelf = null;
            }
        }
        private void LoadShelves()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Shelves = new ObservableCollection<ShelfInfoSimple>(unitOfWork.Shelves.GetShelvesNamesAndAmountOfBooks(12));
            }
        }

        private void LoadSeries()
        {

            using (var unitOfWork = new UnitOfWork())
            {
                var list = unitOfWork.Books.GetStatusesInfo();

                var series = new SeriesCollection();

                foreach (var item in list)
                {
                    series.Add(new PieSeries
                    {
                        Title = item.Key == null ? "Без статуса" : ((BookStatus)item.Key).GetName(),
                        Values = new ChartValues<ObservableValue> { new ObservableValue(item.Count) },
                        DataLabels = item.Count > 0 ? true : false
                    });

                }
                Series = series;
            }
        }
    }
}
