using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;

namespace Bookshelf.Services
{
    internal class GoogleBookService : IGoogleBookService
    {
        public static BooksService service = new BooksService(
               new BaseClientService.Initializer
               {
                    ApplicationName = "Bookshelf",
                    ApiKey = Credentials.ApiKey,
                });

        public Volumes FindBook(string isbn)
        {
            return service.Volumes.List(isbn).Execute();
        }
    }
}