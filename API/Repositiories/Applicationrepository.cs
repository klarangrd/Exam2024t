using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using System.Net.NetworkInformation;
using Core;
using Serverapi.Repositories;
using Core.Models;

namespace Serverapi.repositories
{

    public class Applicationrepository : Iapplycationrepository
    {

        const string connectionstring = "mongodb+srv://magnusbbb:eksamenmagnus@eksamensprojekt.rpap7va.mongodb.net/";

        //MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionUri);

        IMongoClient mongoClient;

        IMongoDatabase database;

        IMongoCollection<Application> collection;



        public Applicationrepository()
        {

            mongoClient = new MongoClient(connectionstring);

            database = mongoClient.GetDatabase("Circussummarum");

            collection = database.GetCollection<Application>("Applications");

        }

        public void AddItem(Application item)
        {
            var max = 0;
            if (collection.Count(Builders<Application>.Filter.Empty) > 0)
            {
                max = collection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.Child.ChildId).Limit(1).ToList()[0].Child.ChildId;
            }
            item.Child.ChildId = max + 1;

            collection.InsertOne(item);
        }

        public void DeleteById(int id)
        {

        }

        public List<Application> GetAll()
        {
            return collection.Find(Builders<Application>.Filter.Empty).ToList();
        }

        /*
        public void UpdateItem(Application item)
        {

        }
        */
        /*

        public void AddApplication(Application newapplication)
        {
            collection.InsertOne(newapplication);
        }
        */
    }
}






