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
    public class MarcaRepository : IMarcaRepository
    {
        private readonly MapadopetContext _context = null;

        public MarcaRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);
        }

        public async Task<IEnumerable<Marca>> GetAllMarcas()
        {
            return await _context.Marcas.Find(i => true).ToListAsync();
        }

        public async Task<IEnumerable<Marca>> GetMarcaAvalia()
        {
            return await _context.Marcas.Find(i => i.avaliado == false).ToListAsync();
        }

        public async Task<Marca> GetMarca(string id)
        {
            var filter = Builders<Marca>.Filter.Eq("Id", id);
            return await _context.Marcas
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task AddMarca(Marca item)
        {
            await _context.Marcas.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemoveMarca(string id)
        {
            return await _context.Marcas.DeleteOneAsync(
                         Builders<Marca>.Filter.Eq("Id", id));
        }

        public async Task<UpdateResult> UpdateMarca(string id, string nome)
        {
            var filter = Builders<Marca>.Filter.Eq(s => s._id, ObjectId.Parse(id));
            var update = Builders<Marca>.Update
                                .Set(s => s.nome, nome)
                                .CurrentDate(s => s.UpdatedOn);
            return await _context.Marcas.UpdateOneAsync(filter, update);
        }

        public async Task<ReplaceOneResult> UpdateMarca(string id, Marca item)
        {
            return await _context.Marcas
                                 .ReplaceOneAsync(n => n._id.Equals(id)
                                                     , item
                                                     , new UpdateOptions { IsUpsert = true });
        }

        public async Task<DeleteResult> RemoveAllMarcas()
        {
            return await _context.Marcas.DeleteManyAsync(new BsonDocument());
        }

        public async Task<ReplaceOneResult> UpdateMarcaDocument(string id, Marca m)
        {
            var item = await GetMarca(id) ?? new Marca();
            item.nome = m.nome;
            item.UpdatedOn = DateTime.Now.ToString();

            return await UpdateMarca(id, item);
        }
    }
}
