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
    public partial class Busqueda : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        public string fecha_limite = "";
        public string monedas = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //hidUsuario.Value = Session["Usuario"].ToString();
                //hidNumEmp.Value = Session["NumEmp"].ToString();
                //string[] a = Request.Form.AllKeys;
                //Conexion c = new Conexion("E-DESARROLL2", "Initial02");
                Conexion con = new Conexion();
                catalogos c = new catalogos();
                if (con.conectar())
                {
                    List<string> mon = con.monedas();
                    monedas = formaMoneda("MXN", mon);
                    fecha_limite = con.fechaLimite();
                    if (Request.Form.AllKeys.Length == 0)
                    {
                        IRfcTable lista_vkorg = con.ListaVKORG("", "");
                        IRfcTable lista_vtweg = con.ListaVTWEG("", "", "");
                        IRfcTable lista_spart = con.ListaSPART("", "", "", "", "");
                        //IRfcTable lista_pltyp = con.ListaPLTYP();

                        txtVKORG.Items.Add(new ListItem("-", ""));
                        for (int i = 0; i < lista_vkorg.Count; i++)
                        {
                            lista_vkorg.CurrentIndex = i;
                            txtVKORG.Items.Add(new ListItem(lista_vkorg.GetString("VKORG") + " " + lista_vkorg.GetString("VTEXT"), lista_vkorg.GetString("VKORG")));
                        }
                        txtVTWEG.Items.Add(new ListItem("-", ""));
                        for (int i = 0; i < lista_vtweg.Count; i++)
                        {
                            lista_vtweg.CurrentIndex = i;
                            txtVTWEG.Items.Add(new ListItem(lista_vtweg.GetString("VTWEG") + " " + lista_vtweg.GetString("VTEXT"), lista_vtweg.GetString("VTWEG")));
                        }
                        txtSPART.Items.Add(new ListItem("-", ""));
                        for (int i = 0; i < lista_spart.Count; i++)
                        {
                            lista_spart.CurrentIndex = i;
                            txtSPART.Items.Add(new ListItem(lista_spart.GetString("SPART") + " " + lista_spart.GetString("VTEXT"), lista_spart.GetString("SPART")));
                        }
                    }
                    if (Request.Form.AllKeys.Length > 0)
                    {
                        string vkorg = Request.Form["txtVKORG"];
                        string vtweg = Request.Form["txtVTWEG"];
                        string spart = Request.Form["txtSPART"];
                        string pernr = Request.Form["txtPERNR"].Split(' ')[0];
                        string pltyp1 = Request.Form["txtPLTYP1"].Split(' ')[0];
                        string pltyp2 = Request.Form["txtPLTYP2"].Split(' ')[0];

                        string fecha_s = Request.Form["txtDATEs"];
                        if (!fecha_s.Equals(""))
                            fecha_s = f.fechaToSAP(f.fechaD(Request.Form["txtDATEs"]));
                        string fecha_a = Request.Form["txtDATEa"];
                        if (!fecha_a.Equals(""))
                            fecha_a = f.fechaToSAP(f.fechaD(Request.Form["txtDATEa"]));

                        string kunnr1 = Request.Form["txtKUNNR1"].Split(' ')[0];
                        string kunnr2 = Request.Form["txtKUNNR2"].Split(' ')[0];

                        //txtVKORG.Disabled = true;
                        //txtVTWEG.Disabled = true;
                        //txtSPART.Disabled = true;

                        //txtNAME1.Value = c.getColumnaCliente(vkorg, vtweg, spart, kunnr, "NAME1");

                        IRfcTable a720 = con.busquedaL(vkorg, vtweg, spart, pernr, pltyp1, pltyp2, fecha_s, fecha_a, kunnr1, kunnr2);
                        string tabla = "";
                        tabla = "<table id='tblTabla' border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                        tabla += "<tr><td class='tablahead'>Org. de ventas</td>";
                        tabla += "<td class='tablahead'>Canal Dist.</td>";
                        tabla += "<td class='tablahead'>Sector</td>";
                        tabla += "<td class='tablahead'>Cliente</td>";
                        tabla += "<td class='tablahead'>Lista de precios</td>";
                        //tabla += "<td class='tablahead'>Descripción</td>";
                        tabla += "<td class='tablahead'>Lista Anterior</td>";
                        //tabla += "<td class='tablahead'>Descripción</td>";
                        tabla += "<td class='tablahead'>Fecha Solicitud</td>";
                        tabla += "<td class='tablahead'>Solicitante</td>";
                        tabla += "<td class='tablahead'>Fecha Cambio</td></tr>";
                        if (a720.Count > 0)
                        {
                            int pos = 0;
                            for (int i = 0; i < a720.Count; i++)
                            {
                                a720.CurrentIndex = i;
                                pos++;
                                tabla += "<tr><td class='tablaCent'>";
                                tabla += a720.GetString("VKORG");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("VTWEG");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("SPART");
                                tabla += "</td>";
                                tabla += "<td class='tablaIzq'>";
                                tabla += a720.GetString("KUNNR") + " " + a720.GetString("NAME1");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("LP_NVO") + " ";
                                //tabla += "</td>";
                                //tabla += "<td class='tablaIzq'>";
                                tabla += a720.GetString("DESC_NVO");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("LP_ANT") + " ";
                                //tabla += "</td>";
                                //tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("DESC_ANT");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += f.fechaToOUT(f.fecha(a720.GetString("FECHA_H")));
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("PERNR");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                string d = a720.GetString("FECHA_AUT");
                                if (!d.Equals("0000-00-00"))
                                    tabla += f.fechaToOUT(f.fecha(d));
                                tabla += "</td></tr>";
                            }
                        }
                        tabla += "</tbody></table>";

                        lblTabla.InnerHtml = tabla;
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Redirect("../../../Default.aspx");
                var page = HttpContext.Current.Handler as Page;
                if (page != null)
                    page.ClientScript.RegisterClientScriptBlock(typeof(string), "Redirect", "window.parent.location='https://www.terzaonline.com/nworkflow/login/';", true);
                //ClientScriptManager.RegisterClientScriptBlock(this.GetType(), "RedirectScript", "window.parent.location = '../../../Default.aspx'", true);
            }
        }

        private string formaMoneda(string moneda, List<string> monedas)
        {
            string cadena = "";
            if (moneda.Equals(""))
                moneda = "MXN";
            Conexion con = new Conexion();

            if (!monedas.Contains(moneda))
                monedas.Add(moneda);

            foreach (string m in monedas)
            {
                if (m.Equals(moneda))
                    cadena += "<option value='" + m + "' selected>";
                else
                    cadena += "<option value='" + m + "'>";
                cadena += m + "</option>";
            }

            return cadena;
        }
    }
}