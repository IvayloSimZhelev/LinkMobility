using Enitities;
using Microsoft.Extensions.Configuration;
using MongoDb.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoUserRepository _mongoUserRepository;
        private readonly IConfiguration _configuration;


        public UserService(IMongoUserRepository mongoUserRepository, IConfiguration configuration)
        {
            _mongoUserRepository = mongoUserRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _mongoUserRepository.GetAllUsersAsync();
        }
        public async Task<User> GetUserByIdAsync(string Id)
        {
            return await _mongoUserRepository.GetUserByIdAsync(Id);
        }
        public async Task CreateUserAsync(User user)
        {
            await _mongoUserRepository.CreateUserAsync(user);
        }
        public async Task<bool> UpdateUserAsync(string Id, User user)
        {
            return await _mongoUserRepository.UpdateUserAsync(Id, user);
        }
        public async Task<bool> DeleteUserAsync(string Id)
        {
            return await _mongoUserRepository.DeleteUserAsync(Id);
        }

        public async Task<DeleteResult?> DeleteUsersManyAsync(string[] ids)
        {
            return await _mongoUserRepository.DeleteUserManyAsync(ids);
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            return await _mongoUserRepository.ValidateUserAsync(email,password);
        }
    }
}
