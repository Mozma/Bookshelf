using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        private ObservableCollection<ShelfInfoSimple> shelvesItems;
        public ObservableCollection<ShelfInfoSimple> ShelvesItems
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

        public ICommand OpenShelfCommand { get; set; }

        public SeriesCollection Series { get; set; }
        public HomeViewModel()
        {

            LoadView();
            SetupCommadns();
        }

        private void LoadView()
        {
            LoadSeries();
            LoadShelves();
        }

        private void SetupCommadns()
        {
            //S

            //OpenShelfCommand = new RelayCommand(o =>
            //{
            //    using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            //    {
            //        Navigation.SetView(new ShelfViewModel(unitOfWork.Shelves.Get(SelectedShelf.Id), new Stores.ShelfStore()));
            //    }
            //});
        }


        private void OpenShelf()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Navigation.SetView(new ShelfViewModel(unitOfWork.Shelves.Get(SelectedShelf.Id), new Stores.ShelfStore()));
            }
        }
        private void LoadShelves()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                ShelvesItems = new ObservableCollection<ShelfInfoSimple>(unitOfWork.Shelves.GetShelvesNamesAndAmountOfBooks(12));
            }
        }

        private void LoadSeries()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {

                var query = from book in context.Set<Book>().ToList()
                            group book by book.Status into g
                            select new
                            {
                                key = g.Key,
                                count = g.Count()
                            };
                query = query.OrderByDescending(o => o.count);

                var series = new SeriesCollection();

                foreach (var item in query)
                {


                    series.Add(new PieSeries
                    {
                        Title = item.key == null ? "Без статуса" : ((BookStatus)item.key).ToString(),
                        Values = new ChartValues<ObservableValue> { new ObservableValue(item.count) },
                        DataLabels = item.count > 0 ? true : false
                    });

                }

                Series = series;
            }
        }
    }
}
