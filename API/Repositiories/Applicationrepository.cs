using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Serverapi.Repositories;

namespace Serverapi.Repositories
{
    public class ApplicationRepository : Iapplicationrepository
    {
        private const string connectionString = "mongodb+srv://magnusbbb:eksamenmagnus@eksamensprojekt.rpap7va.mongodb.net/";
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Application> _collection;

        public ApplicationRepository()
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase("Circussummarum");
            _collection = _database.GetCollection<Application>("Applications");
        }

        public async Task Add(Application application)
        {
            await _collection.InsertOneAsync(application);
        }

        public async Task<Application[]> GetAllApplications()
        {
            var applications = await _collection.Find(new BsonDocument()).ToListAsync();
            return applications.ToArray();
        }

        public async Task UpdateApplication(Application application)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.Id, application.Id);
            var result = await _collection.ReplaceOneAsync(filter, application);
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                Console.WriteLine($"Application with Id {application.Id} updated successfully.");
            }
            else
            {
                Console.WriteLine("No application found to update.");
            }
        }

        public async Task<Application[]> GetQueuedApplications()
        {
            var filter = Builders<Application>.Filter.Eq(a => a.IsApproved, false);
            var applications = await _collection.Find(filter).ToListAsync();
            Console.WriteLine($"Fetched {applications.Count} Queued Applications");
            return applications.ToArray();
        }

        public async Task<Application[]> GetApprovedApplications()
        {
            var filter = Builders<Application>.Filter.Eq(a => a.IsApproved, true);
            var applications = await _collection.Find(filter).ToListAsync();
            Console.WriteLine($"Fetched {applications.Count} Approved Applications");
            return applications.ToArray();
        }

        public async Task DeleteApplication(ObjectId applicationId)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.Id, applicationId);
            var result = await _collection.DeleteOneAsync(filter);
            if (result.IsAcknowledged && result.DeletedCount > 0)
            {
                Console.WriteLine($"Application with Id {applicationId} deleted successfully.");
            }
            else
            {
                Console.WriteLine("No application found to delete.");
            }
        }
    }
}
