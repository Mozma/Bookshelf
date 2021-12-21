using Bookshelf.Models;
using Bookshelf.Models.Data;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class AddShelfViewModel : BaseViewModel
    {
        public string ShelfName { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand AddShelfCommand { get; set; }

        public AddShelfViewModel()
        {
            CloseCommand = new RelayCommand(o =>
            {
                Navigation.RemoveOverlay();
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
                var shelf = context.Set<Shelf>().FirstOrDefault(s => s.Name == ShelfName);

                //var rnd = new Random();
                //for (int i = 0; i < 100; i++)
                //{
                //    context.Set<Shelf>().Add(new Shelf
                //    {
                //        Name = rnd.Next().ToString()
                //    });
                //}

                //context.SaveChangesAsync();


                if (shelf == null)
                {
                    context.Set<Shelf>().Add(new Shelf
                    {
                        Name = ShelfName
                    });
                    context.SaveChangesAsync();
                }
            }

            CloseCommand.Execute(this);
        }
    }

}
