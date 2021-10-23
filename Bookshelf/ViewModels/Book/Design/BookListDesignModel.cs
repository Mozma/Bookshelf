using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookListDesignModel : BookListViewModel
    {

        public static BookListDesignModel Instance => new BookListDesignModel();

        public BookListDesignModel()
        {
            Items = new List<BookListItemViewModel>
            {
                new BookListItemViewModel()
                {
                    Title = "Сомневайся во всём",
                    Author = "Рене Декарт",
                    Cover = new BitmapImage(new Uri(@"C:\Users\ilmoz\Source\Repos\Bookshelf\Bookshelf\Resources\Images\DesginTime\decart.png", UriKind.RelativeOrAbsolute))
                },
                new BookListItemViewModel()
                {
                    Title = "Рассуждения о методе",
                    Author = "Рене Декарт"
                },
                new BookListItemViewModel()
                {
                    Title = "Афоризмы житейской мудрости",
                    Author = "Артур Шопенгауэр"
                },
                new BookListItemViewModel()
                {
                    Title = "Мир как Воля и Предаставление",
                    Author = "Артур Шопенгауэр"
                },
                new BookListItemViewModel()
                {
                    Title = "Париж",
                    Author = "Эдвард Резерфорд",
                        Cover = new BitmapImage(new Uri(@"C:\Users\ilmoz\Source\Repos\Bookshelf\Bookshelf\Resources\Images\DesginTime\edvard.jpg", UriKind.RelativeOrAbsolute))
                }
            };

        }
    }
}
