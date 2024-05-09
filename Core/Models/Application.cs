using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core.Models
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        public bool IsVolunteer { get; set; }


       public string FirstpriorityWeek { get; set; }

        public string FirstpriorityPeriod { get; set; }

        public string SecondpriorityWeek { get; set; }

        public string SecondpriorityPeriod { get; set; }

        public Child Child { get; set; }
        
    }
}
