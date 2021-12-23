using Bookshelf.Commands;
using Bookshelf.Stores;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class EditShelfViewModel : BaseViewModel
    {
        private readonly ShelfViewModel _viewModel;
        private readonly ShelfStore _shelfStore;
        public string ShelfName { get; set; }
        public string CurrentShelfName => _viewModel.Name;
        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditShelfViewModel(ShelfViewModel shelfViewModel, ShelfStore shelfStore)
        {
            _viewModel = shelfViewModel;
            _shelfStore = shelfStore;

            SetupCommands();

            SetFields();
        }

        private void SetupCommands()
        {
            CancelCommand = new RelayCommand(o =>
            {
                Close();
            });

            AcceptCommand = new SaveShelfChangesCommand(this, _shelfStore, _viewModel.Entity);
        }

        private void SetFields()
        {
            ShelfName = _viewModel.Name;
            GetSuggestions();
        }

        public void GetSuggestions()
        {
        }

        internal void Close()
        {
            Navigation.RemoveOverlay();
        }
    }
}

