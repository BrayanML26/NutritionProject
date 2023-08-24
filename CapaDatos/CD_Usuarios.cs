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
    public class CD_Usuarios
    {
        public List<Usuarios> Listar()
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT u.UserId, u.Name_, u.Email, u.CellPhone, u.Age, u.Gender,");
                    sb.AppendLine("u.Weight_, u.Height, u.Username, u.Password_, u.Date_");
                    sb.AppendLine("FROM Users u");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                Name_ = dr["Name_"].ToString(),
                                Email = dr["Email"].ToString(),
                                CellPhone = dr["CellPhone"].ToString(),
                                Age = Convert.ToInt32(dr["Age"]),
                                Gender = dr["Gender"].ToString(),
                                Weight_ = Convert.ToInt32(dr["Weight_"]),
                                Height = Convert.ToInt32(dr["Height"]),
                                Username = dr["Username"].ToString(),
                                Password_ = dr["Password_"].ToString(),
                                Date_ = Convert.ToDateTime(dr["Date_"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuarios>();
            }
            return lista;
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    obj.Date_ = DateTime.Now;

                    SqlCommand cmd = new SqlCommand("Sp_InsertUser", oconexion);
                    cmd.Parameters.AddWithValue("Name_", obj.Name_);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("CellPhone", obj.CellPhone);
                    cmd.Parameters.AddWithValue("Age", obj.Age);
                    cmd.Parameters.AddWithValue("Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("Weight_", obj.Weight_);
                    cmd.Parameters.AddWithValue("Height", obj.Height);
                    cmd.Parameters.AddWithValue("Username", obj.Username);
                    cmd.Parameters.AddWithValue("Password_", obj.Password_);
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
