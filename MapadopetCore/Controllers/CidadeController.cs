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
    public class CidadeController : Controller
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeController(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Cidade>> Get()
        {
            return GetCidadeInternal();
        }

        private async Task<IEnumerable<Cidade>> GetCidadeInternal()
        {
            return await _cidadeRepository.GetAllCidades();
        }
        
        [HttpGet("{id}")]
        public Cidade Get(string id)
        {
            return new Cidade() ;
        }

        private async Task<Cidade> GetPetByIdInternal(string id)
        {
            return await _cidadeRepository.GetCidade(id) ?? new Cidade();
        }
           
        [HttpPost]
        public void Post([FromBody] Cidade value)
        {
            _cidadeRepository.AddCidade(value);
        }
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Cidade value)
        {
            _cidadeRepository.UpdateCidadeDocument(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _cidadeRepository.RemoveCidade(id);
        }
    }
}

