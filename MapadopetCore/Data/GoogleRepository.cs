using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using MapadopetCore.Interfaces;
using MapadopetCore.Models;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MapadopetCore.Data
{
    public class GoogleRepository : IGoogleRepository
    {
        private readonly MapadopetContext _context = null;

        public GoogleRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);
        }

        public async Task<List<GooglePlace>> GetPlaces(GoogleLocation l)
        {
            try
            {
                var point = GeoJson.Point(GeoJson.Geographic(l.lat, l.lng));
                var locationQuery = new FilterDefinitionBuilder<GooglePlace>().Near(tag => tag.cord , point, 20000); 
                return await _context.Places.Find(locationQuery).Limit(100).ToListAsync();
            }
            catch (Exception ex)
            {
                //do something;
            }
            return null;
        }

        public string UpdatePlaces()
        {
            var c = _context.Cidades.Find(i => true).ToList();
            var total = 0;

            foreach (var item in c)
            {
                using (HttpClient client = new HttpClient())
                {
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={item.cord[0].ToString().Replace(',','.')},{item.cord[1].ToString().Replace(',', '.')}&radius={item.raio}&type=pet_store|veterinary_care&key=AIzaSyCfxAJhwYQ3NTgeQuxBCwaUyuuKCeHsNGI&language=PT-BR").Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    List<GooglePlace> data = JsonConvert.DeserializeObject<GooglePlaceReturn>(stringData).results;
                    _context.Places.InsertMany(data);
                    total += data.Count;
                }
            }

            

            return $" {total} locais adicionados ";
        }

        public async Task<GooglePlace> Get(string place_id)
        {
            try
            {
                var filter = Builders<GooglePlace>.Filter.Eq("place_id", place_id);
                return await _context.Places
                                     .Find(filter)
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                //do something;
            }
            return null;
        }

        
    }

}
