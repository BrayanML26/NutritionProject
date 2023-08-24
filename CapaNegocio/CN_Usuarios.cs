using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuarios> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Name_) || string.IsNullOrWhiteSpace(obj.Name_))
            {
                Mensaje = "El nombre no puede ser vacio";
            }
            if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El correo no puede ser vacio";
            }
            if (string.IsNullOrEmpty(obj.Username) || string.IsNullOrWhiteSpace(obj.Username))
            {
                Mensaje = "El usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(obj.Password_) || string.IsNullOrWhiteSpace(obj.Password_))
            {
                Mensaje = "La contraseña no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
    }
}
