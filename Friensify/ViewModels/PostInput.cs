using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.ViewModels
{
    public class PostInput
    {
        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.MultilineText)]
        public string Contenido { get; set; }

        public string URLImagen { get; set; }

        [DisplayName("Subir Archivo")]
        public IFormFile ImagenPost { get; set; }
    }
}
