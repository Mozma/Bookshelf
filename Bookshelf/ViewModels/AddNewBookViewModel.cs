using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class AddNewBookViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public Control Content { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand AddBookCommand { get; set; }
        public ICommand ChooseBookCoverCommand { get; set; }

        public BitmapImage Image { get; set; }

        public bool IsCoverVisible { get; set; } = false;



        public AddNewBookViewModel(Window window)
        {
            CloseCommand = new RelayCommand(o => window.Close());
            //AddBookCommand = new RelayCommand(o => IsCoverVisible = !IsCoverVisible);
            ChooseBookCoverCommand = new RelayCommand(o =>
            {
                IsCoverVisible = !IsCoverVisible;
            });

            
        }
    }
}
