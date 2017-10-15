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
        public int tipo { get; set; } = 0;
        public DateTime UpdatedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public double[] cord { get; set; }
        public Pet pet { get; set; }
        public string id { get { return _id.ToString();  } }
        public bool avaliado { get; set; } = false;

        //[BsonIgnore]
        //public tipoMarca enumTipo { get {
        //        //return EnumUtils.Parse<tipoMarca>(tipo.ToString()).Value ;
        //        return tipoMarca  [tipo];
        //    } 

        //    set {
        //        tipo = (int)value;
        //    }
        //} 


    }

    //public enum tipoMarca
    //{
    //    perdido,
    //    adocao,
    //    abandonado
    //};

}
