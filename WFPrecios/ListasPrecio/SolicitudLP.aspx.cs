using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPrecios.Models;

namespace WFPrecios.ListasPrecio
{
    public partial class SolicitudLP : System.Web.UI.Page
    {
        List<Solicitudes> sol = new List<Solicitudes>();
        public string fecha_limite = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Conexion c = new Conexion("E-DESARROLL2", "Initial02");

            try
            {
                hidUsuario.Value = Session["Usuario"].ToString();
                hidNumEmp.Value = Session["NumEmp"].ToString();
                hidTipoEmp.Value = Session["TipoEmp"].ToString();

                Conexion c = new Conexion();
                if (c.conectar())
                {
                    IRfcTable lista_vkorg = c.ListaVKORG("", "");
                    IRfcTable lista_vtweg = c.ListaVTWEG("", "", "");
                    IRfcTable lista_spart = c.ListaSPART("", hidNumEmp.Value, "", "", "01");
                    IRfcTable lista_pltyp;
                    if (lista_spart.Count > 0)
                    {
                        lista_spart.CurrentIndex = 0;
                        lista_pltyp = c.ListaPLTYP(hidNumEmp.Value, lista_spart.GetString("SPART"), "");
                        for (int i = 0; i < lista_pltyp.Count; i++)
                        {
                            lista_pltyp.CurrentIndex = i;
                            txtPLTYP_N.Items.Add(new ListItem(lista_pltyp.GetString("PLTYP") + " " + lista_pltyp.GetString("PTEXT"), lista_pltyp.GetString("PLTYP")));
                        }
                    }
                    fecha_limite = c.fechaLimite();

                    for (int i = 0; i < lista_vkorg.Count; i++)
                    {
                        lista_vkorg.CurrentIndex = i;
                        txtVKORG.Items.Add(new ListItem(lista_vkorg.GetString("VKORG") + " " + lista_vkorg.GetString("VTEXT"), lista_vkorg.GetString("VKORG")));
                        if (lista_vkorg.GetString("VKORG").Equals("1000"))
                            txtVKORG.SelectedIndex = i;
                    }
                    for (int i = 0; i < lista_vtweg.Count; i++)
                    {
                        lista_vtweg.CurrentIndex = i;
                        txtVTWEG.Items.Add(new ListItem(lista_vtweg.GetString("VTWEG") + " " + lista_vtweg.GetString("VTEXT"), lista_vtweg.GetString("VTWEG")));
                        if (lista_vtweg.GetString("VTWEG").Equals("10"))
                            txtVTWEG.SelectedIndex = i;
                    }
                    for (int i = 0; i < lista_spart.Count; i++)
                    {
                        lista_spart.CurrentIndex = i;
                        txtSPART.Items.Add(new ListItem(lista_spart.GetString("SPART") + " " + lista_spart.GetString("VTEXT"), lista_spart.GetString("SPART")));
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("https://www.terzaonline.com/nworkflow/login/");
            }
        }
    }
}