using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapadopetCore.Models
{
    public class Imagem
    {
        public string idPet { get; set; }
        public System.IO.Stream imgStream { get; set; }
        public string caminho { get; set; }

    }
}
