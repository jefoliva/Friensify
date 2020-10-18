using Friensify.Models;
using Friensify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.Controllers
{
    public static class Helper
    {
        public static async Task ActualizaVisitas(FriensifyContext _context, PerfilViewModel usuario)
        {
            var resultado = _context.VisitasPerfil
                .SingleOrDefault(u => u.IdUsuario == usuario.UserId 
                                && u.Fecha.Date == DateTime.Now.Date);

            if (resultado == null)
            {
                var visitasPerfil = new VisitasPerfil
                {
                    IdUsuario = usuario.UserId,
                    Nombre = usuario.NombreCompleto(),
                    Fecha = DateTime.Now.Date,
                    Visitas = 1
                };

                await _context.VisitasPerfil.AddAsync(visitasPerfil);
                await _context.SaveChangesAsync();
            }
            else {
                resultado.Visitas = resultado.Visitas + 1;
                await _context.SaveChangesAsync();
            }
        }
    }
}
