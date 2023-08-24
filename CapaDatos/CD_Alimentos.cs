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
    public class CD_Alimentos
    {
        public List<Alimentos> Listar()
        {
            List<Alimentos> lista = new List<Alimentos>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT f.Food_Id, f.Name_, f.Description_, fc.Category_Id,");
                    sb.AppendLine("f.Serving_Size, f.Calories");
                    sb.AppendLine("FROM Food f");
                    sb.AppendLine("INNER JOIN Food_Category fc ON fc.Category_Id = f.Category_Id");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Alimentos()
                            {
                                Food_Id = Convert.ToInt32(dr["Food_Id"]),
                                Name_ = dr["Name_"].ToString(),
                                Description_ = dr["Description_"].ToString(),
                                oCategory_Id = new CategoriaAlimentos() { Category_Id = Convert.ToInt32(dr["Category_Id"]) },
                                Serving_Size = dr["Serving_Size"].ToString(),
                                Calories = dr["Calories"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Alimentos>();
            }
            return lista;
        }


        public List<Alimentos> ListarAlimentosPorDieta(int idtipodieta)
        {
            List<Alimentos> lista = new List<Alimentos>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT DISTINCT f.Food_Id, f.Name_ FROM Recipes r");
                    sb.AppendLine("INNER JOIN Diet_Type dt ON dt.Diet_Type_Id = r.Diet_Type_Id");
                    sb.AppendLine("INNER JOIN Food f ON f.Food_Id = r.Food_Id");
                    sb.AppendLine("WHERE dt.Diet_Type_Id = iif(@idtipodieta = 0, dt.Diet_Type_Id, @idtipodieta)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idtipodieta", idtipodieta);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Alimentos()
                            {
                                Food_Id = Convert.ToInt32(dr["Food_Id"]),
                                Name_ = dr["Name_"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Alimentos>();
            }
            return lista;
        }
    }
}
