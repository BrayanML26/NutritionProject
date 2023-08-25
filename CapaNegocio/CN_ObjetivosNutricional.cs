using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ObjetivosNutricional
    {
        private CD_ObjetivosNutricional objCapaDato = new CD_ObjetivosNutricional();

        public List<ObjetivosNutricional> Listar()
        {
            return objCapaDato.Listar();
        }
    }
}
