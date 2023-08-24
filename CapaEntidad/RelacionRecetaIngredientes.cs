using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class RelacionRecetaIngredientes
    {
        public Recetas oRecipe_Id { get; set; }
        public Ingredientes oIngredient_Id { get; set; }
        public int Ingredient_Quantity { get; set; }
    }
}
