﻿using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPrecios.Models;

namespace WFPrecios.Precios
{
    public partial class SolicitudLO : System.Web.UI.Page
    {
        List<Solicitud> sol = new List<Solicitud>();
        Fechas f = new Fechas();
        public string fecha_limite = "";
        public string monedas = "";
        public string um_meins = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hidUsuario.Value = Session["Usuario"].ToString();
                hidNumEmp.Value = Session["NumEmp"].ToString();
                //string[] a = Request.Form.AllKeys;
                //Conexion c = new Conexion("E-DESARROLL2", "Initial02");
                Conexion con = new Conexion();
                catalogos c = new catalogos();
                if (con.conectar())
                {
                    List<string> mon = con.monedas();
                    List<meins> mei = con.meinss();
                    monedas = formaMoneda("MXN", mon);
                    um_meins = formaMeins("", mei);
                    fecha_limite = con.fechaLimite();
                    if (Request.Form.AllKeys.Length == 0)
                    {
                        //IRfcTable lista_vkorg = c.ListaVKORG("");
                        //IRfcTable lista_vtweg = c.ListaVTWEG("");
                        IRfcTable lista_spart = con.ListaSPART("", hidNumEmp.Value, "", "", "02");
                        //IRfcTable lista_pltyp = c.ListaPLTYP();

                        //for (int i = 0; i < lista_vkorg.Count; i++)
                        //{
                        //    lista_vkorg.CurrentIndex = i;
                        //    txtVKORG.Items.Add(new ListItem(lista_vkorg.GetString("VKORG") + " " + lista_vkorg.GetString("VTEXT"), lista_vkorg.GetString("VKORG")));
                        //}
                        //for (int i = 0; i < lista_vtweg.Count; i++)
                        //{
                        //    lista_vtweg.CurrentIndex = i;
                        //    txtVTWEG.Items.Add(new ListItem(lista_vtweg.GetString("VTWEG") + " " + lista_vtweg.GetString("VTEXT"), lista_vtweg.GetString("VTWEG")));
                        //}
                        for (int i = 0; i < lista_spart.Count; i++)
                        {
                            lista_spart.CurrentIndex = i;
                            txtSPART.Items.Add(new ListItem(lista_spart.GetString("SPART") + " " + lista_spart.GetString("VTEXT"), lista_spart.GetString("SPART")));
                        }
                    }
                    if (Request.Form.AllKeys.Length > 0)
                    {
                        //string vkorg = Request.Form["txtVKORG"];
                        //string vtweg = Request.Form["txtVTWEG"];
                        string spart = Request.Form["txtSPART"];
                        //string kunnr = Request.Form["txtKUNNR"];
                        string matnr = Request.Form["txtMATNR"];


                        string p_inc = Request.Form["P_inc"];
                        string p_dec = Request.Form["P_dec"];

                        string cambio = p_inc + p_dec;
                        if (cambio.Trim().Equals(""))
                            cambio = "X";
                        else
                            cambio = "";

                        matnr = matnr.Split(' ')[0];

                        IRfcTable a720 = con.consultaLODetail(spart, matnr, "A504", chkCalidad.Checked);
                        txtPos.Value = a720.Count + "";
                        string tabla = "";
                        string valores = "";
                        tabla = "<table id='Table9' style='borde -width: 0px; border-style: None; width: 100 %; border-collapse: collapse;'><tbody><tr class='cell08'><td></td></tr></tbody></table>";
                        tabla += "<table id='tblTabla' border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                        tabla += "<tr><td class='tablahead'>Lote</td><td class='tablahead'>Unidad</td>" +
                                 "<td class='tablahead'>Precio actual</td><td class='tablahead'>Moneda</td>" +
                                 "<td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td>" +
                                 "<td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'>Comentario</td><td class='tablahead'></td><td class='tablahead'></td></tr>";// RSG 02.05.2017
                        if (a720.Count > 0)
                        {
                            List<SolicitudesL> ss = new List<SolicitudesL>();
                            for (int i = 0; i < a720.Count; i++)
                            {
                                SolicitudesL s = new SolicitudesL();
                                a720.CurrentIndex = i;
                                s.obj = a720.GetString("CHARG");
                                s.importe = a720.GetString("KBETR");
                                s.moneda = a720.GetString("KONWA");
                                s.importe_n = nuevoPrecio(a720.GetString("KBETR"));
                                s.moneda_n = a720.GetString("KONWA");
                                s.fecha_a = f.fecha(a720.GetString("DATAB"));
                                s.fecha_b = f.fecha(a720.GetString("DATBI"));
                                s.id = a720.GetString("KNUMH");
                                s.escala = a720.GetString("ESCALA");
                                s.desc2 = a720.GetString("KMEIN");

                                ss.Add(s);
                            }
                            int pos = 0;
                            for (int i = 0; i < ss.Count; i++)
                            {
                                //a720.CurrentIndex = i;
                                pos++;
                                tabla += "<tr id='tr-" + pos + "'><td class='tablaCent' id='OBJ-" + pos + "' ondblclick='escalas(this.id)'>";
                                tabla += "<input type='hidden' id='MATNR-" + pos + "' value='" + ss[i].obj + "' />";
                                tabla += ss[i].obj;
                                tabla += "</td>";
                                tabla += "<td class='tablaCent' id='DESC2-" + pos + "' ondblclick='escalas(this.id)'>";
                                tabla += "<select  id='MEINS-" + pos + "' class='cell031' style='width:40px;' onchange='cambiaMeins(this.id, this.value)' />";
                                tabla += formaMeins(ss[i].desc2, mei);
                                tabla += "</select>";
                                tabla += "</td>";
                                tabla += "<td class='tablaCent'>";
                                tabla += ss[i].importe;
                                tabla += "</td><td class='tablaCent'>";
                                tabla += ss[i].moneda;
                                tabla += "<td class='tablaCent'>";
                                tabla += "<input class='cell031' type='text' onchange='cambiaCant(this.id)' id='KBETR-" + pos + "' value='";
                                tabla += nuevoPrecio(ss[i].importe);
                                tabla += "' />";
                                tabla += "</td><td class='tablaCent'>";
                                tabla += "<select class='cell031' type='text' onchange='cambiaMone(this)' id='KONWA-" + pos + "' >";
                                //tabla += "<option>MXN</option><option>USD</option><option>EUR</option>";
                                tabla += formaMoneda(ss[i].moneda_n, mon);
                                tabla += "' </select>";
                                tabla += "</td><td class='tablaCent'>";
                                tabla += "<input class='cell031 datepicker' type='text' onchange='cambiaFechA(this.value, this.id);'  ondblclick='copiaA(this.value)' id='DATAB-" + pos + "' value='";
                                if (!a720.GetString("DATAB").Equals("0000-00-00"))
                                    tabla += f.fechaToOUT(f.fecha(a720.GetString("DATAB")));
                                tabla += "' />";
                                tabla += "</td><td class='tablaCent'>";
                                tabla += "<input class='cell031 datepicker' type='text' onchange='cambiaFechB(this.value, this.id);' ondblclick='copiaB(this.value)' id='DATBI-" + pos + "' value='";
                                if (!a720.GetString("DATBI").Equals("0000-00-00"))
                                    tabla += f.fechaToOUT(f.fecha(a720.GetString("DATBI")));
                                tabla += "' />";
                                tabla += "</td><td><input class='cell031' type='text' onchange='cambiaCome(this)' id='COMM-" + pos + "' /></td>";//05.05.2017

                                tabla += "<td><input type='checkbox' id='chk-" + pos + "'";
                                if (ss[i].escala.Equals("X"))
                                    tabla += "checked disabled='disabled' /><input type='hidden' id='knumh-" + pos + "' value='" + ss[i].id + "' /><script>generaEscalas('" + ss[i].id + "', " + pos + ");</script></td>";
                                else
                                    tabla += " disabled='disabled' /><input type='hidden' id='knumh-" + pos + "' value='' /></td>";

                                tabla += "<td class='tablaCent'><input type='button' id='btn-" + pos + "' value='-' class='btn2' onclick='elimina(this.id)' /></td></tr>";

                                valores += a720.GetString("CHARG") + "|" + a720.GetString("KBETR") + "|" + a720.GetString("KONWA") + "|";
                                valores += nuevoPrecio(a720.GetString("KBETR"));
                                if (a720.GetString("KONWA").Trim().Equals(""))
                                    valores += "|MXN|";
                                else
                                    valores += "|" + a720.GetString("KONWA") + "|";
                                if (!a720.GetString("DATAB").Equals("0000-00-00"))
                                    valores += f.fechaToOUT(f.fecha(a720.GetString("DATAB")));
                                valores += "|";
                                if (!a720.GetString("DATBI").Equals("0000-00-00"))
                                    valores += f.fechaToOUT(f.fecha(a720.GetString("DATBI")));
                                valores += "|" + a720.GetString("KNUMH") + "|X||";
                                valores += ss[i].desc2 + "|";
                                btnSubmit.Disabled = false;
                                btnAgregar.Disabled = false;
                                btnCargar.Disabled = false;
                            }
                        }
                        else
                        {

                            btnAgregar.Disabled = false;
                            btnCargar.Disabled = false;
                            txtPos.Value = "0";

                        }

                        tabla += "</tbody></table>";

                        lblTabla.InnerHtml = tabla;
                        txtTabla.Value = valores;

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

        private string nuevoPrecio(string p_ant)
        {
            string p_inc = Request.Form["P_inc"];
            string p_dec = Request.Form["P_dec"];
            //string c_inc = Request.Form["C_inc"];
            //string c_dec = Request.Form["C_dec"];

            decimal prec = decimal.Parse(p_ant);
            decimal p = 0;
            decimal p2 = 0;

            if (!p_inc.Trim().Equals(""))
            {
                p = decimal.Parse(p_inc);
            }
            else if (!p_dec.Trim().Equals(""))
            {
                p = decimal.Parse("-" + p_dec);
            }
            //else if (!c_inc.Trim().Equals(""))
            //{
            //    p2 = decimal.Parse(c_inc);
            //}
            //else if (!c_dec.Trim().Equals(""))
            //{
            //    p2 = decimal.Parse("-" + c_dec);
            //}
            else
            {
                prec = 0;
            }

            if (p != 0)
            {
                decimal num = prec * p / 100;
                prec = prec + num;
            }
            else if (p2 != 0)
            {
                prec = prec + p2;
            }

            decimal num2 = Math.Truncate(prec * 10);
            num2 = (prec * 10) - num2;
            prec = Math.Truncate(prec * 10);
            if (num2 != 0)
                prec++;
            prec = prec / 10;
            prec = Math.Round(prec, 2);

            return string.Format("{0:0.00}", prec);
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
        private string formaMeins(string meins, List<meins> meinsL)
        {
            string cadena = "";
            if (meins.Equals(""))
                meins = "M2";

            foreach (meins m in meinsL)
            {
                if (m.MSEHI.Equals(meins))
                    cadena += "<option value='" + m.MSEHI + "' selected>";
                else
                    cadena += "<option value='" + m.MSEHI + "'>";
                cadena += quitaComilla(m.MSEH3) + " - " + m.desc + "</option>";
            }

            return cadena;
        }

        private string quitaComilla(string cadena)
        {
            char[] cc = cadena.ToCharArray();
            string ret = "";
            foreach (char c in cc)
            {
                if (c.Equals('"'))
                {
                    ret += "'";
                }
                else
                {
                    ret += c;
                }
            }
            return ret;
        }
    }
}