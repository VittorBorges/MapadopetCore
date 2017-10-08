using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MapadopetCore.Models
{
    public class Pet
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string nome { get; set; } = string.Empty;
        public int tipo { get; set; } 
        public string imagem { get; set; } = "images//petpopup1.jpg";
        public string descricao { get; set; } = string.Empty;
        public string contatos { get; set; } = string.Empty;
        public string localizacao { get; set; } = string.Empty;
        //public DateTime UpdatedOn { get; set; } = DateTime.Now;
        //public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedOn { get; set; }
        public string CreatedOn { get; set; }
        public string userid { get; set; } 
        public string username { get; set; }
        public string useremail { get; set; }
        public string tipoString
        {
            get
            {
                string t = "";
                switch (tipo)
                {
                    case 0:
                        t = "Pet Perdido";
                        break;
                    case 1:
                        t = "Pet para adoção";
                        break;
                    case 2:
                        t = "Pet visto";
                        break;

                }
                return t;
            } }
        [BsonIgnore]
        public string accessToken { get; set; }
        

    }
}
