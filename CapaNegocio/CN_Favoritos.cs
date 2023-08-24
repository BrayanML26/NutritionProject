using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Favoritos
    {
        private CD_Favoritos objCapaDato = new CD_Favoritos();

        public bool ExisteFavorito(int idreceta, int idusuario)
        {
            return objCapaDato.ExisteFavorito(idreceta, idusuario);
        }

        public int CantidadFavorito(int idusuario)
        {
            return objCapaDato.CantidadFavorito(idusuario);
        }
    }
}
