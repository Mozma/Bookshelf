using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        public Shelf Entity { get; set; }
        public int ShelfId { get; }

        public ICommand OpenShelfCommand { get; set; }
        public ICommand AddBookCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public string Name { get; set; }


        public ObservableCollection<BookViewModel> items;
        public ObservableCollection<BookViewModel> Items
        {
            get => items;
            set
            {
                if (items == value)
                {
                    return;
                }

                items = value;

                OnPropertyChanged(nameof(items));
            }
        }
        public bool IsOpen { get; set; } = false;

        public ShelfViewModel(Shelf shelf)
        {
            SetupCommands();

            ShelfId = shelf.Id;

            Navigation.Instance.CurrentOverlayRemoved += OnShelfViewModelChanged;

            LoadView();
        }

        private async void LoadView()
        {

            var shelfService = new ShelfService(new DataContextFactory());
            Entity = shelfService.GetById(ShelfId).Result;
            Name = Entity.Name;

            await Task.Run(() =>
            {
                var items = new ObservableCollection<BookViewModel>();

                List<ShelfBind> shelfBindItems = Entity.ShelfBinds;

                foreach (var shelfBind in shelfBindItems)
                {
                    var bookview = new BookViewModel(shelfBind.Book);
                    items.Add(bookview);
                }

                Items = items;
            });
        }
        private void OnShelfViewModelChanged()
        {
            ShelfViewModelDeleted?.Invoke();
        }

        private void SetupCommands()
        {
            OpenShelfCommand = new RelayCommand(o =>
            {
                Navigation.SetView(this);
                LoadView();
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
            });

            AddBookCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new AddBookViewModel(this));
            });

            LoadViewCommand = new RelayCommand(async o =>
            {
                LoadView();
            });

            EditCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new EditShelfViewModel(this));
            });

            ClearCommand = new RelayCommand(o =>
            {
                ClearShelf();
            });

            DeleteCommand = new RelayCommand(o =>
            {
                DeleteShelf();
            });
        }

        private void ClearShelf()
        {
            Items.Clear();

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

            OnShelfViewModelChanged();
        }

        public event Action ShelfViewModelDeleted;
    }
}
