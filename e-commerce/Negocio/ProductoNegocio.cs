using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public List<Producto> ListarProductosActivos()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                datos.setearConsulta("SELECT IdProducto, Nombre, Descripcion, Precio, Stock, Activo, IdCategoria, ImagenUrl FROM Producto WHERE Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Producto
                    {
                        IdProducto = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (decimal)datos.Lector["Precio"],
                        Stock = (int)datos.Lector["Stock"],
                        Activo = (bool)datos.Lector["Activo"],
                        IdCategoria = (int)datos.Lector["IdCategoria"],
                        ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? datos.Lector["ImagenUrl"].ToString() : ""
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarProducto(Producto p)
        {
            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Producto (Nombre, Descripcion, Precio, Stock, IdCategoria, Activo, ImagenUrl)
                    VALUES (@Nombre, @Descripcion, @Precio, @Stock, @IdCategoria, @Activo, @ImagenUrl)");
                datos.setearParametro("@Nombre", p.Nombre);
                datos.setearParametro("@Descripcion", p.Descripcion);
                datos.setearParametro("@Precio", p.Precio);
                datos.setearParametro("@Stock", p.Stock);
                datos.setearParametro("@IdCategoria", p.IdCategoria);
                datos.setearParametro("@Activo", p.Activo);
                datos.setearParametro("@ImagenUrl", p.ImagenUrl ?? "");
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ActualizarProducto(Producto p)
        {
            try
            {
                datos.setearConsulta(@"
                    UPDATE Producto
                    SET Nombre=@Nombre,
                        Descripcion=@Descripcion,
                        Precio=@Precio,
                        Stock=@Stock,
                        IdCategoria=@IdCategoria,
                        Activo=@Activo,
                        ImagenUrl=@ImagenUrl
                    WHERE IdProducto=@IdProducto");
                datos.setearParametro("@Nombre", p.Nombre);
                datos.setearParametro("@Descripcion", p.Descripcion);
                datos.setearParametro("@Precio", p.Precio);
                datos.setearParametro("@Stock", p.Stock);
                datos.setearParametro("@IdCategoria", p.IdCategoria);
                datos.setearParametro("@Activo", p.Activo);
                datos.setearParametro("@ImagenUrl", p.ImagenUrl ?? "");
                datos.setearParametro("@IdProducto", p.IdProducto);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminarProducto(int idProducto)
        {
            try
            {
                datos.setearConsulta("UPDATE Producto SET Activo = 0 WHERE IdProducto=@IdProducto");
                datos.setearParametro("@IdProducto", idProducto);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre FROM Categoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Categoria
                    {
                        IdCategoria = (int)datos.Lector["IdCategoria"],
                        Nombre = datos.Lector["Nombre"].ToString()
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Producto ObtenerProductoPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            SELECT IdProducto, Nombre, Descripcion, Precio, Stock, Activo, IdCategoria, ImagenUrl
            FROM Producto
            WHERE IdProducto = @IdProducto
        ");
                datos.setearParametro("@IdProducto", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Producto
                    {
                        IdProducto = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (decimal)datos.Lector["Precio"],
                        Stock = (int)datos.Lector["Stock"],
                        Activo = (bool)datos.Lector["Activo"],
                        IdCategoria = (int)datos.Lector["IdCategoria"],
                        ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? datos.Lector["ImagenUrl"].ToString() : ""
                    };
                }
                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Producto> Top3MasVendidos()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT TOP 3 
                p.IdProducto,
                p.Nombre,
                p.Descripcion,
                p.Precio,
                p.Stock,
                p.Activo,
                p.IdCategoria,
                p.ImagenUrl,
                SUM(dp.Cantidad) AS TotalVendido
            FROM DetallePedido dp
            INNER JOIN Producto p ON p.IdProducto = dp.IdProducto
            GROUP BY 
                p.IdProducto, p.Nombre, p.Descripcion, p.Precio,
                p.Stock, p.Activo, p.IdCategoria, p.ImagenUrl
            ORDER BY TotalVendido DESC
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Producto
                    {
                        IdProducto = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (decimal)datos.Lector["Precio"],
                        Stock = (int)datos.Lector["Stock"],
                        Activo = (bool)datos.Lector["Activo"],
                        IdCategoria = (int)datos.Lector["IdCategoria"],
                        ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value
                                     ? datos.Lector["ImagenUrl"].ToString()
                                     : ""
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                datos.setearConsulta("SELECT IdProducto, Nombre, Descripcion, Precio, Stock, Activo, IdCategoria, ImagenUrl FROM Producto");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Producto
                    {
                        IdProducto = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (decimal)datos.Lector["Precio"],
                        Stock = (int)datos.Lector["Stock"],
                        Activo = (bool)datos.Lector["Activo"],
                        IdCategoria = (int)datos.Lector["IdCategoria"],
                        ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? datos.Lector["ImagenUrl"].ToString() : ""
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }




    }
}
