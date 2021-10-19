using System.Collections.Generic;


namespace Bookshelf.ViewModels.Book
{
    public class BookListViewModel : BaseViewModel
    {
        public int Count => Items.Count;
        public List<BookListItemViewModel> Items { get; set; }

    }
}
