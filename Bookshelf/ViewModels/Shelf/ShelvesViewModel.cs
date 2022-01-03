using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Repositories;
using Bookshelf.Stores;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelvesViewModel : BaseViewModel
    {
        private ObservableCollection<ShelfViewModel> items;
        public ObservableCollection<ShelfViewModel> Items
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

        public ICommand AddShelfCommand { get; set; }
        public ICommand LoadViewCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        private readonly ShelfStore _shelfStore;
        public ShelvesViewModel()
        {
            _shelfStore = new ShelfStore();

            SetupCommands();
            BindEvents();

            LoadViewCommand.Execute(this);
        }

        private void SetupCommands()
        {
            AddShelfCommand = new RelayCommand(o =>
            {
                Navigation.SetCurrentOverlayViewModel(new AddShelfViewModel(_shelfStore));
            });

            LoadViewCommand = new RelayCommand(o =>
            {
                SetupView();
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
            });
        }

        public void SetupView()
        {

            Items = new ObservableCollection<ShelfViewModel>();

            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {

                var shelves = unitOfWork.Shelves.GetAllWithBindings();

                foreach (var shelf in shelves)
                {
                    var shelfViewModel = new ShelfViewModel(shelf, _shelfStore);

                    Items.Add(shelfViewModel);
                }
            }
        }

        private void BindEvents()
        {
            _shelfStore.EntityCreated += OnShelfChanged;
            _shelfStore.EntityDeleted += OnShelfDeleted;
            _shelfStore.EntityChanged += OnShelfChanged;
        }
        private void UnbindEvents()
        {
            _shelfStore.EntityCreated -= OnShelfChanged;
            _shelfStore.EntityDeleted -= OnShelfDeleted;
            _shelfStore.EntityChanged -= OnShelfChanged;
        }

        private void OnViewModelChanged()
        {
            LoadViewCommand.Execute(this);
        }

        private void OnShelfChanged(Shelf obj)
        {
            OnViewModelChanged();
        }

        private void OnShelfDeleted(Shelf obj)
        {
            Items.Remove(Items.Single(i => i.Entity == obj));
        }

        public override void Dispose()
        {
            UnbindEvents();
            base.Dispose();
        }
    }
}
