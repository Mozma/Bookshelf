using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class DialogWindowViewModel : BaseViewModel
    {

        public string Title { get; set; }

        public Control Content { get; set; }

        public DialogWindowViewModel(Window window) 
        {

        }
    }
}
