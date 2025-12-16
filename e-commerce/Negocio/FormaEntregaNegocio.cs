using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;

namespace Negocio
{
    public class FormaEntregaNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public List<FormaEntrega> ListarFormasEntrega()
        {
            List<FormaEntrega> lista = new List<FormaEntrega>();

            try
            {
                datos.setearConsulta("SELECT IdFormaEntrega, Nombre FROM FormaEntrega");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new FormaEntrega
                    {
                        IdFormaEntrega = (int)datos.Lector["IdFormaEntrega"],
                        Nombre = datos.Lector["Nombre"].ToString()
                    });
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