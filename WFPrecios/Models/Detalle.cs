using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFPrecios.Models
{
    public class Detalle
    {
        public string folio { get; set; }
        public int pos { get; set; }
        public string vkorg { get; set; }
        public string vtweg { get; set; }
        public string spart { get; set; }
        public string kunnr { get; set; }
        public string pltyp { get; set; }
        public string pltyp_n { get; set; }
        public string estatus { get; set; }
        public string date { get; set; }

    }
}