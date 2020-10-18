using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Friensify.Models
{
    public class VisitasPerfil
    {
        public string IdUsuario { get; set; }

        public DateTime Fecha { get; set; }

        public string Nombre { get; set; }

        public int Visitas { get; set; }

    }
}
