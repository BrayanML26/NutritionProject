using System;
using System.Collections.Generic;

namespace CapaEntidad
{
    public class Recetas
    {
        public int Recipe_Id { get; set; }
        public Usuarios oUserId { get; set; }
        public string Name_ { get; set; }
        public string Description_ { get; set; }
        public TipoDieta oDiet_Type_Id { get; set; }
        public int Time_Preparation { get; set; }
        public int Servings { get; set; }
        public string Image_ { get; set; }
        public Alimentos oFood_Id { get; set; }
        public DateTime Date_ { get; set; }

        public string Steps { get; set; }
        public string Ingredients { get; set; }

        //public List<DatosNutricionales> Datos { get; set; } = new List<DatosNutricionales>();

    }
}
