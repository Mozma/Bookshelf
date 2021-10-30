using System;

namespace Bookshelf.Navigation
{
    public class NavigationStore
    {
        private BaseViewModel currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;
    }
}
