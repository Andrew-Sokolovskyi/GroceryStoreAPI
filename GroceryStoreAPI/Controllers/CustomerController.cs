using GroceryStoreAPI.Domain.Models;
using GroceryStoreAPI.Logging;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogApi]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersServices _services;
        private readonly ILog _logger;

        public CustomerController(ICustomersServices services, ILog logger)
        {
            _services = services;
            _logger = logger;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Customer> customers;
            try
            {
                _logger.Information("Getting all customers");
                customers = _services.GetCustomers();
                if (customers == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Getting all customers was failed with error: {0}", ex.Message));
                return StatusCode(500, ex);
            }

            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Customer customer;
            try
            {
                _logger.Information("Getting customer by id");
                customer = _services.FindCustomerById(id);
                if (customer == null)
                    return NotFound();

            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Getting customer by id was failed with error: {0}", ex.Message));
                return StatusCode(500, ex);
            }

            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            Customer addedCustomer;
            try
            {
                addedCustomer = _services.AddCustomer(customer);
                if (addedCustomer == null)
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Adding customer was failed with error: {0}", ex.Message));
                return StatusCode(500, ex);
            }

            return Ok(addedCustomer);
        }

        // PUT api/<CustomerController>
        [HttpPut]
        public IActionResult Update([FromBody] Customer customer)
        {
            Customer updatedCustomer;
            try
            {
                updatedCustomer = _services.UpdateCustomer(customer);
                if (updatedCustomer == null)
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Updating customer was failed with error: {0}", ex.Message));
                return StatusCode(500, ex);
            }

            return Ok(updatedCustomer);
        }
    }
}
