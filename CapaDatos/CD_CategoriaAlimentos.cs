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
    public class CD_CategoriaAlimentos
    {
        public List<CategoriaAlimentos> Listar()
        {
            List<CategoriaAlimentos> lista = new List<CategoriaAlimentos>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT fc.Category_Id, fc.Name_, fc.Description_");
                    sb.AppendLine("FROM Food_Category fc");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CategoriaAlimentos()
                            {
                                Category_Id = Convert.ToInt32(dr["Category_Id"]),
                                Name_ = dr["Name_"].ToString(),
                                Description_ = dr["Description_"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<CategoriaAlimentos>();
            }
            return lista;
        }
    }
}
