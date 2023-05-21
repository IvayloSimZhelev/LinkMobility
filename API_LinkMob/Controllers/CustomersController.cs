using Microsoft.AspNetCore.Mvc;
using MongoDb.Services;
using MongoDb;
using Enitities;
using System.Linq;
using API_LinkMob.Responses;

namespace API_LinkMob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly Seeder _seeder;

        public CustomersController(ICustomerService customerService, Seeder seeder)
        {
            _customerService = customerService;
            _seeder = seeder;
            _seeder.Seed();
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<Customer>>> GetCustomers(string? companyName = null, int page = 1, int pageSize = 10)
        {
            var totalRecords = await _customerService.GetCountCustomersAsync();
            if (totalRecords == 0)
            {
                return NotFound();
            }
            
            var pagedCustomers = await _customerService.GetAllCustomersByPageAsync(companyName, page, pageSize);

            // Create object pagingation 
            var response = new PaginationResponse<Customer>
            {
                Data = pagedCustomers,
                Page = page,
                PageSize = pageSize,
                TotalRecords = (int)totalRecords
            };

            return Ok(response);
        }

        // GET: api/users/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);


            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            await _customerService.CreateCustomerAsync(customer);
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        // PUT: api/users/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCustomer(string id, Customer customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }
            await _customerService.UpdateCustomerAsync(id, customerId);

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
