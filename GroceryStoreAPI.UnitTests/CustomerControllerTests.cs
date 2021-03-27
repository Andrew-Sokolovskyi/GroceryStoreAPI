using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Data;
using GroceryStoreAPI.Data.Context;
using GroceryStoreAPI.Domain.Models;
using GroceryStoreAPI.Logging;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace GroceryStoreAPI.UnitTests
{
    [TestClass]
    public class CustomerControllerTests
    {
        Mock<IDatabase> _mockDatabase = new Mock<IDatabase>();
        Mock<ILog> _mockLogging = new Mock<ILog>();
        IRepository<Customer> _repository;
        ICustomersServices _service;
        CustomerController _controller;

        private readonly List<Customer> testData = new List<Customer>
        {
            new Customer { Id = 1, Name = "Bob"},
            new Customer { Id = 2, Name = "Mary"},
            new Customer { Id = 3, Name = "Joe"}
        };

        [TestInitialize]
        public void Initialize()
        {
            _repository = new CustomerRepository(_mockDatabase.Object) { DbTables = new DataTables { Customers = testData } };
            _service = new CustomersServices(_repository);
            _controller = new CustomerController(_service, _mockLogging.Object);
        }

        [TestMethod]
        public void Get_Workable()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Get_Returns_AllCustomers()
        {
            // Act
            var result = _controller.Get();
            var totalCount = (((result as OkObjectResult)?.Value) as List<Customer>)?.Count;

            // Assert
            Assert.AreEqual(testData.Count, totalCount);
        }

        [TestMethod]
        public void Get_Returns_SearchCustomer()
        {
            // Act
            var result = _controller.Get(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            // Act
            var foundedCustomer = ((result as OkObjectResult)?.Value) as Customer;
            // Assert
            Assert.IsInstanceOfType(foundedCustomer, typeof(Customer));
        }

        [TestMethod]
        public void Update_Returns_UpdatedCustomer()
        {
            // Act
            var newCustomer = new Customer() { Id = 2, Name = "UpdatedCustomer" };
            var result = _controller.Update(newCustomer);
            var updatedCustomer = ((result as OkObjectResult)?.Value) as Customer;

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(newCustomer, updatedCustomer);
        }

        [TestMethod]
        public void Update_Returns_BadRequest()
        {
            var result = _controller.Update(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Add_Returns_AllCustomersWithNewOne()
        {
            // Act
            var totalCustomers = testData.Count;
            var random = new Random();
            var newCustomer = new Customer() { Id = random.Next(10, 1000), Name = "NewRandomCustomer" };
            var result = _controller.Post(newCustomer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreNotEqual(totalCustomers, testData.Count);
        }
    }
}