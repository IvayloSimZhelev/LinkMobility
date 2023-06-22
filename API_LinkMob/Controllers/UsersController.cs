
using API_LinkMob.Dto.Dtos;
using API_LinkMob.Extentions;
using Enitities;
using Microsoft.AspNetCore.Mvc;
using MongoDb;
using MongoDb.Repositories;

namespace API_LinkMob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMongoUserRepository _mongoUserRepository;
        private readonly Seeder _userSeeder;

        public UsersController(IMongoUserRepository mongoUserRepository, Seeder userSeeder)
        {
            _mongoUserRepository = mongoUserRepository;
            _userSeeder = userSeeder;
            _userSeeder.Seed();
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {

            var users = await _mongoUserRepository.GetAllUsersAsync();
            if(users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(Guid Id)
        {
            var user = await _mongoUserRepository.GetUserByIdAsync(Id);


            if(user == null)
            {
                return NotFound();
            }

            return user.asUserDto();
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginUser(User user)
        {
            UserDto? validUser = null;
            if(!string.IsNullOrEmpty(user?.Email) && !string.IsNullOrEmpty(user?.Password))
            {
                validUser = (await _mongoUserRepository.ValidateUserAsync(user.Email, user.Password)).asUserDto();
            }

            if(validUser == null)
            {
                return Unauthorized();
            }

            return Ok(validUser);
        }


        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.name,
                Email = userDto.email,
                Password = userDto.password,
            };

            await _mongoUserRepository.CreateUserAsync(user);
            return CreatedAtRoute(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(Guid Id, UserDto userDto)
        {
            var existingUser = await _mongoUserRepository.GetUserByIdAsync(Id);

            if(existingUser == null)
            {
                return NotFound();
            }

            existingUser.Id = userDto.id;
            existingUser.Name = userDto.name;
            existingUser.Email = userDto.email;
            existingUser.Password = userDto.password;

            await _mongoUserRepository.UpdateUserAsync(existingUser);

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var existingUser = await _mongoUserRepository.GetUserByIdAsync(Id);

            if(existingUser == null)
            {
                return NotFound();
            }

            await _mongoUserRepository.DeleteUserAsync(existingUser.Id);
            return NoContent();
        }
    }
}