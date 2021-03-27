using System.Collections.Generic;

namespace GroceryStoreAPI.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        void Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity FindById(int Id);
    }
}