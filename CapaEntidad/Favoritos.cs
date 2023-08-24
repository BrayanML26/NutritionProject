using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Favoritos
    {
        public int Favorite_Id { get; set; }
        public Recetas oRecipe_Id { get; set; }
        public Usuarios oUserId { get; set; }
    }
}
