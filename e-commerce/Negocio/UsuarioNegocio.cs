using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        private AccesoDatos datos = new AccesoDatos(); // Atributo compartido

        public void RegistrarUsuario(Usuario usuario)
        {
            try
            {
                datos.setearConsulta("INSERT INTO Usuario (Nombre, Email, Password, Rol, Telefono) VALUES (@Nombre, @Email, @Password, @Rol, @Telefono)");
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Password", usuario.Password);
                datos.setearParametro("@Rol", usuario.Rol);
                datos.setearParametro("@Telefono", usuario.Telefono ?? "");
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExisteEmail(string email)
        {
            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Cantidad FROM Usuario WHERE Email = @Email");
                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = Convert.ToInt32(datos.Lector["Cantidad"]);
                    return count > 0;
                }

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario ValidarLogin(string email, string password)
        {
            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Email, Password, Rol, Telefono, Activo FROM Usuario WHERE Email = @Email AND Password = @Password AND Activo = 1");
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Password", password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Usuario
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

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
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
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                datos.setearConsulta(@"
                    UPDATE Usuario
                    SET Nombre=@Nombre, Email=@Email, Password=@Password, Telefono=@Telefono
                    WHERE IdUsuario=@IdUsuario
                ");
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Password", usuario.Password);
                datos.setearParametro("@Telefono", usuario.Telefono ?? "");
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminarUsuario(int idUsuario)
        {
            try
            {
                datos.setearConsulta("UPDATE Usuario SET Activo = 0 WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Email, Password, Rol, Telefono, Activo FROM Usuario WHERE IdUsuario=@IdUsuario");
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
