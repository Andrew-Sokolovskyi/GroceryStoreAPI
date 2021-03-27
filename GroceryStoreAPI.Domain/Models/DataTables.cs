using System.Collections.Generic;

namespace GroceryStoreAPI.Domain.Models
{
    public class DataTables
    {
        public List<Customer> Customers { get; set; }

        public List<Product> Products { get; set; }
    }
}
