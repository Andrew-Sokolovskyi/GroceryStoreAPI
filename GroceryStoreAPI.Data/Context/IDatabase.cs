using GroceryStoreAPI.Domain.Models;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Context
{
    public interface IDatabase
    {
        DataTables GetData();
        Task ReadData();
        void SaveData();
    }
}
