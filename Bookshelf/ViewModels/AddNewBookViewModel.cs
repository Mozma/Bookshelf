using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class AddNewBookViewModel : BaseViewModel
    {
        public BitmapImage Image { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string ShelfName { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand AddBookCommand { get; set; }
        public ICommand ChooseBookCoverCommand { get; set; }

        public bool IsCoverVisible { get; set; } = false;

        public List<string> BooksTitles { get; set; }
        public List<string> AuthorsNames { get; set; }
        public List<string> ShelvesNames { get; set; }
        public List<Shelf> Shelves { get; set; }
        public Shelf SelectedShelf { get; set; }

        public AddNewBookViewModel(Window window)
        {
            CloseCommand = new RelayCommand(o => window.Close());
            AddBookCommand = new RelayCommand(o => AddBook());
            ChooseBookCoverCommand = new RelayCommand(o =>
            {
                IsCoverVisible = !IsCoverVisible;
            });

            GetSuggestions();
        }

        private void AddBook()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {

                var bookBindRepository = new Repository<BookBind>(context);
                var shelfBindRepository = new Repository<ShelfBind>(context);

                var bookRepository = new Repository<Book>(context);
                var shelfRepository = new Repository<Shelf>(context);

                var book = bookRepository.Create(new Book { Title = BookTitle });


                var shelf = shelfRepository.GetAll().First(s => s.Name == SelectedShelf.Name);

                shelfBindRepository.Create(new ShelfBind
                {
                    Book = book,
                    Shelf = shelf
                });


                bookBindRepository.Create(new BookBind
                {
                    Book = book,
                    Author = new Author { FullName = AuthorName }
                });

            }


            CloseCommand.Execute(this);
        }

        public void GetSuggestions()
        {
            var bookRepository = new Repository<Book>();
            var authorRepository = new Repository<Author>();
            var shelvesRepository = new Repository<Shelf>();

            BooksTitles = bookRepository.GetAll().Select(o => o.Title).ToList();
            AuthorsNames = authorRepository.GetAll().Select(o => o.FullName).ToList();
            ShelvesNames = shelvesRepository.GetAll().Select(o => o.Name).ToList();

            Shelves = shelvesRepository.GetAll().ToList();
        }
    }
}
