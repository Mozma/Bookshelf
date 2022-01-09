using Bookshelf.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using System.Globalization;

namespace Bookshelf.Services
{
    public class GoogleBooks : IGoogleBooks
    {
        public static BooksService service = new BooksService(
           new BaseClientService.Initializer
           {
               ApplicationName = "Bookshelf",
               ApiKey = Credentials.ApiKey,
           });

        public async Task<BookInfo> FindBookByIsbnAsync(string isbn)
        {
            //string URL = "http://www.google.com/books/feeds/volumes/?q=ISBN%3C" + isbn + "%3E";

            var result = service.Volumes.List(isbn).Execute();

            if (result.Items == null)
            {
                return null;
            }
            string[] formats = new string[] { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy" };

            var book = new BookInfo
            {
                Title = result.Items[0].VolumeInfo.Title,
                PagesNumber = result.Items[0].VolumeInfo.PageCount,
                Publisher = result.Items[0].VolumeInfo.Publisher,
                Author = result.Items[0].VolumeInfo.Authors != null ? result.Items[0].VolumeInfo.Authors[0] : null,
                Year = result.Items[0].VolumeInfo.PublishedDate != null ?
                    DateTime.ParseExact(result.Items[0].VolumeInfo.PublishedDate, formats, CultureInfo.InvariantCulture).Year : null
            };

            return book;
        }
    }

}
