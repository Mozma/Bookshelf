
using Bookshelf.Models.Interfaces;
using Bookshelf.Services;
using Bookshelf.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bookshelf.ViewModels
{
    public class ShelfListItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public List<BookListItemViewModel> Items { get; set; }


        public ShelfListItemViewModel()
        {

        }
    }
}
