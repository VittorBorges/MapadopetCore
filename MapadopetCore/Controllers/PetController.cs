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
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly IFacebookUserRepository _facebookUserRepository;

        public PetController(IPetRepository petRepository, IFacebookUserRepository facebookUserRepository)
        {
            _petRepository = petRepository;
            _facebookUserRepository = facebookUserRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Pet>> Get()
        {
            return GetPetInternal();
        }

        private async Task<IEnumerable<Pet>> GetPetInternal()
        {
            return await _petRepository.GetAllPets();
        }
        
        [HttpGet("{id}")]
        public Pet Get(string id)
        {
            
            return  _petRepository.GetPet(id);
        }


        private  Pet GetPetByIdInternal(string id)
        {
            return  _petRepository.GetPet(id) ?? new Pet();
        }
           
        [HttpPost]
        public async void Post([FromBody] Pet value)
        {
            if (await _facebookUserRepository.checkLogin(value.accessToken, value.userid))
            _petRepository.AddPet(value);
        }
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Pet value)
        {
            _petRepository.UpdatePetDocument(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _petRepository.RemovePet(id);
        }
    }
}

