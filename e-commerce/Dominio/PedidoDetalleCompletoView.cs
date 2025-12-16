using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PedidoDetalleCompletoView
    {
        public int IdPedido { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        // Usuario
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        // Dirección
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
    }
}
