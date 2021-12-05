﻿using Bookshelf.Helpers;
using Bookshelf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Bookshelf
{
    public partial class WindowViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel
        {
            get => Navigation.GetCurrentViewModel();
            set => Navigation.SetView(value);
        }

        public bool IsHomeViewModel => CurrentViewModel is HomeViewModel;
        public bool IsNotesViewModel => CurrentViewModel is NotesViewModel;
        public bool IsShelvesViewModel => CurrentViewModel is ShelvesViewModel;

        #region Window property
        private Window currentWindow;
        private int outerMarginSize = 10;
        private int windowRadius = 5;

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

        #endregion

        #region Commands

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand HomeViewCommand { get; set; }
        public ICommand NotesViewCommand { get; set; }
        public ICommand ShelvesViewCommand { get; set; }


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


            Navigation.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;

            FixEfFirstLoadProblem();

            SetupViewModels();
            SetupCommands();

            var resizer = new WindowResizer(currentWindow);
        }

        private void SetupViewModels()
        {
            Navigation.SetView(new HomeViewModel());
        }

        private void SetupCommands()
        {
            MinimizeCommand = new RelayCommand(o => currentWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(o => currentWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(o => currentWindow.Close());


            /// Navigation Commands

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentViewModel = CurrentViewModel is HomeViewModel ? CurrentViewModel : new HomeViewModel();
            });

            ShelvesViewCommand = new RelayCommand(o =>
            {
                CurrentViewModel = CurrentViewModel is ShelvesViewModel ? CurrentViewModel : new ShelvesViewModel();
            });

            NotesViewCommand = new RelayCommand(o =>
            {
                CurrentViewModel = CurrentViewModel is NotesViewModel ? CurrentViewModel : new NotesViewModel();
            });

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(IsHomeViewModel));
            OnPropertyChanged(nameof(IsNotesViewModel));
            OnPropertyChanged(nameof(IsShelvesViewModel));
        }

        private void FixEfFirstLoadProblem()
        {
            // Fix for Issue: Reduce EF Core application startup time via compiled models
            // https://github.com/dotnet/efcore/issues/1906
            CurrentViewModel = new ShelvesViewModel();
            CurrentViewModel = null;
        }
    }
}
