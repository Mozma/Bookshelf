using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookshelf.CustomControls
{
    /// <summary>
    /// Interaction logic for BookListControl.xaml
    /// </summary>
    public partial class BookListControl : UserControl
    {

        public ICommand TitleClickCommand
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("TitleClickCommand", typeof(ICommand), typeof(BookListControl));


        public BookListControl()
        {
            InitializeComponent();
        }
    }
}
