using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Repositories;
using Bookshelf.Stores;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public readonly string PlaceHolder = "---";
        private BookStore _bookStore;

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
        public string CreationDate { get; set; }
        public string CreationDateToolTip { get; set; }
        public BookStatus Status { get; set; }

        public IEnumerable<string> Publishers { get; set; }

        public BookViewModel(Book entity, BookStore bookStore)
        {
            _bookStore = bookStore;
            Entity = entity;

            BindEvents();

            SetFields();
            SetupCommands();
        }


        private void BindEvents()
        {
            _bookStore.EntityChanged += OnViewModelChanged;
        }

        private void UnbindEvents()
        {
            _bookStore.EntityChanged -= OnViewModelChanged;
        }

        private void OnViewModelChanged(Book obj)
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                Entity = unitOfWork.Books.Get(Entity.Id);
            }

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
                Navigation.SetCurrentOverlayViewModel(new EditBookViewModel(this, _bookStore));
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
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Books.UpdateStatus(Entity, Status);
                unitOfWork.Complete();
                _bookStore.ChangeEntity(Entity);
            }
        }

        private void DeleteBook()
        {
            using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
            {
                unitOfWork.Books.Remove(Entity);
                unitOfWork.Complete();

                _bookStore.DeleteEntity(Entity);
            }
        }

        private void SetFields()
        {
            if (Entity != null)
            {
                using (var unitOfWork = new UnitOfWork(new DataContextFactory().CreateDbContext()))
                {
                    Entity = unitOfWork.Books.Get(Entity.Id);

                    Title = Entity.Title;

                    var authors = unitOfWork.Books.GetAuthors(Entity.Id);
                    Author = authors.First().FullName;
                    ISBN = Entity.ISBN == null ? PlaceHolder : Entity.ISBN.ToString();
                    PagesNumber = Entity.PagesNumber == null ? PlaceHolder : Entity.PagesNumber.ToString();
                    Year = Entity.Year == null ? PlaceHolder : Entity.Year.ToString();
                    CreationDate = Entity.CreationTime == null ? PlaceHolder : Entity.CreationTime.ToString("dd MMMM yyyy");
                    CreationDateToolTip = Entity.CreationTime == null ? PlaceHolder : Entity.CreationTime.ToString("ddd, dd MMM yyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

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
            }

            GetSuggestions();
        }

        public void GetSuggestions()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Publishers = unitOfWork.Publishers.GetNames();
            }
        }

        public override void Dispose()
        {
            UnbindEvents();
            base.Dispose();
        }
    }
}
