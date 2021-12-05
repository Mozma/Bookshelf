using Bookshelf.Models;
using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookshelf.Services
{
    public class ShelfService : DataService<Shelf>
    {
        public ShelfService(DataContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public async Task<Shelf> GetById(int id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                Shelf entity = await context.Set<Shelf>()
                    .Include(o => o.ShelfBinds).SingleOrDefaultAsync(o => o.Id == id);

                return entity;
            }
        }

        public async Task<IEnumerable<Shelf>> GetAll()
        {
            using (var context = contextFactory.CreateDbContext())
            {
                IEnumerable<Shelf> entities = await context.Set<Shelf>()
                    .Include(s => s.ShelfBinds)
                    .ToListAsync();
                return entities;
            }
        }

    }
}
