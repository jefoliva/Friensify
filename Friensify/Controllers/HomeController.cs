using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Friensify.Models;
using Microsoft.AspNetCore.Authorization;
using Friensify.ViewModels;

namespace Friensify.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FriensifyContext _context;

        public HomeController(ILogger<HomeController> logger, FriensifyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Busqueda()
        {
            var perfilesMasVisitados = (from v in _context.VisitasPerfil
                          join u in _context.Users
                          on v.IdUsuario equals u.Id
                          where v.Fecha.Date == DateTime.Now.Date
                          select new VisitasPerfilViewModel
                          {
                              Nombre = v.Nombre,
                              Username = u.UserName,
                              Visitas = v.Visitas,
                              ImagenURL = u.ImagenPerfil
                          }).OrderByDescending(v => v.Visitas)
                            .Take(5).ToList();

            return View(perfilesMasVisitados);
        }

        public ActionResult Presentacion()
        {
            return View();
        }
    }
}
