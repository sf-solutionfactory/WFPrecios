using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFPrecios.Models
{
    public class SolicitudesL
    {
        public Solicitudes sol { get; set; }
        public string id { get; set; }
        public string obj { get; set; }
        public string desc { get; set; }
        public string desc2 { get; set; }
        public string importe { get; set; }
        public string moneda { get; set; }
        public string importe_n { get; set; }
        public string moneda_n { get; set; }
        public DateTime fecha_a { get; set; }
        public DateTime fecha_b { get; set; }
        public string modif { get; set; }
        public string porcentaje { get; set; }
        public bool error { get; set; }
        public string tipo_error { get; set; }
        public string comentario { get; set; }
        public string escala { get; set; }
        public string pos { get; set; }
        public SolicitudesL()
        {
            tipo_error = "";
        }


        public string menge { get; set; } //ADD RSG 15.05.2017
        public string um1 { get; set; } //ADD RSG 15.05.2017
        public string kbetr { get; set; } //ADD RSG 15.05.2017
        public string um2 { get; set; } //ADD RSG 15.05.2017
    }
}