using GroceryStoreAPI.Data;
using GroceryStoreAPI.Domain.Models;
using System.Collections.Generic;

namespace GroceryStoreAPI.Services
{
    public class CustomersServices : ICustomersServices
    {
        private readonly IRepository<Customer> _customers;

        public CustomersServices(IRepository<Customer> customers)
        {
            _customers = customers;
        }

        public Customer AddCustomer(Customer customer)
        {
            _customers.Add(customer);
            return customer;
        }

        public Customer FindCustomerById(int id)
        {
            return _customers.FindById(id);
        }

        public List<Customer> GetCustomers()
        {
            return _customers.GetAll();
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return _customers.Update(customer);
        }
    }
}
