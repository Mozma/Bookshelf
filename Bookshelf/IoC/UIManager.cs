using Bookshelf.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace Bookshelf
{
    public class UIManager : IUIManager
    {

        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            var tcs = new TaskCompletionSource<bool>(); 

            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    new AddNewBookWindow().ShowDialog();
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