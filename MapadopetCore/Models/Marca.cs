using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MapadopetCore.Models
{
    public class Marca
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        //public DateTime UpdatedOn { get; set; } = DateTime.Now;
        //public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedOn { get; set; }
        public string CreatedOn { get; set; }
        public double[] cord { get; set; }
        public Pet pet { get; set; }
        public string id { get { return _id.ToString(); } }


    }
}
