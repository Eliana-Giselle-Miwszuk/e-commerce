using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } // Pendiente, Pagado, Enviado

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public int? IdFormaPago { get; set; }
        public FormaPago FormaPago { get; set; }

        public int? IdFormaEntrega { get; set; }
        public FormaEntrega FormaEntrega { get; set; }

        public List<DetallePedido> Detalles { get; set; }

            

    }
}
