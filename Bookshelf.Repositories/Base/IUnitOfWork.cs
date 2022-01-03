namespace Bookshelf.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IShelfRepository Shelves { get; }

        int Complete();
    }
}
