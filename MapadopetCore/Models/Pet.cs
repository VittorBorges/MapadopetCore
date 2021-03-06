﻿using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MapadopetCore.Models
{
    public class Pet
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        public string imagem { get; set; } = "images//petpopup1.jpg";
        public string descricao { get; set; } = string.Empty;
        public string contatos { get; set; } = string.Empty;
        public string localizacao { get; set; } = string.Empty;
        //public DateTime UpdatedOn { get; set; } = DateTime.Now;
        //public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedOn { get; set; }
        public string CreatedOn { get; set; }
        public int UserId { get; set; } = 0;
        
    }
}
