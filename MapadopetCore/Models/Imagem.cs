using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapadopetCore.Models
{
    public class Imagem
    {
        public string idPet { get; set; }
        public string fileName { get; set; }
        public System.IO.Stream imgStream { get; set; }
        public string patch { get; set; }

    }
}
