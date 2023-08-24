using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Recetas
    {
        public List<Recetas> Listar()
        {
            List<Recetas> lista = new List<Recetas>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT r.Recipe_Id, u.UserId, r.Name_, r.Description_, dt.Diet_Type_Id, r.Time_Preparation, r.Servings,");
                    sb.AppendLine("    r.Image_, f.Food_Id, r.Date_,");
                    sb.AppendLine("    (SELECT STRING_AGG(p.Step_by_Step, ' ') FROM Preparation p WHERE p.Recipe_Id = r.Recipe_Id) AS Steps,");
                    sb.AppendLine("    (SELECT STRING_AGG(i.Name_, ', ')");
                    sb.AppendLine("     FROM Relationship_Recipes_Ingredients rri");
                    sb.AppendLine("     INNER JOIN Ingredients i ON rri.Ingredient_Id = i.Ingredient_Id");
                    sb.AppendLine("     WHERE rri.Recipe_Id = r.Recipe_Id) AS Ingredients");
                    sb.AppendLine("FROM Recipes r");
                    sb.AppendLine("INNER JOIN Users u ON u.UserId = r.UserId");
                    sb.AppendLine("INNER JOIN Diet_Type dt ON dt.Diet_Type_Id = r.Diet_Type_Id");
                    sb.AppendLine("INNER JOIN Food f ON f.Food_Id = r.Food_Id;");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Recetas()
                            {
                                Recipe_Id = Convert.ToInt32(dr["Recipe_Id"]),
                                oUserId = new Usuarios() { UserId = Convert.ToInt32(dr["UserId"]) },
                                Name_ = dr["Name_"].ToString(),
                                Description_ = dr["Description_"].ToString(),
                                oDiet_Type_Id = new TipoDieta() { Diet_Type_Id = Convert.ToInt32(dr["Diet_Type_Id"]) },
                                Time_Preparation = Convert.ToInt32(dr["Time_Preparation"]),
                                Servings = Convert.ToInt32(dr["Servings"]),
                                Image_ = Convert.ToBase64String((byte[])dr["Image_"]),
                                oFood_Id = new Alimentos() { Food_Id = Convert.ToInt32(dr["Food_Id"]) },
                                Date_ = Convert.ToDateTime(dr["Date_"]),
                                Steps = dr["Steps"].ToString(),
                                Ingredients = dr["Ingredients"].ToString()
                            });
                            //DatoNutricional(lista.Last(), oconexion);
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Recetas>();
            }
            return lista;
        }

        //public void DatoNutricional(Recetas receta, SqlConnection oconexion)
        //{
        //    List<DatosNutricionales> datosNutricionales = new List<DatosNutricionales>();

        //    string consultaNutricional = @"
        //        SELECT nf.Nutrient, SUM(nf.Value_) AS TotalValue, nf.Unit
        //        FROM Nutrition_Facts nf
        //        INNER JOIN Food f ON nf.Food_Id = f.Food_Id
        //        INNER JOIN Relationship_Recipes_Ingredients rri ON rri.Ingredient_Id = f.Food_Id 
        //        WHERE rri.Recipe_Id = @RecipeId
        //        GROUP BY nf.Nutrient, nf.Unit";

        //    using (SqlCommand cmd = new SqlCommand(consultaNutricional, oconexion))
        //    {
        //        cmd.Parameters.AddWithValue("@RecipeId", receta.Recipe_Id);

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                receta.Datos.Add(new DatosNutricionales()
        //                {
        //                    Nutrient = dr["Nutrient"].ToString(),
        //                    Value_ = Convert.ToInt32(dr["TotalValue"]),
        //                    Unit = dr["Unit"].ToString()
        //                });
        //            }
        //        }
        //    }
        //}



        public int Registrar(Recetas obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_InsertRecipe", oconexion);
                    cmd.Parameters.AddWithValue("UserId", obj.oUserId.UserId);
                    cmd.Parameters.AddWithValue("Name_", obj.Name_);
                    cmd.Parameters.AddWithValue("Description_", obj.Description_);
                    cmd.Parameters.AddWithValue("Diet_Type_Id", obj.oDiet_Type_Id.Diet_Type_Id);
                    cmd.Parameters.AddWithValue("Time_Preparation", obj.Time_Preparation);
                    cmd.Parameters.AddWithValue("Servings", obj.Servings);
                    cmd.Parameters.AddWithValue("Image_", obj.Image_);
                    cmd.Parameters.AddWithValue("Food_Id", obj.oFood_Id.Food_Id);
                    cmd.Parameters.AddWithValue("Date_", obj.Date_);



                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

    }
}
