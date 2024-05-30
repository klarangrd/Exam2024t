using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Admin
    {
        [BsonId]
        public ObjectId Id { get; set; }

   
        public int adminid { get; set; }

       
        public string Name { get; set; }

      
        public string Email { get; set; }

        [Required(ErrorMessage = "Brugernavn skal tastes")]
      
        public string Username { get; set; }

        [Required(ErrorMessage = "Kodeord skal indsættes")]
    
        public string Password { get; set; }
    }
}
