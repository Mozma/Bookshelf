using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class ShelvesViewModel : BaseViewModel
    {
        public List<ShelfViewModel> Items { get; set; }

        public ShelvesViewModel()
        {
            SetupView();
        }

        public void SetupView()
        {
            var shelvesRepository = new Repository<Shelf>();
            var bookBindRepository = new Repository<BookBind>();
            var shelfBindRepository = new Repository<ShelfBind>();
            var imageRepository = new Repository<Models.Image>();

            List<Shelf> shelfItems = shelvesRepository.GetAll().ToList();
            List<BookBind> bookBindItems = bookBindRepository.GetAll().ToList();
            List<ShelfBind> shelfBindItems = shelfBindRepository.GetAll().ToList();

            Items = new List<ShelfViewModel>();

            foreach (var item in shelfItems)
            {
                var books = new List<BookViewModel>() { };

                shelfBindItems = shelfBindRepository.GetAll()
                    .Where(o => o.Shelf.Id.Equals(item.Id))
                    .ToList();

                foreach (var shelfBind in shelfBindItems)
                {
                    Bitmap bitmapImage;

                    if (shelfBind.Book.BookImageId != null)
                    {

                        bitmapImage = imageRepository.Get(shelfBind.Book.BookImageId).Base64Data.Base64StringToBitmap();

                    }
                    else
                    {
                        bitmapImage = ResourceFinder.Get<BitmapImage>("DefaultBookCover").BitmapImageToBitmap();
                    }


                    books.Add(new BookViewModel
                    {
                        Title = shelfBind.Book.Title,
                        Author = bookBindItems.Where(o => o.Book.Id == shelfBind.Book.Id).Select(o => o.Author.FullName).First(),
                        Cover = bitmapImage
                    });
                }

                Items.Add(new ShelfViewModel(item.Name, books));
            }
        }
    }
}
