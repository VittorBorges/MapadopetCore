using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using MapadopetCore.Interfaces;
using MapadopetCore.Models;
using MapadopetCore.Infrastructure;



namespace MapadopetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MarcaController : Controller
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaController(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Marca>> Get()
        {
            return GetMarcaInternal();
        }

        private async Task<IEnumerable<Marca>> GetMarcaInternal()
        {
            return await _marcaRepository.GetAllMarcas();
        }

        [HttpGet("{MapBounds}")]
        public async Task<List<Marca>> GetAllMarcas(MapBounds m)
        {
            return await _marcaRepository.GetAllMarcas(m);
        }
        
        [HttpGet("{id}")]
        public Task<Marca> Get(string id)
        {
            return GetMarcaByIdInternal(id);
        }

        private async Task<Marca> GetMarcaByIdInternal(string id)
        {
            return await _marcaRepository.GetMarca(id) ?? new Marca();
        }
           
        [HttpPost]
        public void Post([FromBody] Marca value)
        {
            _marcaRepository.AddMarca(value);
        }
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Marca value)
        {
            _marcaRepository.UpdateMarcaDocument(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _marcaRepository.RemoveMarca(id);
        }

        [HttpPost]
        public void lostpet([FromBody] Marca value)
        {
            _marcaRepository.AddMarca(value);
        }

        
    }
}
