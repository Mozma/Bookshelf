using Bookshelf.ViewModels;
using System;
using System.Windows;

namespace Bookshelf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddNewBookWindow : Window
    {
        public string CoverImagePath { get; set; }

        public AddNewBookWindow()
        {
            InitializeComponent();
            this.DataContext = new AddNewBookViewModel(this);
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
