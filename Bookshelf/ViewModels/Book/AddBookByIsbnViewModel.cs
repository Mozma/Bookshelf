using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Bookshelf.Stores;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class AddBookByIsbnViewModel : BaseViewModel
    {
        public BookInfo Entity { get; set; }
        public ICommand SelectCoverCommand { get; set; }
        public ICommand FindByIsbnCommand { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public Bitmap Cover { get; set; }

        public string PagesNumber { get; set; }
        public string PagesRead { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }


        public List<Shelf> Shelves { get; set; }
        public Shelf SelectedShelf { get; set; }

        public List<string> Publishers { get; set; }
        public List<string> Statuses { get; set; }

        private BookViewModel _viewModel;
        private BookStore _bookStore;

        public AddBookByIsbnViewModel(BookStore bookStore)
        {
            _bookStore = bookStore;

            SetupCommands();
            SetFields();
        }

        private void SetupCommands()
        {
            CloseCommand = new RelayCommand(o =>
            {
                Navigation.RemoveOverlay();
            });

            SelectCoverCommand = new RelayCommand(o =>
            {
                SelectCover();
            });

            CancelCommand = new RelayCommand(o =>
            {
                SetFields();
            });

            FindByIsbnCommand = new RelayCommand(o =>
            {
                FindByIsbn();
            });

        }

        private void FindByIsbn()
        {
            var googleBooks = new GoogleBooks();

            var book = googleBooks.FindBookByIsbnAsync(ISBN).Result;

            Entity = book;

            SetFields();
        }

        private void SetFields()
        {
            if (Entity != null)
            {

                Title = Entity.Title;
                Author = Entity.Author;

                PagesNumber = Entity.PagesNumber == null ? string.Empty : Entity.PagesNumber.ToString();
                Year = Entity.Year == null ? string.Empty : Entity.Year.ToString();
                Publisher = Entity.Publisher == null ? string.Empty : Entity.Publisher;

            }

            //GetSuggestions();
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


        public void GetSuggestions()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {

                Publishers = context.Set<Publisher>().Select(o => o.Name).ToList();
            }
        }
    }
}

