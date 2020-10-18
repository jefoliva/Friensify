using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Friensify.Areas.Identity.Data;
using Friensify.Models;
using Friensify.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Friensify.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FriensifyContext _context;

        public PerfilController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            FriensifyContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        // POST api/<controller>
        [HttpGet("ver")]
        public async Task<ActionResult> Ver(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                var current_user = await _userManager.GetUserAsync(HttpContext.User);
                var usuariolog = await _context.Users.Include(p => p.Posts)
                .FirstOrDefaultAsync(id => id.UserName == current_user.UserName);

                var vmusuariolog = new PerfilViewModel
                {
                    UserId = usuariolog.Id,
                    Username = usuariolog.UserName,
                    Nombre = usuariolog.Nombre,
                    Apellido = usuariolog.Apellido,
                    Biografia = usuariolog.Biografia,
                    ImagenPerfil = usuariolog.ImagenPerfil,
                    Posts = usuariolog.Posts.OrderByDescending(p => p.Fecha).ToList()
                };

                var output = JsonConvert.SerializeObject(vmusuariolog);
                return new JsonResult(JObject.Parse(output));
            }

            
            var usuario = await _context.Users.Include(p => p.Posts)
                .FirstOrDefaultAsync(id => id.UserName == username);

            if (usuario == null)
                return BadRequest("Usuario Inválido");

            var vmusuario = new PerfilViewModel
            {
                UserId = usuario.Id,
                Username = usuario.UserName,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Biografia = usuario.Biografia,
                ImagenPerfil = usuario.ImagenPerfil,
                Posts = usuario.Posts.OrderByDescending(p => p.Fecha).ToList()
            };

            var output2 = JsonConvert.SerializeObject(vmusuario);
            return new JsonResult(JObject.Parse(output2));
        }
    }

}