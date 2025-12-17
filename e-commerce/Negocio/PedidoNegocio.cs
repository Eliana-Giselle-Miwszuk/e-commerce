using System;
using System.Collections.Generic;

using Dominio;

namespace Negocio
{
    public class PedidoNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public void CrearPedido(Usuario usuario, Carrito carrito, int idDireccionEntrega, int idFormaEntrega)
        {
            if (usuario == null)
                throw new Exception("Debe iniciar sesión para realizar la compra.");

            if (carrito == null || carrito.Items.Count == 0)
                throw new Exception("El carrito está vacío.");

            ProductoNegocio productoNegocio = new ProductoNegocio();

            try
            {
            
                datos.setearConsulta(@"
                    INSERT INTO Pedido (Total, Estado, IdUsuario, IdDireccionEntrega, IdFormaEntrega)
                    VALUES (@Total, 'Pendiente', @IdUsuario, @IdDireccionEntrega, @IdFormaEntrega);
                    SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@Total", carrito.Total());
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);
                datos.setearParametro("@IdDireccionEntrega", idDireccionEntrega);
                datos.setearParametro("@IdFormaEntrega", idFormaEntrega);

                int idPedido = Convert.ToInt32(datos.ObtenerIdGenerado());

                foreach (var item in carrito.Items)
                {
                    Producto producto = productoNegocio.ObtenerProductoPorId(item.ProductoId);

                    if (producto == null)
                        throw new Exception("Producto no encontrado.");

                   
                    if (producto.Stock < item.Cantidad)
                        throw new Exception($"Stock insuficiente para {producto.Nombre}");

                
                    producto.Stock -= item.Cantidad;
                    productoNegocio.ActualizarProducto(producto);

                    datos.setearConsulta(@"
                        INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario)
                        VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario)");

                    datos.setearParametro("@IdPedido", idPedido);
                    datos.setearParametro("@IdProducto", item.ProductoId);
                    datos.setearParametro("@Cantidad", item.Cantidad);
                    datos.setearParametro("@PrecioUnitario", item.Precio);

                    datos.ejecutarAccion();
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<PedidoAdminView> ListarPedidosConUsuario()
        {
            List<PedidoAdminView> lista = new List<PedidoAdminView>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT 
                        P.IdPedido,
                        U.Nombre AS NombreUsuario,
                        P.Total,
                        P.Estado,
                        P.Fecha
                    FROM Pedido P
                    INNER JOIN Usuario U ON U.IdUsuario = P.IdUsuario
                    ORDER BY P.Fecha DESC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new PedidoAdminView
                    {
                        IdPedido = (int)datos.Lector["IdPedido"],
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString(),
                        Total = (decimal)datos.Lector["Total"],
                        Estado = datos.Lector["Estado"].ToString(),
                        Fecha = (DateTime)datos.Lector["Fecha"]
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ActualizarEstadoPedido(int idPedido, string estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Pedido SET Estado = @estado WHERE IdPedido = @id");
                datos.setearParametro("@estado", estado);
                datos.setearParametro("@id", idPedido);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<PedidoDetalleView> ListarDetallePedido(int idPedido)
        {
            List<PedidoDetalleView> lista = new List<PedidoDetalleView>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT 
                        P.Nombre AS NombreProducto,
                        DP.Cantidad,
                        DP.PrecioUnitario
                    FROM DetallePedido DP
                    INNER JOIN Producto P ON P.IdProducto = DP.IdProducto
                    WHERE DP.IdPedido = @id");

                datos.setearParametro("@id", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new PedidoDetalleView
                    {
                        NombreProducto = datos.Lector["NombreProducto"].ToString(),
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public PedidoDetalleCompletoView ObtenerInfoPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                P.IdPedido,
                P.Estado,
                P.Fecha,
                P.Total,
                U.Nombre AS NombreUsuario,
                U.Email,
                U.Telefono,
                D.Calle,
                D.Numero,
                D.Localidad,
                D.CodigoPostal
            FROM Pedido P
                INNER JOIN Usuario U ON U.IdUsuario = P.IdUsuario
                LEFT JOIN Direccion D ON D.IdDireccion = P.IdDireccionEntrega
                WHERE P.IdPedido = @id");

                datos.setearParametro("@id", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new PedidoDetalleCompletoView
                    {
                        IdPedido = (int)datos.Lector["IdPedido"],
                        Estado = datos.Lector["Estado"].ToString(),
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Total = (decimal)datos.Lector["Total"],
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),

                        Calle = datos.Lector["Calle"] == DBNull.Value ? "" : datos.Lector["Calle"].ToString(),
                        Numero = datos.Lector["Numero"] == DBNull.Value ? "" : datos.Lector["Numero"].ToString(),
                        Localidad = datos.Lector["Localidad"] == DBNull.Value ? "" : datos.Lector["Localidad"].ToString(),
                        CodigoPostal = datos.Lector["CodigoPostal"] == DBNull.Value ? "" : datos.Lector["CodigoPostal"].ToString()
                    };

                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



    }
}
