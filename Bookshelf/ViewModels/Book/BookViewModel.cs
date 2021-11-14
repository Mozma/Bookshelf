﻿using Bookshelf.Helpers;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public ICommand OpenBookViewCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
        public BitmapImage Cover { get; set; } = ResourceFinder.Get<BitmapImage>("DefaultBookCover");

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

        }

    }
}
