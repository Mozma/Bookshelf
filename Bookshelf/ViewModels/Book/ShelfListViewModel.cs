
using Bookshelf.Models;
using Bookshelf.Models.Interfaces;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Linq;

namespace Bookshelf.ViewModels
{
    public class ShelfListViewModel : BaseViewModel
    {
        public List<ShelfListItemViewModel> Items { get; set; }

        //public ShelfListViewModel()
        //{
        //    IDataManagerService<Book> bookService = new DataManagerService<Book>();
        //    IDataManagerService<Author> authorService = new DataManagerService<Author>();
        //    IDataManagerService<Shelf> shelvesService = new DataManagerService<Shelf>();
        //    IDataManagerService<BookBind> bookBindService = new DataManagerService<BookBind>();


        //    List<Book> bookItems = bookService.GetAll().Result.ToList();
        //    List<Shelf> shelfItems = shelvesService.GetAll().Result.ToList();

            

        //    foreach (var item in shelfItems)
        //    {
        //        List<BookListItemViewModel>() list = from shelf in shelfItems
        //                                             where user.Age > 25
        //                                             select user;




        //        Items.Add(
        //            new ShelfListItemViewModel()
        //            {
        //                Name = item.Name,
        //                Items = shelfBooks
        //            }
        //                );
        //    }
        //}
    }
}
