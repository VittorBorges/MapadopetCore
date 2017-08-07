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
                var locationQuery = new FilterDefinitionBuilder<GooglePlace>().Near(tag => tag.cord, point, 200000000000000);
                return await _context.Places.Find(locationQuery).Limit(1000).ToListAsync();
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
                    //https://maps.googleapis.com/maps/api/place/nearbysearch/json?pagetoken=CqQCFQEAAMwvyaE6MbU0zKHsFaEq7qAykFapxKN1jm5x44yntAIGM33Q7d844rTxpjxIlEYjp2AO2rbd3Ucfr7o3-GWlOZ2zxFFs2m0KUgy0pgbpKP4k8s2Xl4vjbxQPlSSBPae1G7vnxxpJGw-pDl8_MQv2N66bQx74UVN4audGRrso8dnSGrGMvKWke-Gc_CPqF-1K6QzkTt8gDRLkDlnUz56gCrCsq-vso8HkVO3e6_vb0d0WgCEtVpubuqaG4fC3syL1N_6Q_mDx31Q__4G5h1H9zpGIZg4LQS2kkE5zXquIcuJmGgTu_4tS-hGwTibIrWb_o1CXUvCK5_32mApqxtWAWTEpxYoTKZZ0pgTNw0CqyA5m6hFBzEDVR_DeLf_mgKECHBIQHHaCGdJkTpxIhrn_3Hbp4hoUqytgEWc3L7i-Vk-uvZEgI_5Ua4Y&location=-10.185, -48.328&radius=5000&type=pet_store|veterinary_care&key=AIzaSyCfxAJhwYQ3NTgeQuxBCwaUyuuKCeHsNGI&language=PT-BR
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
