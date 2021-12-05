using Bookshelf.ViewModels;
using System;
using System.Collections.Generic;

namespace Bookshelf
{
    public class Navigation
    {
        public static Navigation Instance = new Navigation();

        public static Stack<BaseViewModel> History { get; set; } = new Stack<BaseViewModel>();

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
            return IoC.Get<ApplicationViewModel>().CurrentViewModel;
        }

        public static void SetView(BaseViewModel viewModel)
        {
            History.Push(Instance.CurrentViewModel);
            Instance.CurrentViewModel = viewModel;

        }

        public static void GoToPrevieusViewModel()
        {
            // Нужен тест

            if (History.Count != 0)
            {
                while (History.Count != 0 && History.Peek() == GetCurrentViewModel())
                {
                    History.Pop();
                }
                Instance.CurrentViewModel = History.Pop();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }


        public event Action CurrentViewModelChanged;
    }
}
