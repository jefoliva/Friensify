using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.Models
{
    public class Comentarios
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public DateTime Fecha { get; set; }
        public int Id_publicacion { get; set; }
    }
}
