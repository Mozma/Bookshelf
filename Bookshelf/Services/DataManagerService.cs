using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Services
{
    public class DataManagerService<T> : IDataManagerService<T>  where T : DomainObject
    {
        private readonly DataContext dataContext;

        public DataManagerService()
        {

        }


        public async Task<T> Create(T entity)
        {
            var newEntity = dataContext.Set<T>().Add(entity);
            dataContext.SaveChanges();

            return newEntity.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            T entity = dataContext.Set<T>().FirstOrDefault(e => e.Id == id);
            dataContext.Set<T>().Remove(entity);
            dataContext.SaveChanges();

            return true;
        }


        public async Task<T> Update(int id, T entity)
        {
            

        }

        public async Task<T> Get(int id)
        {
            T entity = dataContext.Set<T>().FirstOrDefault(e => e.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            IEnumerable<T> entities = await dataContext.Set<T>().ToListAsync();
            return entities;
        }
    }
}
