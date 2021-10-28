
using Bookshelf.Models.Interfaces;
using Bookshelf.Services;
using Bookshelf.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        public string Name { get; set; } = "Полка";
        public List<BookListItemViewModel> Items { get; set; }


        public ShelfViewModel()
        {

        }
    }
}
