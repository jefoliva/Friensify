using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.Models
{
    public class Publicaciones
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        public DateTime Fecha { get; set; }

        public string Texto { get; set; }
    }
}
