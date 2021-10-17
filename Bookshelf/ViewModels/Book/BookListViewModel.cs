using System.Collections.Generic;


namespace Bookshelf.ViewModels.Book
{
    public class BookListViewModel : BaseViewModel
    {
        public List<BookListItemViewModel> Items { get; set; }

    }
}
