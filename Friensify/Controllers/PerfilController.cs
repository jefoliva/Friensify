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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Friensify.Controllers
{
    public class PerfilController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FriensifyContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PerfilController(
            FriensifyContext context, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        // GET: PerfilController
        public async Task<IActionResult> Ver(string username)
        {
            
            if(string.IsNullOrEmpty(username))
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

                return View(vmusuariolog);
            }


            var usuario = await _context.Users.Include(p => p.Posts)
                .FirstOrDefaultAsync(id => id.UserName == username);

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

        public async  Task<IActionResult> CrearPost(PostInput post)
        {

            if(!ModelState.IsValid)
            {
                RedirectToAction("Ver", null);
            }

            
            if (!String.IsNullOrEmpty(post.ImagenPost?.FileName))
            {
                string wwwRoothRuta = _hostEnvironment.WebRootPath;
                string nombreArchivo = Path.GetFileNameWithoutExtension(post.ImagenPost.FileName);
                string extension = Path.GetExtension(post.ImagenPost.FileName);
                nombreArchivo = nombreArchivo + DateTime.Now.ToString("yymmssfff") + extension;
                post.URLImagen = nombreArchivo;

               
                string ruta = Path.Combine(wwwRoothRuta + "\\img", nombreArchivo);
                using (var fileStream = new FileStream(ruta, FileMode.Create))
                {
                    await post.ImagenPost.CopyToAsync(fileStream);
                }
            }

            var postDB = new Post
            {
                Contenido = post.Contenido,
                Fecha =  DateTime.Now,
                URLImagen = post.URLImagen,
                UserId = post.UserId
            };

            
            await _context.Post.AddAsync(postDB);
            await _context.SaveChangesAsync();

            return RedirectToAction("Ver");
           // return Content($"userid: {post?.UserId} fecha: {postDB?.Fecha} contenido: {post?.Contenido} url: {post?.URLImagen}");
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
