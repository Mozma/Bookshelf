using Bookshelf.Models;
using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bookshelf.Tests
{
    public class DataContextBuilder
    {
        private DataContext _context;

        private Shelf[] _shelves;
        private Book[] _books;
        private ShelfBind[] _shelfBinds;


        public DataContextBuilder WithShelves(params Shelf[] items)
        {
            _shelves = items;
            return this;
        }
        public DataContextBuilder WithBooks(params Book[] items)
        {
            _books = items;
            return this;
        }
        public DataContextBuilder WithShelfBinds(params ShelfBind[] items)
        {
            _shelfBinds = items;
            return this;
        }

        public DataContext Build()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                );
            _context = new DataContext(dbOptions.Options);

            if (_shelves != null)
            {
                _context.AddRange(_shelves);
                _context.SaveChanges();
            }
            if (_books != null)
            {
                _context.AddRange(_books);
                _context.SaveChanges();
            }
            if (_shelfBinds != null)
            {
                _context.AddRange(_shelfBinds);
                _context.SaveChanges();
            }

            return _context;
        }

        public static implicit operator DataContext(DataContextBuilder builder)
        {
            return builder.Build();
        }
    }
}
