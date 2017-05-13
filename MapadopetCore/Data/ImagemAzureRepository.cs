using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using MapadopetCore.Interfaces;
using MapadopetCore.Models;
using MongoDB.Bson;


namespace MapadopetCore.Data
{
    public class ImagemAzureRepository : IImagemRepository
    {
        private readonly MapadopetContext _context = null;

        public ImagemAzureRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);
        }

        public void AddImagem(Imagem item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Imagem>> GetAllImagens(string petId)
        {
            throw new NotImplementedException();
        }

        public Imagem GetImagem(string petId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveImagem(string id)
        {
            throw new NotImplementedException();
        }
    }
}
