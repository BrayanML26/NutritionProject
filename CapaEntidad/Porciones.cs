using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Porciones
    {
        public int Portion_Id { get; set; }
        public Alimentos oFood_Id { get; set; }
        public string Name_ { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set; }
        
    }
}
