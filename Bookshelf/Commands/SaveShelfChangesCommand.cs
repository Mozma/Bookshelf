using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using Bookshelf.ViewModels;

namespace Bookshelf.Commands
{
    public class SaveShelfChangesCommand : CommandBase
    {
        private readonly EditShelfViewModel _viewModel;
        private readonly ShelfStore _shelfStore;
        private Shelf _shelf;

        public SaveShelfChangesCommand(EditShelfViewModel viewModel, ShelfStore shelfStore, Shelf shelf)
        {
            _viewModel = viewModel;
            _shelfStore = shelfStore;
            _shelf = shelf;
        }

        public override void Execute(object parameter)
        {
            SaveEntity();
        }

        private void SaveEntity()
        {

            if (!string.IsNullOrWhiteSpace(_viewModel.ShelfName) && !_viewModel.ShelfName.Equals(_viewModel.CurrentShelfName))
            {
                using (var context = new DataContextFactory().CreateDbContext())
                {
                    _shelf.Name = _viewModel.ShelfName;

                    context.Entry(_shelf).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChangesAsync();
                }

                _shelfStore.ChangeShelf(_shelf);
                _viewModel.Close();
            }
        }
    }
}
