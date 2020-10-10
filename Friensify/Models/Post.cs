using Friensify.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Fecha { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Contenido { get; set; }

        public string URLImagen { get; set; }
    }
}
