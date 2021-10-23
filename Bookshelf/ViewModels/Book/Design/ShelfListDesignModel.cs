using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class ShelfListDesignModel : ShelfListViewModel
    {

        public static ShelfListDesignModel Instance => new ShelfListDesignModel();

        public ShelfListDesignModel()
        {
            Items = new List<ShelfListItemViewModel>()
            {
                new ShelfListItemViewModel()
                {
                    Name=  "Философия",
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
                            Title = "Мир как Воля и Представление",
                            Author = "Артур Шопенгауэр"
                        },
                        new BookListItemViewModel()
                        {
                            Title = "Париж",
                            Author = "Эдвард Резерфорд",
                                Cover = new BitmapImage(new Uri(@"C:\Users\ilmoz\Source\Repos\Bookshelf\Bookshelf\Resources\Images\DesginTime\edvard.jpg", UriKind.RelativeOrAbsolute))
                        }
                    }
                },

                new ShelfListItemViewModel()
                {
                    Name=  "IT",
                    Items = new List<BookListItemViewModel>
                    {
                        new BookListItemViewModel()
                        {
                            Title = "Чистый код",
                            Author = "Роберт Мартин"
                        },
                        new BookListItemViewModel()
                        {
                            Title = "CLR via C#",
                            Author = "Джеффри Рихтер"
                        },
                        new BookListItemViewModel()
                        {
                            Title = "Совершенный код",
                            Author = "Стив Макконнелл"
                        }
                    }
                },
                                new ShelfListItemViewModel()
                {
                    Name=  "Искусство",
                    Items = new List<BookListItemViewModel>
                    {
                        new BookListItemViewModel()
                        {
                            Title = "Суждения о науке и искусстве",
                            Author = "Леонардо да Винчи"
                        },
                        new BookListItemViewModel()
                        {
                            Title = "Орлиное гнездо",
                            Author = "Джон Рёскин"
                        }
                    }
                },

                new ShelfListItemViewModel()
                {
                    Name=  "Разное",
                    Items = new List<BookListItemViewModel>()
                },

                new ShelfListItemViewModel()
                {
                    Name=  "Философия",
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
                            Title = "Мир как Воля и Представление",
                            Author = "Артур Шопенгауэр"
                        },
                        new BookListItemViewModel()
                        {
                            Title = "Париж",
                            Author = "Эдвард Резерфорд",
                                Cover = new BitmapImage(new Uri(@"C:\Users\ilmoz\Source\Repos\Bookshelf\Bookshelf\Resources\Images\DesginTime\edvard.jpg", UriKind.RelativeOrAbsolute))
                        }
                    }
                },
                 new ShelfListItemViewModel()
                {
                    Name=  "Философия",
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
                            Title = "Мир как Воля и Представление",
                            Author = "Артур Шопенгауэр"
                        },
                        new BookListItemViewModel()
                        {
                            Title = "Париж",
                            Author = "Эдвард Резерфорд",
                                Cover = new BitmapImage(new Uri(@"C:\Users\ilmoz\Source\Repos\Bookshelf\Bookshelf\Resources\Images\DesginTime\edvard.jpg", UriKind.RelativeOrAbsolute))
                        }
                    }
                }
            };

        }
    }
}
