using System;

namespace Bookshelf.Stores
{
    public abstract class StoreBase<T> where T : class
    {
        public event Action<T> EntityDeleted;
        public event Action<T> EntityChanged;
        public event Action<T> EntityCreated;

        public void CreateEntity(T entity)
        {
            EntityCreated?.Invoke(entity);
        }

        public void ChangeEntity(T entity)
        {
            EntityChanged?.Invoke(entity);
        }

        public void DeleteEntity(T entity)
        {
            EntityDeleted?.Invoke(entity);
        }
    }
}
