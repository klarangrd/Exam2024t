using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using System.Net.NetworkInformation;

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
            var max = 0;
            if (_collection.CountDocuments(Builders<Application>.Filter.Empty) > 0)
            {
                max = _collection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.appId).Limit(1).ToList()[0].appId;
            }
            application.appId = max + 1;

            await _collection.InsertOneAsync(application);
        }

        public async Task<Application[]> GetAllApplications()
        {
            var applications = await _collection.Find(new BsonDocument()).ToListAsync();
            return applications.ToArray();
        }

        public async Task UpdateApplication(int id, Application application)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.appId, id);
            var update = Builders<Application>.Update
                        .Set(a => a.IsApproved, application.IsApproved)
                        .Set(a => a.FirstpriorityWeek, application.FirstpriorityWeek)
                        .Set(a => a.FirstpriorityPeriod, application.FirstpriorityPeriod)
                        .Set(a => a.Child.ChildName, application.Child.ChildName)
                        .Set(a => a.Child.ChildAge, application.Child.ChildAge)
                        .Set(a => a.Child.ClothingSize, application.Child.ClothingSize)
                        .Set(a => a.Child.Volunteer.Name, application.Child.Volunteer.Name)
                        .Set(a => a.Child.Volunteer.Kræwnr, application.Child.Volunteer.Kræwnr)
                        .Set(a => a.Child.Volunteer.Email, application.Child.Volunteer.Email);

            await _collection.UpdateOneAsync(filter, update);
        }


        public async Task<Application[]> GetQueuedApplications()
        {
            var filter = Builders<Application>.Filter.Eq(a => a.IsApproved, false);
            var applications = await _collection.Find(filter).ToListAsync();
            return applications.ToArray();
        }

        public async Task<Application[]> GetApprovedApplications()
        {
            var filter = Builders<Application>.Filter.Eq(a => a.IsApproved, true);
            var applications = await _collection.Find(filter).ToListAsync();
            return applications.ToArray();
        }

        public void DeleteApplication(int Id)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.appId, Id);
            _collection.DeleteOne(filter);
        }

        public async Task<List<string>> GetVolunteerEmails()
        {
            var filter = Builders<Application>.Filter.And(
                Builders<Application>.Filter.Eq(app => app.Issignedupfornewsletter, true)
            );

            var applications = await _collection.Find(filter).ToListAsync();

            var emails = applications
                .Select(app => app.Child?.Volunteer?.Email)
                .Where(email => !string.IsNullOrEmpty(email))
                .ToList();

            return emails;
        }

        //get an application by its id from the database
        public async Task<Application> GetApplicationById(int id)
        {
            //filter to find application with the id
            var filter = Builders<Application>.Filter.Eq(a => a.appId, id);

            //using filter to find the first application that matches the filter criteria
            var application = await _collection.Find(filter).FirstOrDefaultAsync();

            //returning the application or null if no application was found
            return application;
        }
    }
}
