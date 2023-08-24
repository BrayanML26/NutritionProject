using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DatosNutricionales
    {
        public int Id_Nutrition_Fact { get; set; }
        public string Nutrient { get; set; }
        public string Unit { get; set; }
        public int Value_ { get; set; }
        public Alimentos oFood_Id { get; set; }
    }
}
