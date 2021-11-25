using Microsoft.Win32;
using System.Drawing;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public ICommand OpenBookViewCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand SelectCoverCommand { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
        public Bitmap Cover { get; set; }// = ResourceFinder.Get<BitmapImage>("DefaultBookCover");

        public BookViewModel()
        {
            SetupCommands();
        }

        private void SetupCommands()
        {
            OpenBookViewCommand = new RelayCommand(o =>
            {
                Navigation.SetView(this);

            });

            GoBackCommand = new RelayCommand(o =>
            {
                // Todo: Добавить историю переключений
            });

            SelectCoverCommand = new RelayCommand(o =>
            {
                SelectCover();
            });

        }
        private void SelectCover()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                //  Cover = new BitmapImage(new Uri(System.IO.Path.GetFullPath(openFileDialog.FileName)));
            }
        }

    }
}
