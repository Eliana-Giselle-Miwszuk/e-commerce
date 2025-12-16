using System;
using System.Collections.Generic;
using Dominio;


namespace Negocio
{
    public class UsuarioNegocio
    {
        public void RegistrarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("INSERT INTO Usuario (Nombre, Email, Password, Rol, Telefono) VALUES (@Nombre, @Email, @Password, @Rol, @Telefono)");
            datos.setearParametro("@Nombre", usuario.Nombre);
            datos.setearParametro("@Email", usuario.Email);
            datos.setearParametro("@Password", usuario.Password);
            datos.setearParametro("@Rol", usuario.Rol);
            datos.setearParametro("@Telefono", usuario.Telefono ?? "");
            datos.ejecutarAccion();
            datos.cerrarConexion();
        }

        public Usuario ValidarLogin(string email, string password)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("SELECT IdUsuario, Nombre, Email, Password, Rol, Telefono, Activo FROM Usuario WHERE Email = @Email AND Password = @Password AND Activo = 1");
            datos.setearParametro("@Email", email);
            datos.setearParametro("@Password", password);
            datos.ejecutarLectura();

            Usuario usuario = null;
            if (datos.Lector.Read())
            {
                usuario = new Usuario
                {
                    IdUsuario = (int)datos.Lector["IdUsuario"],
                    Nombre = (string)datos.Lector["Nombre"],
                    Email = (string)datos.Lector["Email"],
                    Password = (string)datos.Lector["Password"],
                    Rol = (string)datos.Lector["Rol"],
                    Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "",
                    Activo = (bool)datos.Lector["Activo"]
                };
            }

            datos.cerrarConexion();
            return usuario;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Usuario
                    SET Nombre = @Nombre,
                        Email = @Email,
                        Password = @Password,
                        Telefono = @Telefono
                    WHERE IdUsuario = @IdUsuario
                ");

                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Password", usuario.Password);
                datos.setearParametro("@Telefono", usuario.Telefono ?? "");
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void EliminarUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuario SET Activo = 0 WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Email, Rol, Telefono, Activo FROM Usuario");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Rol = datos.Lector["Rol"].ToString(),
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "",
                        Activo = (bool)datos.Lector["Activo"]
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ActualizarUsuarioCompleto(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            UPDATE Usuario
            SET Nombre=@Nombre,
                Email=@Email,
                Password=@Password,
                Telefono=@Telefono,
                Rol=@Rol,
                Activo=@Activo
            WHERE IdUsuario=@IdUsuario
        ");
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Password", usuario.Password);
                datos.setearParametro("@Telefono", usuario.Telefono ?? "");
                datos.setearParametro("@Rol", usuario.Rol);
                datos.setearParametro("@Activo", usuario.Activo);
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Usuario ObtenerUsuarioPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            SELECT IdUsuario, Nombre, Email, Password, Rol, Telefono, Activo
            FROM Usuario
            WHERE IdUsuario = @IdUsuario
        ");
                datos.setearParametro("@IdUsuario", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Password = datos.Lector["Password"].ToString(),
                        Rol = datos.Lector["Rol"].ToString(),
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "",
                        Activo = (bool)datos.Lector["Activo"]
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
