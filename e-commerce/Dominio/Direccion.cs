using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Direccion
    {
        public int IDDireccion { get; set; }
        public int IDUsuario { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; } // Nuevo campo
        public string CodigoPostal { get; set; }
        public string Observaciones { get; set; }
        public bool EsPrincipal { get; set; }
        public string TipoDireccion { get; set; } // Nuevo campo
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; } // Nuevo campo (nullable)
        public bool Activo { get; set; }

        // Propiedades adicionales si necesitas mostrar info del usuario
        public string NombreUsuario { get; set; }
        public string DniUsuario { get; set; }
    }
}
