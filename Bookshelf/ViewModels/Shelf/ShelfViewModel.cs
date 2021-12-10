using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Bookshelf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ICommand EditCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public string Name { get; set; }
        public List<BookViewModel> Items { get; set; }

        public ShelfViewModel(int shelfId)
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
                var bookview = new BookViewModel(shelfBind.Book);

                bookview.BookViewModelChanged += OnShelfViewModelChanged;
                bookview.BookViewModelChanged += () => LoadViewCommand.Execute(this);

                items.Add(bookview);
            }

            Items = items;
        }
        private void OnShelfViewModelChanged()
        {
            ShelfViewModelChanged?.Invoke();
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

            EditCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new EditShelfWindow(this));
                LoadViewCommand.Execute(this);
                OnShelfViewModelChanged();
            });

            ClearCommand = new RelayCommand(async o =>
            {
                ClearShelf();
                OnShelfViewModelChanged();
            });

            DeleteCommand = new RelayCommand(o =>
            {
                DeleteShelf();
                OnShelfViewModelChanged();
            });
        }

        private void ClearShelf()
        {
            var shelfBindService = new DataService<ShelfBind>(new DataContextFactory());

            var shelfBinds = shelfBindService.GetAll().Result.Where(s => s.ShelfId == Entity.Id);

            foreach (var item in shelfBinds)
            {
                shelfBindService.Delete(item.Id);
            }
        }

        private void DeleteShelf()
        {
            var shelfService = new DataService<Shelf>(new DataContextFactory());

            shelfService.Delete(Entity.Id);
        }

        public event Action ShelfViewModelChanged;
    }
}
