using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPrecios.Models;

namespace WFPrecios
{
    public partial class Autoriza : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipo = Request.Form["hidTipo"];
            string folio = Request.Form["hidFolio"];
            string oper = Request.Form["hidOper"];
            string posi = Request.Form["hidPosi"];
            string comentario = Request.Form["txtCOMM2"];

            string fecha = f.fechaToSAP(DateTime.Now);
            string hora = f.hora(DateTime.Now);

            Conexion con = new Conexion();
            string accion = con.accionBitacora(tipo, folio, oper, posi, comentario);

            if (accion.Equals("1"))
            {
                string opera = "";
                if (oper.Equals("A"))
                {
                    opera = "Aprobada";
                }
                else if (oper.Equals("R"))
                {
                    opera = "Rechazada";
                }

                if (!accion.Equals(""))
                    accion = "La solicitud ha sido " + opera + "<br />";
            }
            else if (accion.Equals("A"))
            {
                accion = "<span class='red-text'>Solicitud procesada anteriormente</span>";
            }
            else
            {
                accion = "Hubo un error al procesar la Solicitud.";
            }
            lblFolio.InnerHtml = "<p class=''>" + accion + "</p>";
        }
    }
}