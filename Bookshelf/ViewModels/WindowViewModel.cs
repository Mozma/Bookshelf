using Bookshelf.Helpers;
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

        #region Properties
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

            MinimizeCommand = new RelayCommand(() => currentWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => currentWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => currentWindow.Close());

            var resizer = new WindowResizer(currentWindow);
        }
    }
}
