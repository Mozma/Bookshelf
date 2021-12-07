using Bookshelf.ViewModels;
using System.Windows;

namespace Bookshelf.Views
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        public EditBookWindow() : this(null)
        {

        }

        public EditBookWindow(BookViewModel bookViewModel)
        {
            InitializeComponent();
            this.DataContext = new EditBookViewModel(this, bookViewModel);
        }
    }
}
