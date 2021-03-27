using GroceryStoreAPI.Data.Context;
using GroceryStoreAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDatabase _database;
        public virtual List<TEntity> DbTable { get; set; }
        public virtual DataTables DbTables { get; set; }

        public Repository(IDatabase database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));

            _database = database;
            DbTables = database.GetData();
        }

        protected void SaveChanges()
        {
            _database.SaveData();
        }

        public List<TEntity> GetAll()
        {
            return DbTable.ToList();
        }

        public void Add(TEntity entity)
        {
            DbTable.Add(entity);
            SaveChanges();
        }

        public abstract TEntity FindById(int Id);

        public abstract TEntity Update(TEntity entity);
    }
}
