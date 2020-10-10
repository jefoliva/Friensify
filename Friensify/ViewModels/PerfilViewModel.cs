using Friensify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.ViewModels
{
    public class PerfilViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Biografia { get; set; }

        public string ImagenPerfil { get; set; }

        public Post Post { get; set; }

        public string NombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
