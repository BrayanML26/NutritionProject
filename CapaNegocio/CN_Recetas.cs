using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Recetas
    {
        private CD_Recetas objCapaDato = new CD_Recetas();

        public List<Recetas> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Recetas obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Name_) || string.IsNullOrWhiteSpace(obj.Name_))
            {
                Mensaje = "El nombre no puede ser vacio";
            }
            if (string.IsNullOrEmpty(obj.Description_) || string.IsNullOrWhiteSpace(obj.Description_))
            {
                Mensaje = "La descripcion no puede ser vacio";
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
