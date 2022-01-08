namespace Bookshelf.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IShelfRepository Shelves { get; }
        IAuthorRepository Authors { get; }
        IPublisherRepository Publishers { get; }
        IBookRepository Books { get; }
        IBookBindRepository BookBinds { get; }

        int Complete();
    }
}
