using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPrecios.Models;

namespace WFPrecios.ListasPrecio
{
    public partial class EnviaSolicitud : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] tabla = Request.Form["txtTabla"].Split('|');
            List<Solicitudes> solicitud = new List<Solicitudes>();

            for (int i = 0; i < tabla.Length - 1; i += 8)
            {
                Solicitudes s = new Solicitudes();
                s.vkorg = tabla[i];
                s.vtweg = tabla[i + 1];
                s.spart = tabla[i + 2];
                s.kunnr = tabla[i + 3];
                s.pltyp = tabla[i + 4];
                s.pltyp_n = tabla[i + 5];
                s.date = f.fechaD(tabla[i + 6]);

                solicitud.Add(s);
            }
            Conexion con = new Conexion();
            Cabecera c = new Cabecera();
            c.folio = "";
            //c.tipo = "LP";
            c.fecha = f.fechaToSAP(DateTime.Now);
            c.hora = f.hora(DateTime.Now);
            c.usuario = Request.Form["hidUsuario"];
            c.estatus = "P";
            c.visto = " ";
            c.pernr = Request.Form["hidNumEmp"];
            string comentarios = Request.Form["txtCOMM"];
            c.comentario = comentarios;

            List<Detalle> ds = new List<Detalle>();
            for (int i = 0; i < solicitud.Count; i++)
            {
                Detalle d = new Detalle();
                d.folio = "";
                d.pos = i + 1;
                d.vkorg = solicitud[i].vkorg;
                d.vtweg = solicitud[i].vtweg;
                d.spart = solicitud[i].spart;
                d.kunnr = solicitud[i].kunnr;
                d.pltyp = solicitud[i].pltyp;
                d.pltyp_n = solicitud[i].pltyp_n;
                d.estatus = " ";
                d.date = f.fechaToSAP(solicitud[i].date);

                ds.Add(d);
            }


            string folio = con.InsertaSolicitud(c, ds);
            if (!folio.Equals(""))
                folio = "La solicitud de modificación de Listas de precio ha sido recibida, y será procesada con el/los folio(s)<br />" + folio;
            else
                folio = "Hubo un error en la creación de la Solicitud.";
            lblFolio.InnerHtml = "<p class=''>" + folio + "</p>";
        }
    }
}