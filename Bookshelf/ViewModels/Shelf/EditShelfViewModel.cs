using Bookshelf.Models;
using Bookshelf.Models.Data;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class EditShelfViewModel : BaseViewModel
    {
        private Shelf CurrentShelf { get; set; }
        private ShelfViewModel CurrentViewModel { get; set; }

        public string ShelfName { get; set; }
        private string CurrentShelfName { get; }
        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public EditShelfViewModel()
        {
            SetupCommands();
        }

        public EditShelfViewModel(ShelfViewModel shelfViewModel) : this()
        {
            CurrentViewModel = shelfViewModel;
            CurrentShelf = shelfViewModel.Entity;
            CurrentShelfName = shelfViewModel.Entity.Name;

            SetFields();
        }

        private void SetupCommands()
        {
            CancelCommand = new RelayCommand(o =>
            {
                SetFields();
                Navigation.RemoveOverlay();
            });

            AcceptCommand = new RelayCommand(o =>
            {
                SaveEntity();

                CurrentViewModel.OnPropertyChanged();

                Navigation.RemoveOverlay();
            });
        }

        private void SaveEntity()
        {
            bool isDirty = false;

            if (!string.IsNullOrWhiteSpace(ShelfName) && !ShelfName.Equals(CurrentShelfName))
            {
                CurrentShelf.Name = ShelfName;
                CurrentViewModel.Name = ShelfName;
                isDirty = true;
            }

            if (isDirty)
            {
                using (var context = new DataContextFactory().CreateDbContext())
                {
                    context.Entry(CurrentShelf).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChangesAsync();
                }

                SetFields();
            }
        }

        private void SetFields()
        {
            ShelfName = CurrentShelfName;

            GetSuggestions();
        }

        public void GetSuggestions()
        {
        }
    }
}

