using AgendaOnline.Models;
using System.Data.SqlClient;
using System.Data;

namespace AgendaOnline.Datos
{
    public class LoginDatos
    {
        public static List<UsuairosModel> GetList()
        {
            List<UsuairosModel> lista = new List<UsuairosModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.PasameLaCadena()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new UsuairosModel
                        {
                            IdUsuario = reader.GetInt32("IdUsuario"),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Pass = reader["Pass"].ToString()

                        });
                    }
                }
                return lista;
            }
        }
        public static bool CrearUsuario(UsuairosModel model)
        {
            bool resp;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.PasameLaCadena()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CreacionUsuario",conexion);
                    cmd.Parameters.AddWithValue("@nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", model.Apellido);
                    cmd.Parameters.AddWithValue("@userName", model.UserName);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@pass", model.Pass);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    resp = true;
                }
            }
            catch 
            { 
                resp = false; 
            }
            return resp;
        }
        public static bool EliminarUsuario(int idUsuario)
        {
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.PasameLaCadena()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", conexion);
                cmd.Parameters.AddWithValue("id",idUsuario);
                cmd.CommandType = CommandType.StoredProcedure;
                int filaAfectada = cmd.ExecuteNonQuery();
                if (filaAfectada > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool InicioDeSesion(UsuairosModel model)
        {
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.PasameLaCadena()))
            {
                conexion.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_IniciarSesion",conexion);
                    cmd.Parameters.AddWithValue("nombreUsuario", model.UserName);
                    cmd.Parameters.AddWithValue("pass", model.Pass);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }   
}
