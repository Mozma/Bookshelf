using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class AddNewBookViewModel : BaseViewModel
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

        public AddNewBookViewModel(Window window, ShelfViewModel shelfViewModel)
        {


            CloseCommand = new RelayCommand(o => window.Close());

            AddBookCommand = new RelayCommand(o =>
            {
                AddBook();
            });

            SelectCoverCommand = new RelayCommand(o =>
            {
                Cover = GetBitmapImageFromDialog();
            });

            GetSuggestions();
            if (shelfViewModel != null)
            {
                SelectedShelf = Shelves.Find(o => o.Id == shelfViewModel.Entity.Id);
            }
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

        private void AddBook()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                var author = context.Set<Author>().FirstOrDefault(a => a.FullName == AuthorName);
                var book = context.Set<Book>().FirstOrDefault(b => b.Title == BookTitle);
                var shelf = context.Set<Shelf>().FirstOrDefault(s => s.Name == SelectedShelf.Name);
                var image = context.Set<Models.Image>().FirstOrDefault(i => i.Base64Data.Equals(Cover.BitmapToBase64String()));

                if (image == null)
                {
                    image = context.Set<Models.Image>().Add(new Models.Image
                    {
                        Base64Data = Cover.BitmapToBase64String()
                    }).Entity;
                    context.SaveChanges();
                }

                if (book == null)
                {
                    book = context.Set<Book>().Add(new Book
                    {
                        Title = BookTitle,
                        ImageId = image.Id
                    }).Entity;
                    context.SaveChanges();
                }

                if (author == null)
                {
                    author = context.Set<Author>().Add(new Author { FullName = AuthorName }).Entity;
                    context.SaveChanges();
                }

                if (context.Set<ShelfBind>().FirstOrDefault(s => s.BookId == book.Id && s.ShelfId == shelf.Id) == null)
                {
                    context.Set<ShelfBind>().Add(new ShelfBind
                    {
                        BookId = book.Id,
                        ShelfId = shelf.Id
                    });
                    context.SaveChanges();
                }

                if (context.Set<BookBind>().FirstOrDefault(b => b.BookId == book.Id && b.AuthorId == author.Id) == null)
                {
                    context.Set<BookBind>().Add(new BookBind
                    {
                        BookId = book.Id,
                        AuthorId = author.Id
                    });
                    context.SaveChanges();
                }

                context.SaveChanges();
            }

            CloseCommand.Execute(this);
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

