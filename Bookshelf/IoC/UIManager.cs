using Bookshelf.Dialogs;
using System.Windows;

namespace Bookshelf
{
    public class UIManager : IUIManager
    {
        public bool? ShowDialogWindow<T>(T viewModel) where T : Window
        {
            return viewModel.ShowDialog();
        }
    }
}