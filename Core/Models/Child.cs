using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Child
    {
        public ObjectId Id { get; set; }

        public int ChildId { get; set; }

        public string ChildName { get; set; }

        public int ChildAge { get; set; }

        public String ClothingSize { get; set; }

        public string Comment { get; set; }

        public bool Beenbefore { get; set; }

        public string Interest {  get; set; }

        public Volunteer Volunteer { get; set; }

       
    }
}
