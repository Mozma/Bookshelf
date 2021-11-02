using System.Linq;
using System.Windows;

namespace Bookshelf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new WindowViewModel(this);
        }

        private void AppMainWindow_Activated(object sender, System.EventArgs e)
        {
           
            Overlay.Visibility = Visibility.Hidden;  
        }

        private void AppMainWindow_Deactivated(object sender, System.EventArgs e)
        {
            if (Application.Current.Windows.OfType<DialogWindow>().Any()){ 

                Overlay.Visibility = Visibility.Visible;
            }

        }

        private void AppMainWindow_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
