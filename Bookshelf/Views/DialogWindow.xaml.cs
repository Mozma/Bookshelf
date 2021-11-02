using Bookshelf.ViewModels;
using System.Windows;

namespace Bookshelf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
            this.DataContext = new DialogWindowViewModel(this);
        }
    }
}
