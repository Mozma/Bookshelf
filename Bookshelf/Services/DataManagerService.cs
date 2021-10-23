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
    public class DataManagerService<T> : IDataManagerService<T> where T : DomainObject
    {
        private DataContextFactory dataContextFactory = new DataContextFactory();

        public async Task<T> Create(T entity)
        {
            using (var dataContext = dataContextFactory.CreateDbContext())
            {
                var newEntity = dataContext.Set<T>().Add(entity);
                dataContext.SaveChanges();
            
                return newEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var dataContext = dataContextFactory.CreateDbContext())
            {
                T entity = dataContext.Set<T>().FirstOrDefault(e => e.Id == id);
                dataContext.Set<T>().Remove(entity);
                await dataContext.SaveChangesAsync();

                return true;
            }
        }


        public async Task<T> Update(int id, T entity)
        {

            using (var dataContext = dataContextFactory.CreateDbContext())
            {
                entity.Id = id;
                dataContext.Update(entity);
                await dataContext.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<T> Get(int id)
        {
            using (var dataContext = dataContextFactory.CreateDbContext())
            {
                T entity = dataContext.Set<T>().FirstOrDefault(e => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (var dataContext = dataContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = dataContext.Set<T>().ToList();
                return entities;
            }
        }
    }
}
