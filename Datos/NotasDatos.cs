using AgendaOnline.Models;
using System.Data.SqlClient;
using System.Data;

namespace AgendaOnline.Datos
{
    public class NotasDatos
    {
        public static List<ListaNotasModel> GetList(int idUsuario)
        {
            List<ListaNotasModel> lista = new List<ListaNotasModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.PasameLaCadena()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarTusNotas",conexion);
                cmd.Parameters.AddWithValue("@idUsuario",idUsuario);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ListaNotasModel
                        {
                            IdNotes = Convert.ToInt32(reader["IdNotes"]),
                            Indice = reader["Indice"].ToString(),
                            Nota = reader["Nota"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
        public static bool EliminarNota(int idNota)
        {
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.PasameLaCadena()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarNota", conexion);
                cmd.Parameters.AddWithValue("@id", idNota);
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
        public static bool CrearNotas(string notaNueva, string indiceNota, int idUsuario)
        {
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.PasameLaCadena()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_CrearNota", conexion);
                cmd.Parameters.AddWithValue("@nota", notaNueva);
                cmd.Parameters.AddWithValue("@indice ",indiceNota);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
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
    }
}
