using System.Collections.Generic;
using System.Threading.Tasks;
using MapadopetCore.Models;
using MongoDB.Driver;

namespace MapadopetCore.Interfaces
{
    public interface IImagemRepository
    {
        Task<IEnumerable<Imagem>> GetAllImagens(string petId);
        Imagem GetImagem(string petId);
        void AddImagem(Imagem item);
        Task<string> RemoveImagem(string id);
    }
}
