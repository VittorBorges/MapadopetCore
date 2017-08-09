using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MapadopetCore.Interfaces;
using MapadopetCore.Models;
using MapadopetCore.Infrastructure;

using System;
using System.Collections.Generic;

namespace MapadopetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FacebookController : Controller
    {
        private readonly IFacebookUserRepository _FacebookUserRepository;

        public FacebookController(IFacebookUserRepository facebookUserRepository)
        {
            _FacebookUserRepository = facebookUserRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<FacebookUser>> Get()
        {
            return GetFacebookUserInternal();
        }

        private async Task<IEnumerable<FacebookUser>> GetFacebookUserInternal()
        {
            return await _FacebookUserRepository.GetAllFacebookUser();
        }
        
        [HttpGet("{id}")]
        public FacebookUser Get(string id)
        {
            return new FacebookUser() ;
        }

        private async Task<FacebookUser> GetPetByIdInternal(string id)
        {
            return await _FacebookUserRepository.GetFacebookUser(id) ?? new FacebookUser();
        }

        [HttpPost]
        public void Post([FromBody] FacebookUser value)
        {
            _FacebookUserRepository.AddFacebookUser(value);
        }

        [HttpPost]
        public void User([FromBody] FacebookUser value)
        {
            _FacebookUserRepository.AddFacebookUser(value);
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] FacebookUser value)
        {
            _FacebookUserRepository.UpdateFacebookUserDocument(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _FacebookUserRepository.RemoveFacebookUser(id);
        }
    }
}

