using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace EXLuisMontes.Modelos
{
    class Datos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(250)]
        public string Latitud { get; set; }

        [MaxLength(250)]
        public string Longitud { get; set; }

        [MaxLength(250)]
        public string UbicacionLarga { get; set; }

        [MaxLength(250)]
        public string UbicacionCorta { get; set; }
    }
}
