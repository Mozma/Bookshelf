using Bookshelf.Models.Data;

namespace Bookshelf.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;

            Shelves = new ShelfRepository(_context);
            Books = new BookRepository(_context);
            Authors = new AuthorRepository(_context);
            Publishers = new PublisherRepository(_context);
            BookBinds = new BookBindRepository(_context);
        }
        public UnitOfWork() : this(new DataContextFactory().CreateDbContext()) { }

        public IAuthorRepository Authors { get; private set; }
        public IPublisherRepository Publishers { get; private set; }
        public IShelfRepository Shelves { get; private set; }
        public IBookRepository Books { get; private set; }
        public IBookBindRepository BookBinds { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
