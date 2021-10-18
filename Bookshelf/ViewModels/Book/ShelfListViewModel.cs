
using System.Collections.Generic;

namespace Bookshelf.ViewModels.Book
{
    public class ShelfListViewModel : BaseViewModel
    {
        public List<ShelfListItemViewModel> Items { get; set; }
    }
}
