using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bookshelf.ViewModels.Base
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
