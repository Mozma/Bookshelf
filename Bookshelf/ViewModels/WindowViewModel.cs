using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bookshelf
{
    /// <summary>
    /// View model for custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        private Window controlledWindow;
        private int outerMarginSize = 10;
        private int windowRadius = 5;

        public int ResizeBorder { get; set; } = 4;
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); }  }
        

        public int OuterMarginSize 
        { 
            get 
            { 
                return controlledWindow.WindowState == WindowState.Maximized ? 0 : outerMarginSize;
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
                return controlledWindow.WindowState == WindowState.Maximized ? 0 : windowRadius;
            }
            set
            {
                windowRadius = value;
            }
        }
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 21;
        public GridLength TitleHeightGridLength { get { return new GridLength(WindowRadius); } }
        public WindowViewModel(Window window)
        {
            controlledWindow = window;
            controlledWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThikness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(TitleHeight));
                OnPropertyChanged(nameof(TitleHeightGridLength));
            };
        }
    }
}
