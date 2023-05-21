using Enitities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb.Repositories
{
    public class MongoUserRepository : IMongoUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public MongoUserRepository(IMongoClient client)
        {
            var database = client.GetDatabase("LinkMobilityDB");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _users.Find(u => true).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string Id)
        {
            return await _users.Find(u => u.Id == Id).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<bool> UpdateUserAsync(string Id, User user)
        {
            var updateResult = await _users.UpdateOneAsync(
                u => u.Id == Id,
                Builders<User>.Update
                    .Set(u => u.Name, user.Name)
                    .Set(u => u.Email, user.Email)
                    .Set(u => u.Password, user.Password)
            );

            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUserAsync(string Id)
        {
            var deleteResult = await _users.DeleteOneAsync(u => u.Id == Id);
            return deleteResult.DeletedCount > 0;
        }

        public async Task<DeleteResult?> DeleteUserManyAsync(string[] ids)
        {
            try
            {
                var filter = Builders<User>.Filter.In("_id", ids);
                return  await _users.DeleteManyAsync(filter);
            }
            catch (Exception ex)
            {
                return null;
                // log need to be here;
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            return await _users.Find(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }
    }
}
