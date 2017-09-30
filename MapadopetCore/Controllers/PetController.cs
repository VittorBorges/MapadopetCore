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

        public PetController(IPetRepository petRepository)
        {
           _petRepository = petRepository;
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
        public void Post([FromBody] Pet value)
        {
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

