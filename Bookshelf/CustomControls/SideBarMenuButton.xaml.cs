using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bookshelf.CustomControls
{
    /// <summary>
    /// Interaction logic for SideBarMenuButton.xaml
    /// </summary>
    public partial class SideBarMenuButton : UserControl
    {


        public BitmapImage Icon
        {
            get { return (BitmapImage)GetValue(MyIconertyProperty); }
            set { SetValue(MyIconertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyIconerty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyIconertyProperty =
            DependencyProperty.Register("MyIconerty", typeof(BitmapImage), typeof(SideBarMenuButton), new PropertyMetadata());


        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(SideBarMenuButton), new PropertyMetadata("Button Content"));


        public SideBarMenuButton()
        {
            InitializeComponent();
        }
    }
}
