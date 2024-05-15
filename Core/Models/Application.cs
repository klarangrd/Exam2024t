using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Core.Validation;

namespace Core.Models
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        public bool IsVolunteer { get; set; }



        [RequiredDropdownSelection("Vælg", ErrorMessage = "Førsteprioritet uge er påkrævet")]
        public string FirstpriorityWeek { get; set; }

        [RequiredDropdownSelection("Vælg", ErrorMessage = "Førsteprioritet Periode er påkrævet")] 
        public string FirstpriorityPeriod { get; set; }

        [RequiredDropdownSelection("Vælg", ErrorMessage = "Andenprioritet uge er påkrævet")] 
        public string SecondpriorityWeek { get; set; }

        [RequiredDropdownSelection("Vælg", ErrorMessage = "Andenprioritet Periode er påkrævet")] 
        public string SecondpriorityPeriod { get; set; }

        public bool IsApproved { get; set; }

        public Child Child { get; set; }
        
    }
}
