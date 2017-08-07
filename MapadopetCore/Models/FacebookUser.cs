using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MapadopetCore.Models
{
    public class FacebookUser
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string id { get; set; } = string.Empty;
        public string name { get; set; }
        public string CreatedOn { get; set; }
        


    }
}
