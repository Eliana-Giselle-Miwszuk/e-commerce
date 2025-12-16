using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;

namespace Negocio
{
    public class DireccionNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        // Listar direcciones de un usuario
        public List<Direccion> ListarDireccionesPorUsuario(int idUsuario)
        {
            List<Direccion> lista = new List<Direccion>();
            try
            {
                datos.setearConsulta("SELECT IdDireccion, IdUsuario, Calle, Numero, Localidad, CodigoPostal, Observaciones FROM Direccion WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion d = new Direccion
                    {
                        IdDireccion = (int)datos.Lector["IdDireccion"],
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Calle = datos.Lector["Calle"].ToString(),
                        Numero = datos.Lector["Numero"].ToString(),
                        Localidad = datos.Lector["Localidad"].ToString(),
                        CodigoPostal = datos.Lector["CodigoPostal"].ToString(),
                        Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? datos.Lector["Observaciones"].ToString() : ""
                    };
                    lista.Add(d);
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

        // Agregar nueva dirección
        public void AgregarDireccion(Direccion direccion)
        {
            try
            {
                datos.setearConsulta("INSERT INTO Direccion (IdUsuario, Calle, Numero, Localidad, CodigoPostal, Observaciones) VALUES (@IdUsuario, @Calle, @Numero, @Localidad, @CodigoPostal, @Observaciones)");
                datos.setearParametro("@IdUsuario", direccion.IdUsuario);
                datos.setearParametro("@Calle", direccion.Calle);
                datos.setearParametro("@Numero", direccion.Numero);
                datos.setearParametro("@Localidad", direccion.Localidad);
                datos.setearParametro("@CodigoPostal", direccion.CodigoPostal);
                datos.setearParametro("@Observaciones", direccion.Observaciones ?? "");
                datos.ejecutarAccion();
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

        // Editar dirección existente
        public void EditarDireccion(Direccion direccion)
        {
            try
            {
                datos.setearConsulta("UPDATE Direccion SET Calle=@Calle, Numero=@Numero, Localidad=@Localidad, CodigoPostal=@CodigoPostal, Observaciones=@Observaciones WHERE IdDireccion=@IdDireccion");
                datos.setearParametro("@Calle", direccion.Calle);
                datos.setearParametro("@Numero", direccion.Numero);
                datos.setearParametro("@Localidad", direccion.Localidad);
                datos.setearParametro("@CodigoPostal", direccion.CodigoPostal);
                datos.setearParametro("@Observaciones", direccion.Observaciones ?? "");
                datos.setearParametro("@IdDireccion", direccion.IdDireccion);
                datos.ejecutarAccion();
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

        // Eliminar dirección
        public void EliminarDireccion(int idDireccion)
        {
            try
            {
                datos.setearConsulta("DELETE FROM Direccion WHERE IdDireccion=@IdDireccion");
                datos.setearParametro("@IdDireccion", idDireccion);
                datos.ejecutarAccion();
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