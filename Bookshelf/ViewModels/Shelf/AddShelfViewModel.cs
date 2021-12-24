using Bookshelf.Commands;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class AddShelfViewModel : BaseViewModel
    {
        public string ShelfName { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddShelfCommand { get; set; }

        public List<string> Shelves { get; set; }

        public AddShelfViewModel(ShelfStore shelfStore)
        {
            CloseCommand = new RelayCommand(o =>
            {
                Navigation.RemoveOverlay();
            });

            AddShelfCommand = new CreateShelfCommand(this, shelfStore);

            GetSuggestions();
        }


        private void GetSuggestions()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                Shelves = context.Set<Shelf>().Select(o => o.Name).ToList();
            }
        }

    }
}
