using System.Windows;

namespace Bookshelf.Dialogs
{
    public interface IUIManager
    {
        bool? ShowDialogWindow<T>(T window) where T : Window;
    }
}
