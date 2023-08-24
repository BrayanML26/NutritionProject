using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Favoritos
    {
        public bool ExisteFavorito(int idreceta, int idusuario)
        {
            bool resultado = true;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ExisteFavorito", oconexion);
                    cmd.Parameters.AddWithValue("Recipe_Id", idreceta);
                    cmd.Parameters.AddWithValue("UserId", idusuario);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }

        public int CantidadFavorito(int idusuario)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Favorites WHERE UserId = @UserId", oconexion);
                    cmd.Parameters.AddWithValue("@UserId", idusuario);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                resultado = 0;
            }
            return resultado;
        }

    }
}
