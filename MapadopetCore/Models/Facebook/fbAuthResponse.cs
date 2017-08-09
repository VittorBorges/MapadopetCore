using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapadopetCore.Models.Facebook
{
    public class fbAuthResponse
    {
        public authResponse authResponse { get; set; }
        public string status { get; set; }
    }

    public class authResponse
    {
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string signedRequest { get; set; }
        public string userId { get; set; }
    }

}
