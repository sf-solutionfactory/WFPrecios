using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPrecios.Models;

namespace WFPrecios
{
    public partial class MailV : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipoF = Request.QueryString["Tipo"];
            string folio = Request.QueryString["Folio"];
            //string posi = Request.QueryString["Posi"];
            //string oper = Request.QueryString["Oper"];
            string date = Request.QueryString["Date"];
            DateTime dia = f.fechafromSAP(date);

            Conexion con = new Conexion();
            catalogos c = new catalogos();

            //if (!oper.Equals("N"))
            //{
            //    string href = "";
            //    string[] hh = Request.Url.AbsoluteUri.Split('/');

            //    for (int i = 0; i < hh.Length - 1; i++)
            //    {
            //        href += hh[i] + "/";
            //    }

            //}

           
            if (con.conectar())
            {
                if (tipoF.Equals("01"))
                {
                    IRfcTable header = con.consultaCabecera(folio, "");
                    IRfcTable detail = con.consultaDetalle(folio, "");
                    List<Solicitudes> ss = new List<Solicitudes>();

                    if (header.Count > 0)
                    {

                        txtFolio.InnerHtml = "La vigencia de la solicitud ";
                        txtFolio.InnerHtml += header.GetString("ID_SOLICITUD");
                        txtFolio.InnerHtml += " va a expirar";

                        txtPERNR.InnerText = header.GetString("PERNR") + " " + con.nombrePERNR(header.GetString("PERNR"));
                        txtDATE.InnerText = f.fechaToOUT(f.fecha(header.GetString("FECHA")));
                        string comm = "";
                        comm += "<table border='0' style='border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;'><tbody>";
                        comm += "<tr><td class='cell12' style='width:1150px;'>Comentario</td></tr><tr><td style='text-align:left;font-size:12px;'>";
                        comm += header.GetString("COMMENTS");
                        comm += "</td></tr></tbody></table>";
                        txtCOMM.InnerHtml = comm;

                        for (int i = 0; i < detail.Count; i++)
                        {
                            detail.CurrentIndex = i;
                            Solicitudes s = new Solicitudes();
                            s.vkorg = detail.GetString("VKORG");
                            s.vtweg = detail.GetString("VTWEG");
                            s.spart = detail.GetString("SPART");
                            s.kunnr = detail.GetString("KUNNR");
                            s.pltyp = detail.GetString("LP_ANT");
                            s.pltyp_n = detail.GetString("LP_NVO");
                            s.date = f.fecha(detail.GetString("FECHA"));
                            if (!s.pltyp.Equals(""))
                                //s.pltyp_desc = c.getDescLP(s.pltyp);
                                s.pltyp_desc = con.getLP_Desc("", s.pltyp);
                            else
                                s.pltyp_desc = "Sin lista de precios";

                            if (!s.pltyp_n.Equals(""))
                                //s.pltyp_n_desc = c.getDescLP(s.pltyp_n);
                                s.pltyp_n_desc = con.getLP_Desc("", s.pltyp_n);
                            else
                                s.pltyp_n_desc = "Sin lista de precios";

                            s.name1 = c.getColumnaCliente(s.vkorg, s.vtweg, s.spart, s.kunnr, "NAME1");

                            ss.Add(s);
                        }
                        string tab = "";

                        tab = "<table border='0' style='border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;'><tbody>";
                        tab += "<tr><td class='cell12'>Org. Compras</td><td class='cell12'>Canal Dist.</td><td class='cell12'>Sector</td><td class='cell12'>Cliente</td><td class='cell12'>Nombre</td><td class='cell12'>Lista anterior</td><td class='cell12'>Nueva Lista de precios</td><td class='cell12'>Vigencia</td></tr>";
                        foreach (Solicitudes s in ss)
                        {
                            string style = "";
                            if (s.error)
                                style = "background-color: red !important;";

                            tab += "<tr style='" + style + "'>";

                            if (s.error)
                                style = "white-text";
                            tab += "<td class='cell18 " + style + "'>" + s.vkorg + "</td>";
                            tab += "<td class='cell18 " + style + "'>" + s.vtweg + "</td>";
                            tab += "<td class='cell18 " + style + "'>" + s.spart + "</td>";
                            tab += "<td class='cell18 " + style + "'>" + s.kunnr + "</td>";
                            tab += "<td class='cell17 " + style + "'>" + s.name1 + "</td>";
                            tab += "<td class='cell18 " + style + "'>" + s.pltyp_desc + "</td>";
                            tab += "<td class='cell18 " + style + "'>" + s.pltyp_n_desc + "</td>";
                            int com = s.date.CompareTo(dia);
                            if (!com.Equals(0))
                                tab += "<td class='cell18 " + style + "'>" + s.date.ToString("dd/MM/yyyy") + "</td>";
                            else
                                tab += "<td class='cell18 white-text' style='background-color: red !important;'>" + s.date.ToString("dd/MM/yyyy") + "</td>";

                            tab += "</tr>";
                        }
                        tab += "</tbody></table>";
                        lblTabla.InnerHtml = tab;
                    }
                    else
                    {
                        Response.Redirect("Default.aspx", false);
                    }
                }
                

                IRfcTable bitacora = con.getBitacora(tipoF, folio);
                string tabla = "";

                tabla = "<table border='0' style='border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;'><tbody>";
                tabla += "<tr><td class='cell12'>Empleado</td><td class='cell12' style='width: 100px'>Evento</td><td class='cell12' style='width: 150px'>Fecha autorizar</td><td class='cell12' style='width: 150px'>Fecha Procesada</td><td class='cell12' style='width: 50px'>Status</td><td class='cell12'>Comentario</td></tr>";

                for (int i = 0; i < bitacora.Count; i++)
                {
                    bitacora.CurrentIndex = i;
                    tabla += "<tr><td class='cell17'>";
                    tabla += bitacora.GetString("ZUSRA") + " - " + bitacora.GetString("ZDUSA");
                    tabla += "</td>";
                    tabla += "<td class='cell18' style='width: 100px'>";
                    tabla += bitacora.GetString("ZDEEV");
                    tabla += "</td>";
                    tabla += "<td class='cell18' style='width: 150px'>";
                    tabla += bitacora.GetString("ZFEAL") + "[" + bitacora.GetString("ZHOAL") + "]";
                    tabla += "</td>";
                    tabla += "<td class='cell18' style='width: 150px'>";
                    tabla += bitacora.GetString("ZFEUM") + "[" + bitacora.GetString("ZHOUM") + "]";
                    tabla += "</td>";
                    tabla += "<td class='cell18' style='width: 50px'>";
                    tabla += bitacora.GetString("ZSWAR");
                    tabla += "</td>";
                    tabla += "<td class='cell18'>";
                    tabla += bitacora.GetString("ZCOME");
                    //tabla += "sad ads fas a s fasifu sa disad iasduasdiasud a sdaiud asi duiasdhasoidha soih sad sahdjashdiusahd sa ";
                    tabla += "</td>";
                    tabla += "</tr>";

                }

                lblBItacora.InnerHtml = tabla;
            }
        }
        private List<SolicitudesL> obtenerDatos(List<SolicitudesL> sol, string tipo)
        {
            List<SolicitudesL> ss = new List<SolicitudesL>();

            List<string> matnr = new List<string>();
            foreach (SolicitudesL s in sol)
            {
                matnr.Add(s.obj);
            }
            Conexion con = new Conexion();
            List<material> mm = new List<material>();
            if (tipo.Equals("M"))
                mm = con.TcolumnaMatnr(matnr);
            else
                mm = con.TcolumnaMatkl(matnr);


            foreach (SolicitudesL s in sol)
            {
                foreach (material m in mm)
                {
                    if (m.matnr.Equals(s.obj))
                    {
                        s.desc = m.maktg;
                        s.desc2 = m.meins;
                    }
                }
                ss.Add(s);
            }

            return ss;
        }
    }
}