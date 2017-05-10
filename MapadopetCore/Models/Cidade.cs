using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MapadopetCore.Models
{
    public class Cidade
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string UpdatedOn { get; set; }
        public string CreatedOn { get; set; }
        public int UserId { get; set; } = 0;
        public double[] cord { get; set; }
        public int raio { get; set; }


    }
}
