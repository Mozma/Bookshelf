using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using Bookshelf.ViewModels;
using System.Linq;
using System.Windows;

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
            if (Validate())
            {
                SaveEntity();

                _viewModel.Close();
            }
        }

        private bool Validate()
        {
            //TODO: Добавить диалог

            if (string.IsNullOrWhiteSpace(_viewModel.ShelfName))
            {
                MessageBox.Show("Имя не может быть пустым");
                return false;
            }

            using (var context = new DataContextFactory().CreateDbContext())
            {
                var shelf = context.Set<Shelf>().FirstOrDefault(s => s.Name == _viewModel.ShelfName.Trim());

                if (shelf != null)
                {
                    MessageBox.Show("Полка с таким названием уже существует");
                    return false;
                }
            }

            return true;
        }

        private void SaveEntity()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                _shelf.Name = _viewModel.ShelfName;

                context.Entry(_shelf).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChangesAsync();
            }

            _shelfStore.ChangeEntity(_shelf);
        }
    }
}
