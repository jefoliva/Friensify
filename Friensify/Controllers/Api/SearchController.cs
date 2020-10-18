using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friensify.Areas.Identity.Data;
using Friensify.Models;
using Friensify.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Friensify.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FriensifyContext _context;

        public SearchController(FriensifyContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/Search
        [HttpGet]
        public async Task<IEnumerable<BusquedaViewModel>> Buscar(string query = null)
        {
            var usuariosDB = await _context.Users.ToListAsync();
            var usuariosVM = new List<BusquedaViewModel>();
            IEnumerable<ApplicationUser> resultado = null;

            if (!String.IsNullOrWhiteSpace(query))
            {
                resultado = usuariosDB.Where(c => {
                    return (c.UserName.Contains(query) || 
                            c.NombreCompleto().Contains(query) ||
                            c.Email.Contains(query));
                }).Distinct().ToList();


                foreach(var user in resultado)
                {
                    var usuario = new BusquedaViewModel
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        Email = user.Email,
                        URLImagen = user.ImagenPerfil
                    };

                    usuariosVM.Add(usuario);
                }

                return usuariosVM;
            }

            if(resultado == null)
            {
                var usuarios = new List<BusquedaViewModel>();

                foreach (var user in usuariosDB)
                {
                    var usuario = new BusquedaViewModel
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        Email = user.Email,
                        URLImagen = user.ImagenPerfil
                    };

                    usuarios.Add(usuario);
                }

                return usuarios;
            }

            return usuariosVM;
        }

        // GET: api/Search/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Search
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Search/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
