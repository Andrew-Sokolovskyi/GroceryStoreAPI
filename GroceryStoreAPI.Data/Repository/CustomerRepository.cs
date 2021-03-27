using GroceryStoreAPI.Data.Context;
using GroceryStoreAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Data
{
    public class CustomerRepository : Repository<Customer>
    {
        public override List<Customer> DbTable
        {
            get => DbTables.Customers;
            set => DbTables.Customers = value;
        }

        public CustomerRepository(IDatabase database) : base(database)
        {
        }


        public override Customer FindById(int id)
        {
            try
            {
                return DbTables.Customers.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {
                // _logger.LogError("FAILED: FindById - ${ex.Error}");
                return null;
            }
        }

        public override Customer Update(Customer customer)
        {
            try
            {
                var selectedCustomer = FindById(customer.Id);
                selectedCustomer.Name = customer.Name;
                SaveChanges();

            }
            catch (Exception)
            {
                // _logger.LogError("FAILED: Update - ${ex.Error}");
                return null;
            }

            return customer;
        }
    }
}
