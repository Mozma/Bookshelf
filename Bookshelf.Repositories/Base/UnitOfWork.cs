﻿using Bookshelf.Models.Data;

namespace Bookshelf.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;

            Shelves = new ShelfRepository(_context);
            Books = new BookRepository(_context);
        }

        public IShelfRepository Shelves { get; private set; }
        public IBookRepository Books { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}