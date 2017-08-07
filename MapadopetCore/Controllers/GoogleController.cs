using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

using MapadopetCore.Models;
using System.Xml.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MapadopetCore.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MapadopetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class GoogleController : Controller
    {
        static HttpClient client = new HttpClient();

        private readonly IGoogleRepository _googleRepository;

        public GoogleController(IGoogleRepository googleRepository)
        {
            _googleRepository = googleRepository;
        }

        // GET: api/values
        [HttpGet]
        public string atualizar()
        {
                return _googleRepository.UpdatePlaces(); ;

        }

        [HttpGet]
        public async Task<List<GooglePlace>> GetPlaces(double lat, double lng)
        {
            GoogleLocation l = new GoogleLocation() { lat = lat, lng = lng };
            return await _googleRepository.GetPlaces(l);
        }

        [HttpGet("{id}")]
        public async Task<GooglePlace> Get(string id)
        {
            return await _googleRepository.Get(id);
        }
    }
}
