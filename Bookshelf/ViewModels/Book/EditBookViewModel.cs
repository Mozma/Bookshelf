using Bookshelf.Helpers;
using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bookshelf.ViewModels
{
    public class EditBookViewModel : BaseViewModel
    {
        public Book Entity { get; set; }
        public ICommand SelectCoverCommand { get; set; }

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

        private BookViewModel viewModel;

        public EditBookViewModel(BookViewModel bookViewModel)
        {
            SetupCommands();

            viewModel = bookViewModel;
            Entity = viewModel.Entity;

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

            SaveCommand = new RelayCommand(o =>
            {
                SaveEntity();
                CloseCommand.Execute(this);
            });

        }

        private void SaveEntity()
        {
            bool isDirty = false;

            if (!string.IsNullOrWhiteSpace(ISBN))
            {
                Entity.ISBN = ISBN;
                isDirty = true;
            }

            if (!string.IsNullOrWhiteSpace(Title))
            {
                Entity.Title = Title;
                isDirty = true;
            }


            if (!string.IsNullOrWhiteSpace(PagesNumber) && int.TryParse(PagesNumber, out int pagesNumber))
            {
                Entity.PagesNumber = pagesNumber;
                isDirty = true;
            }

            if (!string.IsNullOrWhiteSpace(PagesRead) && int.TryParse(PagesRead, out int pagesRead))
            {
                Entity.PagesRead = pagesRead;
                isDirty = true;
            }

            if (!string.IsNullOrWhiteSpace(Year) && int.TryParse(Year, out int year))
            {
                Entity.Year = year;
                isDirty = true;
            }

            using (var context = new DataContextFactory().CreateDbContext())
            {

                var image = context.Set<Models.Image>().FirstOrDefault(i => i.Base64Data.Equals(Cover.BitmapToBase64String()));
                if (image == null)
                {
                    image = context.Set<Models.Image>().Add(new Models.Image
                    {
                        Base64Data = Cover.BitmapToBase64String()
                    }).Entity;
                }

                var publisher = context.Set<Publisher>().FirstOrDefault(p => p.Name.Equals(Publisher.Trim()));
                if (publisher == null)
                {
                    publisher = context.Set<Publisher>().Add(new Publisher
                    {
                        Name = Publisher.Trim()
                    }).Entity;
                }

                var author = context.Set<Author>().FirstOrDefault(p => p.FullName.Equals(Author.Trim()));
                if (author == null)
                {
                    author = context.Set<Author>().Add(new Author
                    {
                        FullName = Author.Trim()
                    }).Entity;
                }

                context.SaveChanges();

                Entity.ImageId = image.Id;
                Entity.PublisherId = publisher.Id;
                Entity.BookBinds[0].AuthorId = author.Id;

                isDirty = true;
            }

            if (isDirty)
            {
                using (var context = new DataContextFactory().CreateDbContext())
                {
                    context.Entry(Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.Entry(Entity.BookBinds[0]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChangesAsync();
                }

                SetFields();
            }
        }

        private void SetFields()
        {
            if (Entity != null)
            {
                var bookBindSerice = new DataService<BookBind>(new DataContextFactory());
                var authors = bookBindSerice.GetAll().Result
                    .Where(o => o.BookId == Entity.Id)
                    .Select(o => o.Author)
                    .ToList();

                Entity = bookBindSerice.GetAll().Result.Single(o => o.BookId == Entity.Id).Book;

                Title = Entity.Title;
                Author = authors[0].FullName;

                ISBN = Entity.ISBN == null ? string.Empty : Entity.ISBN.ToString();
                PagesNumber = Entity.PagesNumber == null ? string.Empty : Entity.PagesNumber.ToString();
                Year = Entity.Year == null ? string.Empty : Entity.Year.ToString();

                PagesRead = Entity.PagesRead == null ? string.Empty : Entity.PagesRead.ToString();
                Publisher = Entity.Publisher == null ? string.Empty : Entity.Publisher.Name;

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

