using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

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
                            Title = item.key == null ? "Без статуса": ((BookStatus)item.key).ToString(),
                            Values = new ChartValues<ObservableValue> { new ObservableValue(item.count) },
                            DataLabels = item.count > 0 ? true : false
                        });
                    
                }

                Series = series;
            }


            


            //    Series = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Status1",
            //        Values = new ChartValues<ObservableValue>{ new ObservableValue(20) },
            //    },
            //    new PieSeries
            //    {
            //        Title = "Status2",
            //        Values = new ChartValues<ObservableValue>{ new ObservableValue(15) },
            //    },
            //    new PieSeries
            //    {
            //        Title = "Status3",
            //        Values = new ChartValues<ObservableValue>{ new ObservableValue(5) },
            //    },
            //    new PieSeries
            //    {
            //        Title = "Status4",
            //        Values = new ChartValues<ObservableValue>{ new ObservableValue(3) }
            //    },
            //    new PieSeries
            //    {
            //        Title = "Status5",
            //        Values = new ChartValues<ObservableValue>{ new ObservableValue(0) },
            //        DataLabels = false
            //    },
            //};


        }
    }
}
