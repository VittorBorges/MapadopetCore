using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MapadopetCore.Interfaces;

namespace MapadopetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPetRepository _petRepository;

        public HomeController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public IActionResult Index()
        {
            ViewData["sqPet"] = "1";
            ViewData["ogImage"] = "http://www.mapadopet.com.br/images/imgCompartilhamento.jpg";
            ViewData["ogUrl"] = "http://www.mapadopet.com.br";
            return View();
        }

        public IActionResult Pet(string id)
        {
            var p = _petRepository.GetPet(id);
            ViewData["ogImage"] = p.imagem;
            ViewData["sqPet"] = id;
            ViewData["ogUrl"] = "http://www.mapadopet.com.br/home/pet/" + id;
            return View("index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
