using Bookshelf.Commands;
using Bookshelf.Stores;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class AddShelfViewModel : BaseViewModel
    {
        public string ShelfName { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand AddShelfCommand { get; set; }

        public AddShelfViewModel(ShelfStore shelfStore)
        {
            CloseCommand = new RelayCommand(o =>
            {
                Navigation.RemoveOverlay();
            });

            AddShelfCommand = new CreateShelfCommand(this, shelfStore);
        }

    }

}
