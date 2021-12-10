
using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Bookshelf.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public ICommand CancelCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public Bitmap Cover { get; set; }

        public string PagesNumber { get; set; }
        public string PagesRead { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Status { get; set; }


        public List<string> Publishers { get; set; }
        public List<string> Statuses { get; set; }


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
                SetFields();
                Refresh();
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
                SetFields();
                Refresh();
            });

            SelectCoverCommand = new RelayCommand(o =>
            {
                EditCommand.Execute(this);
            });

            EditCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new EditBookWindow(this));
            });

            DeleteCommand = new RelayCommand(o =>
            {
                DeleteBook();
                Refresh();
            });
        }

        private void DeleteBook()
        {
            var bookService = new DataService<Book>(new DataContextFactory());

            bookService.Delete(Entity.Id);
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

                var bookService = new DataService<Book>(new DataContextFactory());

                Entity = bookService.Get(Entity.Id).Result;


                Title = Entity.Title;
                Author = author[0].FullName;

                ISBN = Entity.ISBN == null ? string.Empty : Entity.ISBN.ToString();
                PagesNumber = Entity.PagesNumber == null ? string.Empty : Entity.PagesNumber.ToString();
                Year = Entity.Year == null ? string.Empty : Entity.Year.ToString();


                PagesRead = Entity.PagesRead == null ? string.Empty : Entity.PagesRead.ToString();
                Publisher = Entity.Publisher == null ? string.Empty : Entity.Publisher.Name;
                Status = Entity.Status == null ? string.Empty : Entity.Status.Name;


                if (Entity.Image != null)
                {
                    Cover = Entity.Image.Base64Data.Base64StringToBitmap();
                }
                else
                {
                    Cover = BitmapImageConverter.BitmapImageToBitmap(ResourceFinder.Get<BitmapImage>("DefaultBookCover"));
                }

            }

            GetSuggestions();
        }

        public void Refresh()
        {
            BookViewModelChanged?.Invoke();
        }

        public void GetSuggestions()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {

                Publishers = context.Set<Publisher>().Select(o => o.Name).ToList();
                Statuses = context.Set<Status>().Select(o => o.Name).ToList();
            }
        }

        public event Action BookViewModelChanged;

    }
}
