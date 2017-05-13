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
    public class PetRepository : IPetRepository
    {
        private readonly MapadopetContext _context = null;

        public PetRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);
        }

        public async Task<IEnumerable<Pet>> GetAllPets()
        {
            return await _context.Pets.Find( i => true).ToListAsync();
        }

        public Pet GetPet(string id)
        {
            var filter = Builders<Marca>.Filter.Eq("_id", ObjectId.Parse(id));
            //return _context.Marcas
            //                     .Find(filter)
            //                     .FirstOrDefault().pet;

            return _context.Marcas
                              .Find(filter)//.Project<Marca>(Builders<Marca>.Projection.Include(p => p.pet))
                              .FirstOrDefault().pet;

        }

        public void AddPet(Pet item)
        {
            double[] loc = new double[2];

            if (item.localizacao != null && item.localizacao.Length > 0 && item.localizacao.Contains(","))
            {
                double.TryParse(item.localizacao.Split(',')[0].ToString().Replace('.',','), out loc[0]);
                double.TryParse(item.localizacao.Split(',')[1].ToString().Replace('.', ','), out loc[1]);
                
            }

            Marca m = new Marca()
            {
                nome = item.nome,
                tipo = "pet",
                CreatedOn = DateTime.Now.ToString(),
                cord = loc,
                pet = item
            };

             _context.Marcas.InsertOneAsync(m);
             
        }

        public async Task<DeleteResult> RemovePet(string id)
        {
            return await _context.Pets.DeleteOneAsync(
                         Builders<Pet>.Filter.Eq("Id", id));
        }

        public async Task<UpdateResult> UpdatePet(string id, string nome)
        {
            var filter = Builders<Pet>.Filter.Eq(s => s._id, ObjectId.Parse(id));
            var update = Builders<Pet>.Update
                                .Set(s => s.nome, nome)
                                .CurrentDate(s => s.UpdatedOn);
            return await _context.Pets.UpdateOneAsync(filter, update);
        }

        public async Task<ReplaceOneResult> UpdatePet(string id, Pet item)
        {
            return await _context.Pets
                                 .ReplaceOneAsync(n => n._id.Equals(id)
                                                     , item
                                                     , new UpdateOptions { IsUpsert = true });
        }

        public async Task<DeleteResult> RemoveAllPets()
        {
            return await _context.Pets.DeleteManyAsync(new BsonDocument());
        }

        public async Task<ReplaceOneResult> UpdatePetDocument(string id, Pet item)
        {
            var Item = GetPet(id) ?? new Pet();
            item.nome = item.nome;
            item.UpdatedOn = DateTime.Now.ToString();

            return await UpdatePet(id, Item);
        }

        
    }
}
