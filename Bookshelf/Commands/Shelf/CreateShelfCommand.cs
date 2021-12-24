using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using Bookshelf.ViewModels;
using System.Linq;
using System.Windows;

namespace Bookshelf.Commands
{
    internal class CreateShelfCommand : CommandBase
    {
        private readonly AddShelfViewModel _viewModel;
        private readonly ShelfStore _shelfStore;

        public CreateShelfCommand(AddShelfViewModel viewModel, ShelfStore shelfStore)
        {
            _viewModel = viewModel;
            _shelfStore = shelfStore;
        }

        public override void Execute(object parameter)
        {
            if (Validate())
            {
                AddShelf();

                _viewModel.CloseCommand.Execute(this);
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

        private void AddShelf()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                // Для быстрой генерации большого количества полок

                //var rnd = new Random();
                //for (int i = 0; i < 100; i++)
                //{
                //    context.Set<Shelf>().Add(new Shelf
                //    {
                //        Name = rnd.Next().ToString()
                //    });
                //}

                //context.SaveChangesAsync();


                var shelf = new Shelf
                {
                    Name = _viewModel.ShelfName.Trim()
                };

                context.Set<Shelf>().Add(shelf);
                context.SaveChangesAsync();

                _shelfStore.CreateEntity(shelf);
            }
        }
    }
}
