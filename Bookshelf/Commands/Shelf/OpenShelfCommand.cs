using Bookshelf.Models.Data;
using Bookshelf.ViewModels;

namespace Bookshelf.Commands
{
    internal class OpenShelfCommand : CommandBase
    {
        private int _shelfId;
        public OpenShelfCommand()
        {
        }

        public override void Execute(object parameter)
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Navigation.SetView(new ShelfViewModel(unitOfWork.Shelves.Get(_shelfId), new Stores.ShelfStore()));
            }
        }

        public void Execute(int shelfId)
        {
            _shelfId = shelfId;
            Execute(null);
        }
    }
}
