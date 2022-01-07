using Bookshelf.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;

namespace Bookshelf.Services
{



    public class GoogleBooks : IGoogleBooks
    {
        public static BooksService service = new BooksService(
       new BaseClientService.Initializer
       {
           ApplicationName = "Bookshelf",
           ApiKey = "AIzaSyAjzdI_t0rM8XGlH0vc8_Oj5mZ4LDKPauA",
       });

        public async Task<BookInfo> FindBookByIsbnAsync(string isbn)
        {
            //string URL = "http://www.google.com/books/feeds/volumes/?q=ISBN%3C" + isbn + "%3E";

            var result = service.Volumes.List(isbn).Execute();

            var book = new BookInfo
            {
                Title = result.Items[0].VolumeInfo.Title,
                PagesNumber = result.Items[0].VolumeInfo.PrintedPageCount,
                Publisher = result.Items[0].VolumeInfo.Publisher,
                Author = result.Items[0].VolumeInfo.Authors[0],
                Year = Convert.ToDateTime(result.Items[0].VolumeInfo.PublishedDate).Year,
            };

            return book;
        }
    }

}
