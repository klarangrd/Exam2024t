using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Admin
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("adminid")]
        public int adminid { get; set; }

        [BsonElement("Navn")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Brugernavn skal tastes")]
        [BsonElement("Brugernavn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Kodeord skal indsættes")]
        [BsonElement("Kodeord")]
        public string Password { get; set; }
    }
}
