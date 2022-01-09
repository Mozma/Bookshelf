using Bookshelf.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using System.Globalization;

namespace Bookshelf.Services
{
    public class GoogleBooks : IGoogleBooks
    {
        private IGoogleBookService _googleBookService { get; }

        public GoogleBooks()
        {
            _googleBookService = new GoogleBookService();
        }

        public GoogleBooks(IGoogleBookService googleBookService)
        {
            _googleBookService = googleBookService;
        }

        public BookInfo FindBookByIsbn(string isbn)
        {
            var result = _googleBookService.FindBook(isbn);

            if (result.Items == null)
            {
                return null;
            }

            string[] formats = new string[] { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "yyyy" };

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
