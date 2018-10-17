using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFPrecios.Models
{
    public class Cabecera
    {
        public string folio { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string usuario { get; set; }
        public string pernr { get; set; }
        public string estatus { get; set; }
        public string visto { get; set; }
        public string comentario { get; set; }

    }
}