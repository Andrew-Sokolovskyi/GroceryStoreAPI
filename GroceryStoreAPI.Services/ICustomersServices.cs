using GroceryStoreAPI.Domain.Models;
using System.Collections.Generic;

namespace GroceryStoreAPI.Services
{
    public interface ICustomersServices
    {
        List<Customer> GetCustomers();
        Customer FindCustomerById(int id);
        Customer AddCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
    }
}
