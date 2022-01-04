using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bookshelf.Repositories.Tests
{
    public class RepositoryTestBase
    {
        protected readonly DataContext context;

        public RepositoryTestBase()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                );
            context = new DataContext(dbOptions.Options);
        }
    }
}