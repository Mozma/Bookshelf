using System.Collections.Generic;


namespace Bookshelf.ViewModels
{
    public class BookListViewModel : BaseViewModel
    {
        public int Count => Items.Count;
        public List<BookViewModel> Items { get; set; }

        public BookListViewModel()
        {

        }

    }
}
