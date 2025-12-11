using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DireccionNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public List<Direccion> Listar()
        {
            List<Direccion> lista = new List<Direccion>();

            try
            {
                // Consulta SOLO de la tabla Direcciones (sin JOIN con Usuarios)
                datos.setearConsulta(@"
                    SELECT 
                        IDDireccion,
                        IDUsuario,
                        Calle,
                        Numero,
                        Localidad,
                        Provincia,
                        CodigoPostal,
                        Observaciones,
                        EsPrincipal,
                        TipoDireccion,
                        FechaRegistro,
                        FechaModificacion,
                        Activo
                    FROM Direcciones 
                    WHERE Activo = 1
                    ORDER BY FechaRegistro DESC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion direccion = new Direccion();

                    direccion.IDDireccion = Convert.ToInt32(datos.Lector["IDDireccion"]);
                    direccion.IDUsuario = Convert.ToInt32(datos.Lector["IDUsuario"]);
                    direccion.Calle = datos.Lector["Calle"]?.ToString() ?? "";
                    direccion.Numero = datos.Lector["Numero"]?.ToString() ?? "";
                    direccion.Localidad = datos.Lector["Localidad"]?.ToString() ?? "";
                    direccion.Provincia = datos.Lector["Provincia"]?.ToString() ?? "";
                    direccion.CodigoPostal = datos.Lector["CodigoPostal"]?.ToString() ?? "";
                    direccion.Observaciones = datos.Lector["Observaciones"]?.ToString() ?? "";
                    direccion.EsPrincipal = Convert.ToBoolean(datos.Lector["EsPrincipal"]);
                    direccion.TipoDireccion = datos.Lector["TipoDireccion"]?.ToString() ?? "";
                    direccion.FechaRegistro = Convert.ToDateTime(datos.Lector["FechaRegistro"]);

                    // FechaModificacion puede ser NULL
                    if (datos.Lector["FechaModificacion"] != DBNull.Value)
                        direccion.FechaModificacion = Convert.ToDateTime(datos.Lector["FechaModificacion"]);

                    direccion.Activo = Convert.ToBoolean(datos.Lector["Activo"]);

                    // Datos del usuario vacíos (ya que no los estamos trayendo)
                    direccion.NombreUsuario = "";  // Vacío
                    direccion.DniUsuario = "";     // Vacío

                    lista.Add(direccion);
                }

                return lista;
            }
            catch (Exception ex)
            {
                // Lanza una excepción más informativa
                throw new Exception("Error al listar direcciones. Detalles: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int idDireccion)
        {
            try
            {
                datos.setearConsulta("UPDATE Direcciones SET Activo = 0 WHERE IDDireccion = @id");
                datos.setearParametro("@id", idDireccion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar dirección: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}