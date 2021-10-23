using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookshelf.Models.Interfaces
{
    public interface IDataManagerService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id); 
        
    }
}
