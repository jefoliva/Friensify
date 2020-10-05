using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Friensify.Areas.Identity.Data;
using Friensify.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Friensify.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly FriensifyContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public IndexModel(
            FriensifyContext context,
            IWebHostEnvironment hostEnvironment,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "Nombre")]
            public string Nombre { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Apellido")]
            public string Apellido { get; set; }

            [Phone]
            [Display(Name = "Numero de telefono")]
            public string PhoneNumber { get; set; }

            [DataType(DataType.Text)]
            public string ImagenPerfil { get; set; }

            [Display(Name = "Cambiar imagen de perfil")]
            public IFormFile ImagenArchivo { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var usuario = await _context.Users.FirstOrDefaultAsync(id => id.UserName == userName);

            Username = userName;
            
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                ImagenPerfil = usuario.ImagenPerfil
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var usuario = await _context.Users.FirstOrDefaultAsync(id => id.UserName == userName);
           

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if(!String.IsNullOrEmpty(Input.ImagenArchivo?.FileName))
            {
                string wwwRoothRuta = _hostEnvironment.WebRootPath;
                string nombreArchivo = Path.GetFileNameWithoutExtension(Input.ImagenArchivo.FileName);
                string extension = Path.GetExtension(Input.ImagenArchivo.FileName);
                nombreArchivo = nombreArchivo + DateTime.Now.ToString("yymmssfff") + extension;
                Input.ImagenPerfil = nombreArchivo;

                usuario.Nombre = Input.Nombre;
                usuario.Apellido = Input.Apellido;
                usuario.ImagenPerfil = Input.ImagenPerfil;
                await _context.SaveChangesAsync();

                string ruta = Path.Combine(wwwRoothRuta + "\\img", nombreArchivo);
                using (var fileStream = new FileStream(ruta, FileMode.Create))
                {
                    await Input.ImagenArchivo.CopyToAsync(fileStream);
                }
            } 
            else
            {
                usuario.Nombre = Input.Nombre;
                usuario.Apellido = Input.Apellido;
                await _context.SaveChangesAsync();
            }
            
            

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
