
using Bookshelf.Helpers;
using Bookshelf.Models;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public Book Entity { get; set; }
        public ICommand OpenBookViewCommand { get; set; }
        public ICommand SelectCoverCommand { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public Bitmap Cover { get; set; } = BitmapImageConverter.BitmapImageToBitmap(ResourceFinder.Get<BitmapImage>("DefaultBookCover"));


        public BookViewModel()
        {
            SetupCommands();
        }

        public BookViewModel(Book entity) : this()
        {
            Entity = entity;
            SetFields();
        }

        private void SetupCommands()
        {
            OpenBookViewCommand = new RelayCommand(o =>
            {
                Navigation.SetView(this);
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
                SetFields();
            });

            SelectCoverCommand = new RelayCommand(o =>
            {
                SelectCover();
            });

            CancelCommand = new RelayCommand(o =>
            {
                SetFields();
            });

        }

        private void SetFields()
        {
            if (Entity != null)
            {
                Title = Entity.Title;
                Author = Entity.BookBinds[0].Author.FullName;
                if (Entity.Image != null)
                {
                    Cover = Entity.Image.Base64Data.Base64StringToBitmap();
                }
            }
        }

        private void SelectCover()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                Cover = BitmapImageConverter.BitmapImageToBitmap(new BitmapImage(new Uri(System.IO.Path.GetFullPath(openFileDialog.FileName))));
            }
        }

    }
}
