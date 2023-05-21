using Enitities;
using MongoDB.Driver;

namespace MongoDb.Repositories
{
    public interface IMongoUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string Id);
        Task CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(string Id, User user);
        Task<bool> DeleteUserAsync(string Id);
        Task<DeleteResult?> DeleteUserManyAsync(string[] ids);
        Task<User> ValidateUserAsync(string email, string password);        
    }
}
