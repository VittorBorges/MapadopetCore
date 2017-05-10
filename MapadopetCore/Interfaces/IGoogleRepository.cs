using System.Collections.Generic;
using System.Threading.Tasks;
using MapadopetCore.Models;
using MongoDB.Driver;

namespace MapadopetCore.Interfaces
{
    public interface IGoogleRepository
    {
        Task<List<GooglePlace>> GetPlaces(GoogleLocation l);
        string UpdatePlaces();
        Task<GooglePlace> Get(string place_id);

    }
    
    
}
