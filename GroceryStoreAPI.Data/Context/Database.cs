using GroceryStoreAPI.Domain.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Context
{
    public class Database : IDatabase
    {
        readonly string _path;
        private DataTables _data = new DataTables();

        public Database(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(nameof(path));

            _path = path;
        }

        public DataTables GetData()
        {
            return _data;
        }

        public async Task ReadData()
        {
            var data = File.ReadAllText(_path);
            _data = await Task.Run(() => JsonConvert.DeserializeObject<DataTables>(data));
        }

        public void SaveData()
        {
            var data = JsonConvert.SerializeObject(_data);
            File.WriteAllText(_path, data);
        }
    }
}
