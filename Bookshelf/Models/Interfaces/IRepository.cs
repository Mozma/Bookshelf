using System.Linq;

namespace Bookshelf.Models.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(object id);
        T Create(T entity);
        T Update(object id, T entity);
        bool Delete(object id);

    }
}
