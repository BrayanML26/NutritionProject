using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_CategoriaAlimentos
    {
        private CD_CategoriaAlimentos objCapaDato = new CD_CategoriaAlimentos();

        public List<CategoriaAlimentos> Listar()
        {
            return objCapaDato.Listar();
        }
    }
}
