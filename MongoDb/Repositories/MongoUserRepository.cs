using Enitities;
using MongoDB.Driver;

namespace MongoDb.Repositories
{
    public class MongoUserRepository : IMongoUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private const string _collectionName = "Users";
        private const string _databaseName = "LinkMobilityDB";

        public MongoUserRepository(IMongoClient client)
        {
            var database = client.GetDatabase(_databaseName);
            _users = database.GetCollection<User>(_collectionName);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _users.Find(u => true).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(user => user.Id, Id);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);
            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task DeleteUserAsync(Guid Id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(user => user.Id, Id);
            await _users.DeleteOneAsync(filter);
        }

        public async Task<DeleteResult?> DeleteUserManyAsync(string[] ids)
        {
            try
            {
                var filter = Builders<User>.Filter.In("_id", ids);
                return await _users.DeleteManyAsync(filter);
            }
            catch(Exception ex)
            {
                return null;
                //log need to be here;
            }
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            return await _users.Find(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }
    }
}
