using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookshelf.CustomControls
{
    /// <summary>
    /// Interaction logic for BookListControl.xaml
    /// </summary>
    public partial class ShelfItemControl : UserControl
    {
        public ICommand TitleClickCommand
        {
            get { return (ICommand)GetValue(TitleClickCommandProperty); }
            set { SetValue(TitleClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleClickCommandProperty =
            DependencyProperty.Register("TitleClickCommand", typeof(ICommand), typeof(ShelfItemControl));


        public ShelfItemControl()
        {
            InitializeComponent();
        }

    }
}
