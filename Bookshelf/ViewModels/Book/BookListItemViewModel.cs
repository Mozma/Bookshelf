using Bookshelf.Helpers;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookListItemViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }
        public BitmapImage Cover { get; set; } = ResourceFinder.Get<BitmapImage>("DefaultBookCover");

    }
}
