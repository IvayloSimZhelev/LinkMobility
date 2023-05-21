
using Enitities;
using Microsoft.AspNetCore.Mvc;
using MongoDb;
using MongoDb.Services;

namespace API_LinkMob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly Seeder _userSeeder;

        public UsersController(IUserService userService, Seeder userSeeder)
        {
            _userService = userService;
            _userSeeder = userSeeder;
            _userSeeder.Seed();
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            User? validUser = null;
            if (!string.IsNullOrEmpty(user?.Email) && !string.IsNullOrEmpty(user?.Password))
            {
                validUser = await _userService.ValidateUserAsync(user.Email, user.Password);
            }

            if (validUser == null)
            {
                return Unauthorized();
            }

            return Ok(validUser);
        }


        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            await _userService.CreateUserAsync(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, User userIn)
        {
            if (userIn == null)
            {
                return NotFound();
            }
            await _userService.UpdateUserAsync(id, userIn);

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}