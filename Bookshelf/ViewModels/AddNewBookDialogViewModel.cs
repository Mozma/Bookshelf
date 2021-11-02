using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class AddNewBookDialogViewModel : BaseViewModel
    {
        public ICommand CreateNewBookCommand { get; set; }


        public AddNewBookDialogViewModel()
        {
        }
    }
}
