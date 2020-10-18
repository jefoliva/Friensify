using Friensify.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.ViewModels
{
    [JsonObject]
    public class PerfilViewModel 
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Biografia { get; set; }

        public string ImagenPerfil { get; set; }

        [JsonIgnore]
        public PostInput Post { get; set; }

        public ICollection<Post> Posts { get; set; }

        public string NombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
