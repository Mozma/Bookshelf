
using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        private readonly string blankString = "---";

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

        public string PagesNumber { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Status { get; set; }

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

            SaveCommand = new RelayCommand(o =>
            {
                SaveEntity();
            });

        }

        private void SaveEntity()
        {

            if (!Title.Trim().Equals(blankString))
            {
                Entity.Title = Title;
            }

            if (!Author.Trim().Equals(blankString))
            {
                //Entity.BookBinds.firs = Author;
            }

            if (!PagesNumber.Trim().Equals(blankString) && !string.IsNullOrWhiteSpace(PagesNumber))
            {
                Entity.PagesNumber = int.Parse(PagesNumber);
            }

            int year = 0;
            if (!Year.Trim().Equals(blankString) && int.TryParse(Year, out year))
            {
                Entity.Year = year;
            }

            if (!ISBN.Trim().Equals(blankString))
            {
                Entity.ISBN = ISBN;
            }

            //if (!Title.Trim().Equals("---"))
            //{
            //    Entity.Publisher = Title;
            //}

            //if (!Title.Trim().Equals("---"))
            //{
            //    Entity.Publisher = Status;
            //}

            using (var context = new DataContextFactory().CreateDbContext())
            {
                context.Entry(Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChangesAsync();
            }
        }

        private void SetFields()
        {
            if (Entity != null)
            {
                var bookBindSerice = new DataService<BookBind>(new DataContextFactory());
                var author = bookBindSerice.GetAll().Result
                    .Where(o => o.BookId == Entity.Id)
                    .Select(o => o.Author)
                    .ToList();

                Title = Entity.Title;
                Author = author[0].FullName;
                PagesNumber = Entity.PagesNumber == null ? "---" : Entity.PagesNumber.ToString();

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
