using Enitities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> ValidateUserAsync(string email, string password);
        Task<User> GetUserByIdAsync(string Id);
        Task CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(string Id, User user);
        Task<bool> DeleteUserAsync(string Id);
        Task<DeleteResult?> DeleteUsersManyAsync(string[] ids);

        
    }
}
