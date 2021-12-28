using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Linq;

namespace Bookshelf.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public SeriesCollection Series { get; set; }

        public HomeViewModel()
        {
            LoadSeries();
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
