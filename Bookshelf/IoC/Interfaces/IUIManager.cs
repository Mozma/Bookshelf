using System.Threading.Tasks;

namespace Bookshelf.Dialogs
{
    public interface IUIManager
    {
        Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
