using Bookshelf.Models;
using Bookshelf.Models.Data;

namespace Bookshelf.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public Author GetOrCreateAuthorWithName(string name)
        {
            var author = Context.Authors.FirstOrDefault(a => string.Equals(a.FullName.ToUpper(), name.Trim().ToUpper()));

            if (author == null)
            {
                author = Context.Authors.Add(new Author { FullName = name.Trim() }).Entity;
                Context.SaveChanges();
            }

            return author;
        }
    }
}