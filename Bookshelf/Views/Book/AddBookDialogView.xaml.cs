using System;
using System.Windows;
using System.Windows.Controls;

namespace Bookshelf.Views
{
    /// <summary>
    /// Interaction logic for AddBookDialogView.xaml
    /// </summary>
    public partial class AddBookDialogView : UserControl
    {
        public string CoverImagePath { get; set; }

        public AddBookDialogView()
        {
            InitializeComponent();
        }

        private void mask_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileName"))
            {
                CoverImagePath = Convert.ToString(e.Data.GetData("FileName"));
            }
        }
    }
}
