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
    public class CidadeRepository : ICidadeRepository
    {
        private readonly MapadopetContext _context = null;

        public CidadeRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);
        }

        public async Task<IEnumerable<Cidade>> GetAllCidades()
        {
            return await _context.Cidades.Find( i => true).ToListAsync();
        }

        public async Task<Cidade> GetCidade(string id)
        {
            var filter = Builders<Cidade>.Filter.Eq("Id", id);
            return await _context.Cidades
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public void AddCidade(Cidade item)
        {
           
             _context.Cidades.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemoveCidade(string id)
        {
            return await _context.Cidades.DeleteOneAsync(
                         Builders<Cidade>.Filter.Eq("Id", id));
        }

        public async Task<UpdateResult> UpdateCidade(string id, string nome)
        {
            var filter = Builders<Cidade>.Filter.Eq(s => s._id, ObjectId.Parse(id));
            var update = Builders<Cidade>.Update
                                .Set(s => s.nome, nome)
                                .CurrentDate(s => s.UpdatedOn);
            return await _context.Cidades.UpdateOneAsync(filter, update);
        }

        public async Task<ReplaceOneResult> UpdateCidade(string id, Cidade item)
        {
            return await _context.Cidades 
                                 .ReplaceOneAsync(n => n._id.Equals(id)
                                                     , item
                                                     , new UpdateOptions { IsUpsert = true });
        }

        public async Task<DeleteResult> RemoveAllCidades()
        {
            return await _context.Cidades.DeleteManyAsync(new BsonDocument());
        }

        public async Task<ReplaceOneResult> UpdateCidadeDocument(string id, Cidade item)
        {
            var Item = await GetCidade(id) ?? new Cidade();
            item.nome = item.nome;
            item.UpdatedOn = DateTime.Now.ToString();

            return await UpdateCidade(id, Item);
        }

        
    }
}
