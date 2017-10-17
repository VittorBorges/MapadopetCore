using System.Collections.Generic;
using System.Threading.Tasks;
using MapadopetCore.Models;
using MongoDB.Driver;

namespace MapadopetCore.Interfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllPets();
        Pet GetPet(string id);
        void  AddPet(Pet item);
        Task<DeleteResult> RemovePet(string id);
        Task<UpdateResult> UpdatePet(string id, string body);
        Task<ReplaceOneResult> UpdatePetDocument(string id, Pet body);
        Task<DeleteResult> RemoveAllPets();
        Task<UpdateResult> Desativar(Pet item);
        Task<UpdateResult> Ativar(Pet item);
    }
}
