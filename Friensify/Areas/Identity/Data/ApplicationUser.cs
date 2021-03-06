﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Friensify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Friensify.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Nombre { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Apellido { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string Biografia { get; set; }

        [PersonalData]
        [DisplayName("Imagen Nombre")]
        public string ImagenPerfil { get; set; }

        [NotMapped]
        [DisplayName("Subir Archivo")]
        public IFormFile ImagenArchivo { get; set; }

        public ICollection<Post> Posts { get; set; }


        public string NombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
