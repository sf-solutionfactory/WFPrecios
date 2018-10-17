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
    public partial class Default : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        protected void Page_Load(object sender, EventArgs e)
        {
            string table = "";
            Conexion con = new Conexion();
            IRfcTable delega;

            //string[] keys = Session.
            //if (IsPostBack)
            //{
            if (txtDELEGAR.Items.Count > 0)
            {
                if (txtDELEGAR.SelectedItem.Value == "-")
                {
                    Session["Usuario"] = Session["UsuarioP"].ToString();
                    Session["NumEmp"] = Session["NumEmpP"].ToString();
                    Session["TipoEmp"] = Session["TipoEmpP"].ToString();
                }
                else
                {
                    Session["NumEmp"] = txtDELEGAR.SelectedItem.Value;
                }
            }
            //}
            try
            {
                hidNumEmp.Value = Session["NumEmp"].ToString();
                if (hidNumEmp.Value.Equals(Session["NumEmpP"].ToString()))
                {
                    hidUsuario.Value = Session["Usuario"].ToString();
                    hidTipoEmp.Value = Session["TipoEmp"].ToString();

                    if (txtDELEGAR.Items.Count.Equals(0))
                    {
                        delega = con.usuariosDelega(hidNumEmp.Value);
                        if (delega.Count > 0)
                        {
                            txtDELEGAR.Items.Add(new ListItem("Seleccione Usuario", "-"));
                        }
                        for (int i = 0; i < delega.Count; i++)
                        {
                            delega.CurrentIndex = i;
                            txtDELEGAR.Items.Add(new ListItem(delega.GetString("ZUEMP") + " " + delega.GetString("SNAME"), delega.GetString("ZUEMP")));
                        }
                    }
                }
                else
                {
                    delega = con.usuariosDelega(Session["NumEmpP"].ToString());
                    for (int i = 0; i < delega.Count; i++)
                    {
                        delega.CurrentIndex = i;
                        if (delega.GetString("ZUEMP").Equals(hidNumEmp.Value))
                        {
                            hidUsuario.Value = delega.GetString("ZURED");
                            hidTipoEmp.Value = delega.GetString("ZTUSR");
                            hidNumEmp.Value = delega.GetString("ZUEMP");

                            Session["Usuario"] = delega.GetString("ZURED");
                            Session["NumEmp"] = delega.GetString("ZUEMP");
                            Session["TipoEmp"] = delega.GetString("ZTUSR");
                        }
                    }
                    if (txtDELEGAR.Items.Count.Equals(0))
                    {
                        if (delega.Count > 0)
                        {
                            txtDELEGAR.Items.Add(new ListItem("Seleccione Usuario", "-"));
                        }
                        for (int i = 0; i < delega.Count; i++)
                        {
                            delega.CurrentIndex = i;
                            txtDELEGAR.Items.Add(new ListItem(delega.GetString("ZUEMP") + " " + delega.GetString("SNAME"), delega.GetString("ZUEMP")));
                        }

                        txtDELEGAR.SelectedValue = hidNumEmp.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    hidUsuario.Value = Session["UsuarioP"].ToString();
                    hidNumEmp.Value = Session["NumEmpP"].ToString();
                    hidTipoEmp.Value = Session["TipoEmpP"].ToString();

                    if (txtDELEGAR.Items.Count.Equals(0))
                    {
                        delega = con.usuariosDelega(hidNumEmp.Value);
                        if (delega.Count > 0)
                        {
                            txtDELEGAR.Items.Add(new ListItem("Seleccione Usuario", "-"));
                        }
                        for (int i = 0; i < delega.Count; i++)
                        {
                            delega.CurrentIndex = i;
                            txtDELEGAR.Items.Add(new ListItem(delega.GetString("ZUEMP") + " " + delega.GetString("SNAME"), delega.GetString("ZUEMP")));
                        }
                        //txtDELEGA
                        if (txtDELEGAR.Items.Count > 0)
                        {
                            if (txtDELEGAR.SelectedItem.Value == "-")
                            {
                                Session["NumEmp"] = hidNumEmp.Value;
                                Session["Usuario"] = hidUsuario.Value;
                                Session["TipoEmp"] = hidTipoEmp.Value;
                            }
                            else
                            {
                                for (int i = 0; i < delega.Count; i++)
                                {
                                    delega.CurrentIndex = i;
                                    if (txtDELEGAR.SelectedItem.Value.Equals(delega.GetString("ZUEMP")))
                                    {
                                        Session["NumEmp"] = delega.GetString("ZURED");
                                        Session["Usuario"] = delega.GetString("ZUEMP");
                                        Session["TipoEmp"] = delega.GetString("ZTUSR");

                                        hidUsuario.Value = Session["Usuario"].ToString();
                                        hidNumEmp.Value = Session["NumEmp"].ToString();
                                        hidTipoEmp.Value = Session["TipoEmp"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exE)
                {
                    Response.Redirect("https://www.terzaonline.com/nworkflow/login/");
                }
            }
            try
            {
                if (con.conectar())
                {
                    if (txtDELEGAR.Items.Count.Equals(0))
                    {
                        Session["NumEmp"] = hidNumEmp.Value;
                        Session["Usuario"] = hidUsuario.Value;
                        Session["TipoEmp"] = hidTipoEmp.Value;
                        delegar.Visible = false;
                    }

                    IRfcTable tab = con.tablaCabeceraL("", hidNumEmp.Value); //Trae Solicitudes de Listas de precio

                    if (tab != null)
                    {
                        for (int i = 0; i < tab.Count; i++)
                        {
                            tab.CurrentIndex = i;
                            string estatus = con.estatusSol("02", tab.GetString("ID_SOLICITUD"));
                            //switch (estatus)
                            //{
                            //    case "P":
                            //        estatus = "Pendiente";
                            //        break;
                            //}

                            string tipo = tab.GetString("TIPO");
                            switch (tipo)
                            {
                                case "CP":
                                    tipo = "Pedido";
                                    break;
                                case "ML":
                                    tipo = "Lote";
                                    break;
                                default:
                                    tipo = "Precio";
                                    break;
                            }

                            string autorizante = "";
                            table += "<tr>";
                            table += "<td class='tablaCent'>";
                            table += tab.GetString("ID_SOLICITUD");
                            table += "</td>";
                            //table += "<td class='tablaCent'>";
                            //table += tab.GetString("USUARIO");
                            //table += "</td>";
                            table += "<td class='tablaCent'>";
                            table += f.fechaToOUT(f.fecha(tab.GetString("FECHA"))) + " " + tab.GetString("HORA");
                            table += "</td>";
                            table += "<td class='tablaCent'>";
                            table += tipo;
                            table += "</td>";
                            table += "<td class='tablaCent'>";
                            table += estatus;
                            table += "</td>";
                            table += "<td class='tablaCent'>";
                            ////table += tab.GetString("SIGUENTE EN AUTORIZAR");
                            autorizante = con.porAutorizar("02", tab.GetString("ID_SOLICITUD"));
                            if (estatus.Equals("Pendiente"))
                            {
                                table += autorizante;
                            }
                            table += "</td>";

                            autorizante = autorizante.Split('-')[0].Trim();
                            if (!autorizante.Equals(""))
                            {
                                ////if (autorizante.Equals(hidNumEmp.Value.Trim()))
                                ////{
                                ////    table += "<td class='tablaCent'><a class='tablaLink' href='Procesa.aspx?Tipo=02&Folio=";
                                ////    table += tab.GetString("ID_SOLICITUD");
                                ////    table += "&Oper=N&posi=";
                                ////    table += con.sigFolio("02", tab.GetString("ID_SOLICITUD"));
                                ////}
                                ////else
                                ////{
                                table += "<td class='tablaCent'><a class='tablaLink' href='DetalleSolicitud.aspx?Folio=";
                                table += tab.GetString("ID_SOLICITUD");
                                ////}
                            }
                            table += "'>Detalle</a></td>";
                            table += "</tr>";
                        }
                        lblTabla.InnerHtml = table;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("https://www.terzaonline.com/nworkflow/login/");
            }
        }

        protected void txtDELEGAR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtDELEGAR_TextChanged(object sender, EventArgs e)
        {

        }
    }
}