using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friensify.Areas.Identity.Data;
using Friensify.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Friensify.ViewModels;

namespace Friensify.Controllers
{
    public class PerfilController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FriensifyContext _context;

        public PerfilController(FriensifyContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: PerfilController
        public async Task<IActionResult> Ver(string username)
        {
            
            if(string.IsNullOrEmpty(username))
            {
                var current_user = await _userManager.GetUserAsync(HttpContext.User);

                var vmuser = new PerfilViewModel
                {
                    UserId = current_user.Id,
                    Username = current_user.UserName,
                    Nombre = current_user.Nombre,
                    Apellido = current_user.Apellido,
                    Biografia = current_user.Biografia,
                    ImagenPerfil = current_user.ImagenPerfil
                };

                return View(vmuser);
            }


            var usuario = await _context.Users.FirstOrDefaultAsync(id => id.UserName == username);

            var vmusuario = new PerfilViewModel
            {
                UserId = usuario.Id,
                Username = usuario.UserName,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Biografia = usuario.Biografia,
                ImagenPerfil = usuario.ImagenPerfil
            };

            return View(vmusuario);
        }

        public async Task<IActionResult> Lista()
        {

            var users = await _context.Users.ToListAsync();

            var list = "";

            foreach (var user in users)
                list += user.UserName + '\n';

            return Content(list);
        }

        public ActionResult Perfil()
        {


            return View();
        }

        public ActionResult Actualizar()
        {

            return View();
        }

        // GET: PerfilController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PerfilController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerfilController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PerfilController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PerfilController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PerfilController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PerfilController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
