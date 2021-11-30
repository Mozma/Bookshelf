using Bookshelf.Models.Data;
using Bookshelf.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshelf.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContextFactory dataContextFactory = new DataContextFactory();

        private DataContext context = null;
        private DbSet<T> table = null;

        public Repository()
        {
            this.context = dataContextFactory.CreateDbContext();

            table = context.Set<T>();
        }

        public Repository(DataContext context)
        {
            this.context = context;

            table = context.Set<T>();
        }


        //public IQueryable<T> GetAll()
        //{
        //    return table.AsNoTracking();
        //}

        //public T Get(object id)
        //{

        //    T entity = table.Find(id);
        //    return entity;

        //}


        //public T Create(T entity)
        //{
        //    var newEntity = table.Add(entity);
        //    context.SaveChanges();

        //    return newEntity.Entity;
        //}

        //public bool Delete(object id)
        //{
        //    T exsistingEntity = Get(id);
        //    table.Remove(exsistingEntity);
        //    context.SaveChangesAsync();

        //    return true;
        //}


        //public T Update(object id, T entity)
        //{
        //    //T exsistingEntity = Get(id);
        //    //context.Entry(entity).State = EntityState.Detached;
        //    context.Update(entity);
        //    context.SaveChangesAsync();

        //    return entity;
        //}

        //public void SaveChanges() {
        //    context.SaveChangesAsync();
        //}

        public async Task<T> Create(T entity)
        {
            using (var context = dataContextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (var context = dataContextFactory.CreateDbContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var context = dataContextFactory.CreateDbContext())
            {
                T entity = await Get(id);
                
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }
        public async Task<T> Get(int id)
        {
            using (var context = dataContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FindAsync(id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (var context = dataContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

    }
}
