using Bookshelf.Models;
using Bookshelf.Models.Data;

namespace Bookshelf.Repositories
{
    public class BookBindRepository : Repository<BookBind>, IBookBindRepository
    {
        public BookBindRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public void CreateIfNotExist(Book book, Author author)
        {
            if (Context.BookBinds.FirstOrDefault(b => b.BookId == book.Id && b.AuthorId == author.Id) == null)
            {
                Context.BookBinds.Add(new BookBind
                {
                    BookId = book.Id,
                    AuthorId = author.Id
                });
                Context.SaveChanges();
            }
        }

        public bool CheckIfLinkExist(string title, string authorName)
        {
            var bookBind = Context.BookBinds
                    .FirstOrDefault(o =>
                        o.Book.Title.ToUpper() == title.ToUpper()
                        && o.Author.FullName.ToUpper() == authorName.ToUpper()
                    );

            return bookBind != null;
        }
    }
}