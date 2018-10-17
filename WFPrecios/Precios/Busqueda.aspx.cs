using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPrecios.Models;

namespace WFPrecios.Precios
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
                        string kunnr = Request.Form["txtKUNNR"].Split(' ')[0];
                        string inc = Request.Form["P_inc"];
                        string dec = Request.Form["P_dec"];

                        string fecha_s = Request.Form["txtDATEs"];
                        if (!fecha_s.Equals(""))
                            fecha_s = f.fechaToSAP(f.fechaD(Request.Form["txtDATEs"]));
                        string fecha_a = Request.Form["txtDATEa"];
                        if (!fecha_a.Equals(""))
                            fecha_a = f.fechaToSAP(f.fechaD(Request.Form["txtDATEa"]));
                        string fecha_i = Request.Form["txtDATEi"];
                        if (!fecha_i.Equals(""))
                            fecha_i = f.fechaToSAP(f.fechaD(Request.Form["txtDATEi"]));
                        string fecha_f = Request.Form["txtDATEf"];
                        if (!fecha_f.Equals(""))
                            fecha_f = f.fechaToSAP(f.fechaD(Request.Form["txtDATEf"]));
                        string matkl = Request.Form["txtMATKL"].Split(' ')[0];
                        string pltyp = Request.Form["txtPLTYP"].Split(' ')[0];
                        string matnr = Request.Form["txtMATNR"].Split(' ')[0];

                        //txtVKORG.Disabled = true;
                        //txtVTWEG.Disabled = true;
                        //txtSPART.Disabled = true;

                        //txtNAME1.Value = c.getColumnaCliente(vkorg, vtweg, spart, kunnr, "NAME1");

                        IRfcTable a720 = con.busquedaP(vkorg, vtweg, spart, pernr, kunnr, inc, dec, fecha_s, fecha_a, fecha_i, fecha_f, matkl, pltyp, matnr);
                        string tabla = "";
                        tabla = "<table id='tblTabla' border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                        tabla += "<tr><td class='tablahead'>Org. de ventas</td>";
                        tabla += "<td class='tablahead'>Canal Dist.</td>";
                        tabla += "<td class='tablahead'>Sector</td>";
                        tabla += "<td class='tablahead'>Solicitante</td>";
                        tabla += "<td class='tablahead'>Cliente</td>";
                        tabla += "<td class='tablahead'>Obj</td>";
                        tabla += "<td class='tablahead'>Denominación</td>";
                        tabla += "<td class='tablahead'>UM</td>";
                        tabla += "<td class='tablahead'>Precio Ant</td>";
                        tabla += "<td class='tablahead'>Moneda</td>";
                        tabla += "<td class='tablahead'>Precio Nvo</td>";
                        tabla += "<td class='tablahead'>Moneda</td>";
                        tabla += "<td class='tablahead'>Difererncia</td>";
                        tabla += "<td class='tablahead'>Válido de</td>";
                        tabla += "<td class='tablahead'>Válido a </td>";
                        //tabla += "<td class='tablahead'>Solicitante</td>";
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
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("PERNR");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("KUNNR");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                string obj = a720.GetString("MATNR");
                                if (!obj.Equals(""))
                                    tabla += obj;
                                obj = a720.GetString("MATKL");
                                if (!obj.Equals(""))
                                    tabla += obj;
                                obj = a720.GetString("EBELN");
                                if (!obj.Equals(""))
                                    tabla += obj;
                                obj = a720.GetString("CHARG");
                                if (!obj.Equals(""))
                                    tabla += obj;
                                tabla += "</td>";
                                tabla += "<td class='tablaIzq'>";
                                tabla += a720.GetString("NAME1");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("MEINS");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("PR_ANT");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("MON_ANT");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("PR_NVO");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("MON_NVO");
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += a720.GetString("PORCENTAJE") + "%";
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += f.fechaToOUT(f.fecha(a720.GetString("FECHA_INI")));
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += f.fechaToOUT(f.fecha(a720.GetString("FECHA_FIN")));
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                string d = a720.GetString("FECHA_AUT");
                                if (!d.Equals("0000-00-00"))
                                    tabla += f.fechaToOUT(f.fecha(d));
                                tabla += "</td>";
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