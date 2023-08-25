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
    public class CD_ObjetivosNutricional
    {
        public List<ObjetivosNutricional> Listar()
        {
            List<ObjetivosNutricional> lista = new List<ObjetivosNutricional>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT ng.Nutritional_Goal_Id, ng.Name_, ng.Description_");
                    sb.AppendLine("FROM Nutritional_Goals ng");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ObjetivosNutricional()
                            {
                                Nutritional_Goal_Id = Convert.ToInt32(dr["Nutritional_Goal_Id"]),
                                Name_ = dr["Name_"].ToString(),
                                Description_ = dr["Description_"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<ObjetivosNutricional>();
            }
            return lista;
        }
    }
}
