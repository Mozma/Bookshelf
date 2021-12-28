using System;

namespace Bookshelf
{
    public interface IUnitOfWork : IDisposable
    {
        IShelfRepository Shelves { get; }

        int Complete();
    }
}
