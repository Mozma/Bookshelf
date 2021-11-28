using System.Threading.Tasks;
using System.Windows;

namespace Bookshelf.Dialogs
{
    public interface IUIManager
    {
        Task ShowDialogWindow<T>(T window) where T : Window;
    }
}
