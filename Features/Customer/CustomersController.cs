using ItemShop.Features.Customer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Customer
{
    [Authorize]
    public class CustomersController: ApiController
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateModel model)
        {

            var id = await _service.Create(model.Name, model.Address, model.Phone, model.Gender);

            return Created(nameof(Create), id);

        }

        [HttpGet]
        public async Task<IEnumerable<CustomerListingModel>> GetAllCustomers()
        {
            return await _service.GetAllCustomers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailModel>> GetCustomerById(int id)
        {
            var customer = await _service.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, CustomerUpdateModel model)
        {
            var updated = await _service.UpdateCustomer(id, model.Name, model.Phone, model.Address, model.Gender);
            if (!updated)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _service.DeleteCustomer(id);
            if (!deleted)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
