
using Bookshelf.Models.Interfaces;
using Bookshelf.Services;
using Bookshelf.Models;
using System.Collections.Generic;
using System.Linq;
using Bookshelf.Navigation;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        private NavigationStore navigationStore;

        public string Name { get; set; }
        public List<BookListItemViewModel> Items { get; set; }

        public ShelfViewModel()
        {

        }

        public ShelfViewModel(string name,List<BookListItemViewModel> items)
        {
            Name = name;
            Items = items;
        }

        public ShelfViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
        }
    }
}
