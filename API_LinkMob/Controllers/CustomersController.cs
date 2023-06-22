using API_LinkMob.Dto.Dtos;
using API_LinkMob.Extentions;
using API_LinkMob.Responses;
using Enitities;
using Microsoft.AspNetCore.Mvc;
using MongoDb;
using MongoDb.Repositories;

namespace API_LinkMob.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IMongoCustomerRepository _mongoCustomerRepository;
        private readonly Seeder _seeder;

        public CustomersController(IMongoCustomerRepository mongoCustomerRepository, Seeder seeder)
        {
            _mongoCustomerRepository = mongoCustomerRepository;
            _seeder = seeder;
            _seeder.Seed();
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<CustomerDto>>> GetCustomers(string? companyName = null, int page = 1, int pageSize = 10)
        {
            var totalRecords = await _mongoCustomerRepository.GetCountCustomersAsync();

            if(totalRecords == 0)
            {
                return NotFound();
            }

            var pagedCustomers = (await _mongoCustomerRepository.GetAllCustomersByPageAsync(companyName, page, pageSize))
                                 .Select(items => items.asDto());

            // Create object pagingation 
            var response = new PaginationResponse<CustomerDto>
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
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid Id)
        {
            var customer = await _mongoCustomerRepository.GetCustomerByIdAsync(Id);

            if(customer == null)
            {
                return NotFound();
            }

            return customer.asDto();
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                Id = customerDto.Id,
                CompanyName = customerDto.CompanyName,
                Address = customerDto.Address,
                Country = customerDto.Country,
                State = customerDto.State,
                Invoice = new List<Invoice>(),
                NumberOfInvoices = customerDto.NumberOfInvoices,
                SubscriptionState = customerDto.SubscriptionState
            };

            await _mongoCustomerRepository.CreateCustomerAsync(customer);
            return CreatedAtRoute(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/users/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, CustomerDto customer)
        {
            var existingCustomer = await _mongoCustomerRepository.GetCustomerByIdAsync(id);
            if(existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.CompanyName = customer.CompanyName;
            existingCustomer.Address = customer.Address;
            existingCustomer.Country = customer.Country;
            existingCustomer.State = customer.State;
            existingCustomer.NumberOfInvoices = customer.NumberOfInvoices;
            existingCustomer.SubscriptionState = customer.SubscriptionState;

            await _mongoCustomerRepository.UpdateCustomerAsync(existingCustomer);

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCustomer(Guid Id)
        {
            var existingCustomer = await _mongoCustomerRepository.GetCustomerByIdAsync(Id);
            if(existingCustomer == null)
            {
                return NotFound();
            }


            await _mongoCustomerRepository.DeleteCustomerAsync(existingCustomer.Id);
            return NoContent();
        }
    }
}
