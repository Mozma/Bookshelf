using Bookshelf.Models;
using Bookshelf.Models.Data;
using Bookshelf.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public IQueryable<T> GetAll()
        {
            var query = table.AsNoTracking();
            return query;

        }

        public T Get(object id)
        {

            T entity = table.Find(id);
            return entity;

        }


        public T Create(T entity)
        {
            var newEntity = table.Add(entity);
            context.SaveChanges();

            return newEntity.Entity;
        }

        public bool Delete(object id)
        {
            T exsistingEntity = Get(id);
            table.Remove(exsistingEntity);
            context.SaveChangesAsync();

            return true;
        }


        public T Update(object id, T entity)
        {
                T exsistingEntity = Get(id);
                context.Update(entity);
                context.SaveChangesAsync();

                return entity;
        }



    }
}
