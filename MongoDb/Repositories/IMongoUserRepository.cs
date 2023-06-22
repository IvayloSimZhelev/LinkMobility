using Enitities;
using MongoDB.Driver;

namespace MongoDb.Repositories
{
    public interface IMongoUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid Id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid Id);
        Task<DeleteResult?> DeleteUserManyAsync(string[] ids);
        Task<User> ValidateUserAsync(string email, string password);
    }
}
