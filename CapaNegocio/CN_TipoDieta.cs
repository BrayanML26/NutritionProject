using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_TipoDieta
    {
        private CD_TipoDieta objCapaDato = new CD_TipoDieta();

        public List<TipoDieta> Listar()
        {
            return objCapaDato.Listar();
        }
    }
}
