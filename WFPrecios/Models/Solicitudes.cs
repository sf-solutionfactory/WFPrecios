using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFPrecios.Models
{
    public class Solicitudes
    {
        public string vkorg { get; set; }
        public string vtweg { get; set; }
        public string spart { get; set; }
        public string kunnr { get; set; }
        public string pltyp { get; set; }
        public string pltyp_desc { get; set; }
        public string pltyp_n { get; set; }
        public string pltyp_n_desc { get; set; }
        public string name1 { get; set; }
        public bool error { get; set; }
        public DateTime date { get; set; }
        public Solicitudes()
        {
            vkorg = "";
            vtweg = "";
            spart = "";
            kunnr = "";
            pltyp = "";
            pltyp_desc = "";
            pltyp_n = "";
            pltyp_n_desc = "";
            name1 = "";
            error = false;
            date = DateTime.Now;
        }
    }
}