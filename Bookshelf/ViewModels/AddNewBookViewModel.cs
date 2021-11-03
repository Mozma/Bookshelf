using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class AddNewBookViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public Control Content { get; set; }

        public ICommand CloseCommand { get; set; }

        public AddNewBookViewModel(Window window) 
        {
            CloseCommand = new RelayCommand(o => window.Close());

        }
    }
}
