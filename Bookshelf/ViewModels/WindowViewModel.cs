﻿using Bookshelf.Helpers;
using Bookshelf.Navigation;
using Bookshelf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Bookshelf
{
    /// <summary>
    /// View model for custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        private Window currentWindow;
        private int outerMarginSize = 10;
        private int windowRadius = 5;

        private readonly NavigationStore navigationStore;

        #region Properties

        public HomeViewModel HomeVM { get; set; }
        public ShelvesViewModel ShelfsVM { get; set; }
        public NotesViewModel NotesVM { get; set; }


        public BaseViewModel CurrentViewModel
        {
            get => navigationStore.CurrentViewModel;
            set => navigationStore.CurrentViewModel = value;
        }

        public int ResizeBorder { get; set; } = 4;
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }
        public int OuterMarginSize
        {
            get
            {
                return currentWindow.WindowState == WindowState.Maximized ? 0 : outerMarginSize;
            }
            set
            {
                outerMarginSize = value;
            }
        }
        public Thickness OuterMarginSizeThikness { get { return new Thickness(OuterMarginSize); } }
        public int WindowRadius
        {
            get
            {
                return currentWindow.WindowState == WindowState.Maximized ? 0 : windowRadius;
            }
            set
            {
                windowRadius = value;
            }
        }
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }
        public int TitleHeight { get; set; } = 21;
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public int MinHeight { get; set; } = 400;
        public int MinWidth { get; set; } = 600;
        #endregion

        #region Commands

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand HomeViewCommand { get; set; }
        public ICommand NotesViewCommand { get; set; }
        public ICommand ShelfsViewCommand { get; set; }


        #endregion

        public WindowViewModel(Window window)
        {
            currentWindow = window;
            currentWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThikness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(TitleHeight));
                OnPropertyChanged(nameof(TitleHeightGridLength));
            };

            navigationStore = new NavigationStore();
            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            SetupViewModels();
            SetupCommands();

            var resizer = new WindowResizer(currentWindow);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void SetupViewModels()
        {
            navigationStore.CurrentViewModel = new HomeViewModel();
        }

        private void SetupCommands()
        {
            MinimizeCommand = new RelayCommand(o => currentWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(o => currentWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(o => currentWindow.Close());


            /// Navigation Commands

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentViewModel = new HomeViewModel(); ;
            });

            ShelfsViewCommand = new RelayCommand(o =>
            {
                CurrentViewModel = new ShelvesViewModel(navigationStore); ;
            });

            NotesViewCommand = new RelayCommand(o =>
            {
                CurrentViewModel = new NotesViewModel();
            });

        }
    }
}
