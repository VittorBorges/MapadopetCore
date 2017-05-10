using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MapadopetCore.Models;

namespace MapadopetCore.Data
{
    public class MapadopetContext
    {
        private readonly IMongoDatabase _database = null;

        public MapadopetContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Marca> Marcas
        {
            get
            {
                return _database.GetCollection<Marca>("Marca");
            }
        }

        public IMongoCollection<Pet> Pets
        {
            get
            {
                return _database.GetCollection<Pet>("Pet");
            }
        }

        public IMongoCollection<GooglePlace> Places
        {
            get
            {
                return _database.GetCollection<GooglePlace>("GooglePlace");
            }
        }

        public IMongoCollection<Cidade> Cidades
        {
            get
            {
                return _database.GetCollection<Cidade>("Cidade");
            }
        }

    }
}
