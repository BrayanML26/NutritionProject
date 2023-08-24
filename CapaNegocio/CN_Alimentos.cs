using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Alimentos
    {
        private CD_Alimentos objCapaDato = new CD_Alimentos();

        public List<Alimentos> Listar()
        {
            return objCapaDato.Listar();
        }

        public List<Alimentos> ListarAlimentosPorDieta(int idtipodieta)
        {
            return objCapaDato.ListarAlimentosPorDieta(idtipodieta);
        }
    }
}
