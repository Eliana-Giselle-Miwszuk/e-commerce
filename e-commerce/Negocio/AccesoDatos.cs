using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS;database=EcommerceDB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ejecutarAccion(bool retornarFilasAfectadas)
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
        }

        public List<T> ObtenerLista<T>(Func<SqlDataReader, T> mapear)
        {
            var lista = new List<T>();
            try
            {
                ejecutarLectura();
                while (lector.Read())
                {
                    lista.Add(mapear(lector));
                }
                return lista;
            }
            finally
            {
                cerrarConexion();
            }
        }

        public int Insertar(string tabla, Dictionary<string, object> parametros)
        {
            var campos = string.Join(", ", parametros.Keys);
            var valores = string.Join(", ", parametros.Keys.Select(k => "@" + k));
            comando.CommandText = $"INSERT INTO {tabla} ({campos}) VALUES ({valores}); SELECT SCOPE_IDENTITY();";
            comando.CommandType = CommandType.Text;
            comando.Parameters.Clear();

            Debug.WriteLine($"Consulta SQL: {comando.CommandText}");

            foreach (var param in parametros)
            {
                comando.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
            }

            try
            {
                comando.Connection = conexion;
                conexion.Open();
                var resultado = comando.ExecuteScalar();
                return resultado != null ? Convert.ToInt32(resultado) : 0;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Error SQL al insertar en {tabla}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar en {tabla}: {ex.Message}", ex);
            }
            finally
            {
                cerrarConexion();
            }
        }

        public int Actualizar(string tabla, Dictionary<string, object> parametros, string condicion)
        {
            var sets = string.Join(", ", parametros.Keys.Select(k => $"{k} = @{k}"));
            comando.CommandText = $"UPDATE {tabla} SET {sets} WHERE {condicion}";
            comando.CommandType = CommandType.Text;
            comando.Parameters.Clear();

            foreach (var param in parametros)
            {
                comando.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
            }

            try
            {
                comando.Connection = conexion;
                conexion.Open();
                return comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar {tabla}: {ex.Message}", ex);
            }
            finally
            {
                cerrarConexion();
            }
        }

        public int Eliminar(string tabla, string condicion, Dictionary<string, object> parametros = null)
        {
            comando.CommandText = $"DELETE FROM {tabla} WHERE {condicion}";
            comando.CommandType = CommandType.Text;
            comando.Parameters.Clear();

            if (parametros != null)
            {
                foreach (var param in parametros)
                {
                    comando.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                }
            }

            try
            {
                comando.Connection = conexion;
                conexion.Open();
                return comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar de {tabla}", ex);
            }
            finally
            {
                cerrarConexion();
            }
        }

        public List<T> Listar<T>(string tabla, string condiciones = null, Dictionary<string, object> parametros = null, Func<SqlDataReader, T> mapear = null)
        {
            var lista = new List<T>();

            // Construir la consulta SQL
            string consulta = $"SELECT * FROM {tabla}";
            if (!string.IsNullOrEmpty(condiciones))
                consulta += $" WHERE {condiciones}";

            comando.CommandText = consulta;
            comando.CommandType = CommandType.Text;
            comando.Parameters.Clear();

            Debug.WriteLine($"Consulta Listar: {consulta}");

            if (parametros != null)
            {
                foreach (var param in parametros)
                {
                    comando.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                }
            }

            try
            {
                comando.Connection = conexion;
                conexion.Open();

                using (var reader = comando.ExecuteReader())
                {
                    if (mapear != null)
                    {
                        while (reader.Read())
                        {
                            var item = mapear(reader);
                            if (item != null)
                                lista.Add(item);
                        }
                    }
                }
                Debug.WriteLine($"Registros encontrados: {lista.Count}");
                return lista;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en Listar: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw new Exception($"Error al listar {tabla}: {ex.Message}", ex);
            }
            finally
            {
                cerrarConexion();
            }
        }

        public List<KeyValuePair<int, string>> CargarDesplegable(string tabla, string idColumna, string textoColumna, string condiciones = null, Dictionary<string, object> parametros = null)
        {
            var lista = new List<KeyValuePair<int, string>>();
            string query = $"SELECT {idColumna}, {textoColumna} FROM {tabla}";

            if (!string.IsNullOrEmpty(condiciones))
                query += $" WHERE {condiciones}";

            comando.CommandText = query;
            comando.CommandType = CommandType.Text;
            comando.Parameters.Clear();

            if (parametros != null)
            {
                foreach (var param in parametros)
                {
                    comando.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                }
            }

            try
            {
                comando.Connection = conexion;
                conexion.Open();

                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int clave = reader.GetInt32(0);
                        string texto = reader.GetString(1);
                        lista.Add(new KeyValuePair<int, string>(clave, texto));
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar desplegable: {ex.Message}");
                throw;
            }
            finally
            {
                cerrarConexion();
            }
        }

        public void limpiarParametros()
        {
            if (this.comando != null)
            {
                this.comando.Parameters.Clear();
            }
        }

        public bool ProbarConexion()
        {
            try
            {
                conexion.Open();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error de conexión: " + ex.Message);
                return false;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
          
        }





    }


}