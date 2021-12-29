using Bookshelf.Models.Data;
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
            if (ConfirmDelete())
            {
                DeleteShelf();
            }
        }

        private bool ConfirmDelete()
        {
            //TODO: Вызвть диалог для подтверждения удавления

            return true;
        }

        private void DeleteShelf()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                unitOfWork.Shelves.Remove(_viewModel.Entity);
                unitOfWork.Complete();

                _shelfStore.DeleteEntity(_viewModel.Entity);
            }
        }
    }
}
