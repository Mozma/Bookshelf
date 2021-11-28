using Bookshelf.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace Bookshelf
{
    public class UIManager : IUIManager
    {

        public Task ShowDialogWindow<T>(T viewModel) where T : Window
        {
            var tcs = new TaskCompletionSource<bool>();

            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    viewModel.ShowDialog();
                }
                finally
                {
                    tcs.TrySetResult(true);
                }

            });

            return tcs.Task;
        }
    }
}