using Bookshelf.ViewModels;
using System.Windows;

namespace Bookshelf.Views
{
    /// <summary>
    /// Interaction logic for EditShelfWindow.xaml
    /// </summary>
    public partial class EditShelfWindow : Window
    {
        public EditShelfWindow(ShelfViewModel shelfViewModel)
        {
            InitializeComponent();
            this.DataContext = new EditShelfViewModel(this, shelfViewModel);
        }
    }
}
