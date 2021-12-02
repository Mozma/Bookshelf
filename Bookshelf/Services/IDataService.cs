using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookshelf.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(object id);
        Task<T> Create(T entity);
        Task<T> Update(object id, T entity);
        Task<bool> Delete(object id);
    }
}
