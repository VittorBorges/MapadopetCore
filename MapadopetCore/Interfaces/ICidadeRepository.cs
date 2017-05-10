using System.Collections.Generic;
using System.Threading.Tasks;
using MapadopetCore.Models;
using MongoDB.Driver;

namespace MapadopetCore.Interfaces
{
    public interface ICidadeRepository
    {
        Task<IEnumerable<Cidade>> GetAllCidades();
        Task<Cidade> GetCidade(string id);
        void AddCidade(Cidade item);
        Task<DeleteResult> RemoveCidade(string id);
        Task<UpdateResult> UpdateCidade(string id, string body);
        Task<ReplaceOneResult> UpdateCidadeDocument(string id, Cidade body);
        Task<DeleteResult> RemoveAllCidades();

    }
}
