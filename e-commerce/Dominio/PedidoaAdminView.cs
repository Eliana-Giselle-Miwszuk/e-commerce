using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PedidoAdminView
    {
        public int IdPedido { get; set; }
        public string NombreUsuario { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
