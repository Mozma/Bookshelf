using Bookshelf.ViewModels.Book;
using System;

namespace Bookshelf.ViewModels 
{ 
    public class BookListItemDesignModel : BookListItemViewModel
    {

        public static BookListItemDesignModel Instance => new BookListItemDesignModel();

        public BookListItemDesignModel()
        {
            Title = "Сомневайся во всём";
            Author = "Рене Декарт";
            Cover = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"C:\Users\ilmoz\Source\Repos\Bookshelf\Bookshelf\Resources\Images\DesginTime\decart.png", UriKind.RelativeOrAbsolute));
        }

    }
}
