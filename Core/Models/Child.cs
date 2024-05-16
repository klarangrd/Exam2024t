using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Models
{
    public class Child
    {
        public ObjectId Id { get; set; }

        public int ChildId { get; set; }

        [Required(ErrorMessage = "Navn barn er påkrævet")]
        public string ChildName { get; set; }

        [Range(0, 18, ErrorMessage = "Alder skal være mellem 0 og 18")]
        [Required(ErrorMessage = "Du skal skrive dit barns alder")]
        public int ChildAge { get; set; }

        [Required(ErrorMessage = "Du skal skrive dit barns trøje størrelse")]
        public String ClothingSize { get; set; }

        public string Comment { get; set; }

        public bool Beenbefore { get; set; }

        public string Interest {  get; set; }

        public Volunteer Volunteer { get; set; }

       
    }
}
