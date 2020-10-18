using Friensify.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Friensify.Models
{
    [DataContract(IsReference = true)]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(255)")]
        public string Contenido { get; set; }

        [DataMember]
        public string URLImagen { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }

        public string TiempoTranscurrido()
        {
            var intervalo = DateTime.Now - Fecha;
            var intervaloSec = intervalo.TotalSeconds;

            if (intervaloSec < 60)
                return $"Hace {Math.Floor(intervaloSec)} segundos";
            if (intervaloSec < 3_600)
                return $"Hace {intervalo.Minutes} minutos";
            if (intervaloSec < 86_400)
                return $"Hace {intervalo.Hours} horas";
            if (intervaloSec < 604_800)
                return $"Hace {intervalo.Days} días";
            else
                return $"Hace {Math.Abs(intervalo.Days / 7) } semanas";
        }
    }
}
