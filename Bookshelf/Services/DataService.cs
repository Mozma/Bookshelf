using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookshelf.Services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        DataContextFactory contextFactory;

        public DataService(DataContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var createdEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(object id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FindAsync(id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(object id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FindAsync(id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (var context = contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(object id, T entity)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var updatedEntity = context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return updatedEntity.Entity;
            }
        }
    }
}
