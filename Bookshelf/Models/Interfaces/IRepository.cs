using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookshelf.Models.Interfaces
{
    public interface IRepository<T>
    {
        //IQueryable<T> GetAll();
        //T Get(object id);
        //T Create(T entity);
        //T Update(object id, T entity);
        //bool Delete(object id);

        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();

    }
}
