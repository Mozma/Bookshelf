using Bookshelf.ViewModels;
using System.Windows;

namespace Bookshelf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddNewBookWindow : Window
    {
        public AddNewBookWindow()
        {
            InitializeComponent();
            this.DataContext = new AddNewBookViewModel(this);
        }

        private void RoundCornerTextBox_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
