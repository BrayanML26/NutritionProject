using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Preparacion
    {
        public int Preparation_Id { get; set; }
        public Recetas oRecipe_Id { get; set; }
        public string Step_by_Step { get; set; }
    }
}
