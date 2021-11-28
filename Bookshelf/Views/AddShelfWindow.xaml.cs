using Bookshelf.ViewModels;
using System.Windows;

namespace Bookshelf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddShelfWindow : Window
    {

        public AddShelfWindow()
        {
            InitializeComponent();
            this.DataContext = new AddShelfViewModel(this);
        }
    }
}
