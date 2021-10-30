using Bookshelf.Navigation;

namespace Bookshelf.ViewModels
{
    public class ShelfListDesignModel : ShelvesViewModel
    {
        /*
                public static ShelfListDesignModel Instance => new ShelfListDesignModel();

                public ShelfListDesignModel()
                {
                    Items = new List<ShelfViewModel>()
                    {
                        new ShelfViewModel()
                        {
                            re
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

                        new ShelfViewModel()
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
                                        new ShelfViewModel()
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

                        new ShelfViewModel()
                        {
                            Name=  "Разное",
                            Items = new List<BookListItemViewModel>()
                        },

                        new ShelfViewModel()
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
                         new ShelfViewModel()
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
        */
        public ShelfListDesignModel(NavigationStore navigationStore) : base(navigationStore)
        {
        }
    }
}
