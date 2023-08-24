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
    public class CD_TipoDieta
    {
        public List<TipoDieta> Listar()
        {
            List<TipoDieta> lista = new List<TipoDieta>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT dt.Diet_Type_Id, dt.Name_, dt.Description_");
                    sb.AppendLine("FROM Diet_Type dt");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TipoDieta()
                            {
                                Diet_Type_Id = Convert.ToInt32(dr["Diet_Type_Id"]),
                                Name_ = dr["Name_"].ToString(),
                                Description_ = dr["Description_"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<TipoDieta>();
            }
            return lista;
        }
    }
}
