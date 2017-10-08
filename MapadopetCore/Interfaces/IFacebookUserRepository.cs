using System.Collections.Generic;
using System.Threading.Tasks;
using MapadopetCore.Models;
using MongoDB.Driver;

namespace MapadopetCore.Interfaces
{
    public interface IFacebookUserRepository
    {
        Task<IEnumerable<FacebookUser>> GetAllFacebookUser();
        Task<FacebookUser> GetFacebookUser(string id);
        void AddFacebookUser(FacebookUser item);
        Task<DeleteResult> RemoveFacebookUser(string id);
        Task<UpdateResult> UpdateFacebookUser(string id, string body);
        Task<ReplaceOneResult> UpdateFacebookUserDocument(string id, FacebookUser body);
        Task<DeleteResult> RemoveAllFacebookUser();
        Task<bool> checkLogin(string accessToken, string userID);
    }
}
