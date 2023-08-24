using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Alimentos
    {
        public int Food_Id { get; set; }
        public string Name_ { get; set; }
        public string Description_ { get; set; }
        public CategoriaAlimentos oCategory_Id { get; set; }
        public string Serving_Size { get; set; }
        public string Calories { get; set; }

    }
}
