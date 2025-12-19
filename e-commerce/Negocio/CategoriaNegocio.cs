using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public List<Categoria> ListarCategoriasActivas()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre FROM Categoria WHERE Activa = 1 AND IdCategoria <> 7");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria c = new Categoria
                    {
                        IdCategoria = (int)datos.Lector["IdCategoria"],
                        Nombre = datos.Lector["Nombre"].ToString()
                    };
                    lista.Add(c);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}