using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using MapadopetCore.Interfaces;
using MapadopetCore.Models;
using MongoDB.Bson;
using Flurl.Http;

namespace MapadopetCore.Data
{
    public class FacebookUserRepository : IFacebookUserRepository
    {
        
        private readonly MapadopetContext _context = null;

        public FacebookUserRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);
        }

        public async Task<IEnumerable<FacebookUser>> GetAllFacebookUser()
        {
            return await _context.FacebookUsers.Find( i => true).ToListAsync();
        }

        public async Task<FacebookUser> GetFacebookUser(string id)
        {
            var filter = Builders<FacebookUser>.Filter.Eq("Id", id);
            return await _context.FacebookUsers
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public void AddFacebookUser(FacebookUser item)
        {
           
             _context.FacebookUsers.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemoveFacebookUser(string id)
        {
            return await _context.FacebookUsers.DeleteOneAsync(
                         Builders<FacebookUser>.Filter.Eq("Id", id));
        }

        public async Task<UpdateResult> UpdateFacebookUser(string id, string name)
        {
            var filter = Builders<FacebookUser>.Filter.Eq(s => s._id, ObjectId.Parse(id));
            var update = Builders<FacebookUser>.Update
                                .Set(s => s.name, name);
            return await _context.FacebookUsers.UpdateOneAsync(filter, update);
        }

        public async Task<ReplaceOneResult> UpdateFacebookUserDocument(string id, FacebookUser item)
        {
            return await _context.FacebookUsers
                                 .ReplaceOneAsync(n => n._id.Equals(id)
                                                     , item
                                                     , new UpdateOptions { IsUpsert = true });
        }

        public async Task<DeleteResult> RemoveAllFacebookUser()
        {
            return await _context.FacebookUsers.DeleteManyAsync(new BsonDocument());
        }

        public async Task<ReplaceOneResult> UpdateCidadeDocument(string id, FacebookUser item)
        {
            var Item = await GetFacebookUser(id) ?? new FacebookUser();
            item.name  = item.name;
            item.CreatedOn = DateTime.Now.ToString();

            return await UpdateFacebookUserDocument(id, Item);
        }
        
        public async Task<bool> checkLogin(string accessToken, string userID)
        {
            string url = string.Concat("https://graph.facebook.com/me?fields=id&access_token=",accessToken);
            var responseString = await url
    .PostUrlEncodedAsync(new { z = ""})
    .ReceiveString();
            return responseString.Split('"')[3] == userID;
        }

    }
}
