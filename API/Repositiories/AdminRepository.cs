using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using MongoDB.Bson;


namespace Serverapi.Repositories
{
    public class AdminRepository : IadminRepository
    {
        private const string ConnectionString = "mongodb+srv://magnusbbb:eksamenmagnus@eksamensprojekt.rpap7va.mongodb.net/";
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Admin> _collection;
        private Admin _currentAdmin;
        private Admin admin;

        public AdminRepository()
        {
            _mongoClient = new MongoClient(ConnectionString);
            _database = _mongoClient.GetDatabase("Circussummarum");
            _collection = _database.GetCollection<Admin>("Admins");
        }

        public void AddItem(Admin item)
        {
            _collection.InsertOne(item);
        }

        public void DeleteById(int id)
        {
            var filter = Builders<Admin>.Filter.Eq(a => a.adminid, id);
            _collection.DeleteOne(filter);
        }

        public List<Admin> GetAll()
        {
            return _collection.Find(Builders<Admin>.Filter.Empty).ToList();
        }

        public async Task<bool> CheckLoginAsync(string username, string password)
        {
            var filter = Builders<Admin>.Filter.And(
                Builders<Admin>.Filter.Eq("Username", username),
                Builders<Admin>.Filter.Eq("Password", password)
            );

            var admin = await _collection.Find(filter).FirstOrDefaultAsync();
            if (admin != null)
            {
                _currentAdmin = admin;
                return true;
            }
            return false;
        }

        public Task<Admin> GetCurrentAdmin()
        {

            return Task.FromResult(_currentAdmin);
        }

        public Task LogoutAdmin()
        {
            _currentAdmin = null;
            return Task.CompletedTask;
        }

        public async Task<Admin[]> GetAllAdmin()
        {
            var admins = await _collection.Find(Builders<Admin>.Filter.Empty).ToListAsync();
            return admins.ToArray();
        }
    }
}

