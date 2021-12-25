using Bookshelf.Commands;
using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Stores;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class AddBookViewModel : BaseViewModel
    {
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string ShelfName { get; set; }
        public Bitmap Cover { get; set; } = ResourceFinder.Get<BitmapImage>("DefaultBookCover").BitmapImageToBitmap();

        public ICommand CloseCommand { get; set; }
        public ICommand AddBookCommand { get; set; }
        public ICommand SelectCoverCommand { get; set; }

        public List<string> BooksTitles { get; set; }
        public List<string> AuthorsNames { get; set; }
        public List<string> ShelvesNames { get; set; }
        public List<Shelf> Shelves { get; set; }
        public Shelf SelectedShelf { get; set; }

        private BookStore _bookStore;

        public AddBookViewModel(ShelfViewModel shelfViewModel, BookStore bookStore)
        {
            _bookStore = bookStore;

            SetupCommands();

            GetSuggestions();

            if (shelfViewModel != null)
            {
                SelectedShelf = Shelves.Find(o => o.Id == shelfViewModel.Entity.Id);
            }
        }

        public AddBookViewModel(BookStore bookStore) : this(null, bookStore) { }


        private void SetupCommands()
        {
            CloseCommand = new RelayCommand(o =>
            {
                Navigation.RemoveOverlay();
            });

            AddBookCommand = new CreateBookCommand(this, _bookStore);

            SelectCoverCommand = new RelayCommand(o =>
            {
                Cover = GetBitmapImageFromDialog();
            });
        }

        private Bitmap GetBitmapImageFromDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                return new BitmapImage(new Uri(Path.GetFullPath(openFileDialog.FileName))).BitmapImageToBitmap();
            }

            return ResourceFinder.Get<BitmapImage>("DefaultBookCover").BitmapImageToBitmap();
        }

        public void GetSuggestions()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {

                BooksTitles = context.Set<Book>().Select(o => o.Title).ToList();
                AuthorsNames = context.Set<Author>().Select(o => o.FullName).ToList();
                ShelvesNames = context.Set<Shelf>().Select(o => o.Name).ToList();

                Shelves = context.Set<Shelf>().ToList();
            }
        }
    }
}

