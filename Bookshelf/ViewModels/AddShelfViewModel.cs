using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class AddShelfViewModel : BaseViewModel
    {
        public string ShelfName { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand AddShelfCommand { get; set; }

        public AddShelfViewModel(Window window)
        {
            CloseCommand = new RelayCommand(o =>
            {
                window.Close();
            });

            AddShelfCommand = new RelayCommand(o =>
            {
                if (!string.IsNullOrWhiteSpace(ShelfName))
                {
                    AddShelf();
                }
            });

        }

        private void AddShelf()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                var shelfRepository = new Repository<Shelf>(context);

                var shelf = shelfRepository.GetAll().FirstOrDefault(s => s.Name == ShelfName);

                if (shelf == null)
                {
                    shelfRepository.Create(new Shelf
                    {
                        Name = ShelfName
                    });
                }
            }


            CloseCommand.Execute(this);
        }
    }

}
