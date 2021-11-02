using Bookshelf.ViewModels;
using System;

namespace Bookshelf
{
    public class Navigation
    {
        public static Navigation Instance = new Navigation();

        public BaseViewModel CurrentViewModel
        {
            get => IoC.Get<ApplicationViewModel>().CurrentViewModel;
            set
            {
                IoC.Get<ApplicationViewModel>().CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public static BaseViewModel GetCurrentViewModel()
        {
            return Instance.CurrentViewModel;
        }

        public static void SetView(BaseViewModel viewModel)
        {
            Instance.CurrentViewModel = viewModel;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }


        public event Action CurrentViewModelChanged;
    }
}
