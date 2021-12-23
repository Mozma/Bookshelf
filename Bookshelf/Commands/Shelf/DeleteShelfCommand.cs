using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Bookshelf.Stores;
using Bookshelf.ViewModels;

namespace Bookshelf.Commands
{
    public class DeleteShelfCommand : CommandBase
    {
        private readonly ShelfViewModel _viewModel;
        private readonly ShelfStore _shelfStore;

        public DeleteShelfCommand(ShelfViewModel viewModel, ShelfStore shelfStore)
        {
            _viewModel = viewModel;
            _shelfStore = shelfStore;
        }

        public override void Execute(object parameter)
        {
            DeleteShelf();
        }

        private void DeleteShelf()
        {
            var shelfService = new DataService<Shelf>(new DataContextFactory());

            shelfService.Delete(_viewModel.Entity.Id);

            _shelfStore.DeleteEntity(_viewModel.Entity);
        }
    }
}
