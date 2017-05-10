using System.Collections.Generic;
using System.Threading.Tasks;
using MapadopetCore.Models;
using MongoDB.Driver;

namespace MapadopetCore.Interfaces
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> GetAllMarcas();
        Task<Marca> GetMarca(string id);
        Task AddMarca(Marca item);
        Task<DeleteResult> RemoveMarca(string id);
        Task<UpdateResult> UpdateMarca(string id, string body);
        Task<ReplaceOneResult> UpdateMarcaDocument(string id, Marca body);
        Task<DeleteResult> RemoveAllMarcas();
    }
}
