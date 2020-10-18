using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friensify.Areas.Identity.Data;
using Friensify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friensify.Controllers.Api
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // POST api/<controller>
        [HttpPost("SignIn")]
        public async Task<ActionResult> SignIn(LoginModel usuario)
        {
            if(ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(usuario.Username, usuario.Password, false, false);
                

                if (resultado.Succeeded)
                {
                    var user = _userManager.Users.SingleOrDefault(u => u.UserName == usuario.Username);



                    return Ok(new { Id = user.Id, Username = user.UserName, Nombre = user.NombreCompleto()});
                }
            }

            return BadRequest("Usuario invalido");


        }
    }
}
