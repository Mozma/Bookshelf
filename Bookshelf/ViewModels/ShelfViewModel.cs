using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        public Shelf Entity { get; set; }
        public int ShelfId { get; }

        public ICommand OpenShelfCommand { get; set; }
        public ICommand AddNewBookCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }

        public string Name { get; set; }
        public List<BookViewModel> Items { get; set; }

        public ShelfViewModel(int  shelfId)
        {
            SetupCommands();

            ShelfId = shelfId;
            LoadViewCommand.Execute(this);
        }

        private void LoadView()
        {
            var shelfService = new ShelfService(new DataContextFactory());
            Entity = shelfService.GetById(ShelfId).Result;
            
            Name = Entity.Name;

            var items = new List<BookViewModel>();

            List<ShelfBind> shelfBindItems = Entity.ShelfBinds;

            foreach (var shelfBind in shelfBindItems)
            {
                items.Add(new BookViewModel(shelfBind.Book));
            }

            Items = items;
        }

        private void SetupCommands()
        {
            OpenShelfCommand = new RelayCommand(o =>
            {
                Navigation.SetView(this);
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
            });

            AddNewBookCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new AddNewBookWindow());
                LoadViewCommand.Execute(this);
            });

            LoadViewCommand = new RelayCommand(async o =>
            {
                LoadView();
            });

        }
    }
}
