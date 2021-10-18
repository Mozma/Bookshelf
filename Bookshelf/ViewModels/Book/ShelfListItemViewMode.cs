
using System.Collections.Generic;

namespace Bookshelf.ViewModels.Book
{
    public class ShelfListItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public List<BookListItemViewModel> Items { get; set; }
    }
}
