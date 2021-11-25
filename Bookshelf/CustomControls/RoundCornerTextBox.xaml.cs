using System.Windows;
using System.Windows.Controls;

namespace Bookshelf.CustomControls
{
    /// <summary>
    /// Interaction logic for RoundCornerTextBox.xaml
    /// </summary>
    public partial class RoundCornerTextBox : UserControl
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(RoundCornerTextBox), new PropertyMetadata(string.Empty));


        public RoundCornerTextBox()
        {
            InitializeComponent();
        }
    }
}
