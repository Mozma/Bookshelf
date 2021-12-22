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

        public BaseViewModel CurrentOverlayViewModel
        {
            get => IoC.Get<ApplicationViewModel>().CurrentOverlayViewModel;
            set
            {
                IoC.Get<ApplicationViewModel>().CurrentOverlayViewModel = value;
                OnCurrentOverlayModelChanged();
            }

        }



        public static void RemoveOverlay()
        {
            Instance.CurrentOverlayViewModel = null;
            Instance.OnCurrentOverlayRemoved();
        }


        internal static BaseViewModel GetCurrentOverlayViewModel()
        {
            return Instance.CurrentOverlayViewModel;
        }

        internal static void SetCurrentOverlayViewModel(BaseViewModel viewModel)
        {
            Instance.CurrentOverlayViewModel = viewModel;
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

        public void Update()
        {
            OnCurrentViewModelChanged();
            OnCurrentOverlayModelChanged();
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        private void OnCurrentOverlayModelChanged()
        {
            CurrentOverlayModelChanged?.Invoke();
        }

        private void OnCurrentOverlayRemoved()
        {
            CurrentOverlayRemoved?.Invoke();
        }


        public event Action CurrentViewModelChanged;
        public event Action CurrentOverlayModelChanged;
        public event Action CurrentOverlayRemoved;

    }
}
