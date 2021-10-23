using System;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookListItemViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }
        public BitmapImage Cover { get; set; } = new BitmapImage(new Uri(@"C:\Users\ilmoz\Source\Repos\Bookshelf\Bookshelf\Resources\Images\DesginTime\default.png", UriKind.RelativeOrAbsolute));

    }
}
