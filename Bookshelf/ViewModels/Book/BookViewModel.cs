using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Bookshelf.Views;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public readonly string PlaceHolder = "---";

        public Book Entity { get; set; }
        public ICommand OpenBookViewCommand { get; set; }
        public ICommand SelectCoverCommand { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChangeStatusCommand { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public Bitmap Cover { get; set; }

        public string PagesNumber { get; set; }
        public string PagesRead { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public BookStatus Status { get; set; }

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
                UpdateView();
            });

            GoBackCommand = new RelayCommand(o =>
            {
                Navigation.GoToPrevieusViewModel();
                UpdateView();
            });

            SelectCoverCommand = new RelayCommand(o =>
            {
                EditCommand.Execute(this);
            });

            EditCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new EditBookWindow(this));
                UpdateView();
            });

            DeleteCommand = new RelayCommand(o =>
            {
                DeleteBook();
            });

            ChangeStatusCommand = new RelayCommand(o =>
            {
                SetNextStatus();
            });
        }

        private void UpdateView()
        {
            SetFields();
        }

        private void SetNextStatus()
        {
            Status = Status.Next();
            SaveStatus();
        }

        private void SaveStatus()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                Entity.Status = (int)Status;

                context.Entry(Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChangesAsync();
            }

        }

        private void DeleteBook()
        {
            var bookService = new DataService<Book>(new DataContextFactory());

            bookService.Delete(Entity.Id);
        }

        private async void SetFields()
        {
            await Task.Run(() =>
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

                    ISBN = Entity.ISBN == null ? PlaceHolder : Entity.ISBN.ToString();
                    PagesNumber = Entity.PagesNumber == null ? PlaceHolder : Entity.PagesNumber.ToString();
                    Year = Entity.Year == null ? PlaceHolder : Entity.Year.ToString();


                    PagesRead = Entity.PagesRead == null ? PlaceHolder : Entity.PagesRead.ToString();
                    Publisher = Entity.Publisher == null ? PlaceHolder : Entity.Publisher.Name;

                    Status = Entity.Status == null ? 0 : (BookStatus)Entity.Status;


                    if (Entity.Image != null)
                    {
                        Cover = Entity.Image.Base64Data.Base64StringToBitmap();
                    }
                    else
                    {
                        Cover = BitmapImageConverter.BitmapImageToBitmap(ResourceFinder.Get<BitmapImage>("DefaultBookCover"));
                    }

                }
            });

            GetSuggestions();
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
