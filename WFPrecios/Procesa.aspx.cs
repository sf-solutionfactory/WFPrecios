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
    public partial class Procesa : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        public string oper = "";
        List<meins> meinss;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipoF = Request.QueryString["Tipo"];
            string folio = Request.QueryString["Folio"];
            oper = Request.QueryString["Oper"];
            string posi = Request.QueryString["posi"];

            hidTipo.Value = tipoF;
            hidFolio.Value = folio;
            hidOper.Value = "N";
            hidPosi.Value = posi;

            Conexion con = new Conexion();
            meinss = con.meinss();
            catalogos c = new catalogos();

            if (oper != null)
                if (oper.Equals("N"))
                {
                    string links = "";
                    links += "<a href='Procesa.aspx";
                    links += "?Tipo=" + tipoF + "&Folio=" + folio + "&Oper=A&Posi=" + posi;
                    links += "'>Autorizar</a>&nbsp;|&nbsp;<a href='Procesa.aspx";
                    links += "?Tipo=" + tipoF + "&Folio=" + folio + "&Oper=R&Posi=" + posi;
                    links += "'>Rechazar</a>&nbsp;&nbsp;";

                    hidOper.Value = oper;

                    lblLinks.InnerHtml = links;

                    if (con.conectar())
                    {
                        if (tipoF.Equals("01"))
                        {
                            IRfcTable header = con.consultaCabecera(folio, "");
                            IRfcTable detail = con.consultaDetalle(folio, "");
                            List<Solicitudes> ss = new List<Solicitudes>();

                            if (header.Count > 0)
                            {
                                txtFolio.InnerText = "Autorización de Solicitud " + header.GetString("ID_SOLICITUD");
                                txtPERNR.InnerText = header.GetString("PERNR") + " " + con.nombrePERNR(header.GetString("PERNR"));
                                txtDATE.InnerText = f.fechaToOUT(f.fecha(header.GetString("FECHA")));
                                txtCOMM.InnerText = header.GetString("COMMENTS");

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

                                tab = "<table border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
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
                                    tab += "<td class='cell18 " + style + "'>" + s.date.ToString("dd/MM/yyyy") + "</td>";


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
                        else if (tipoF.Equals("02"))
                        {

                            IRfcTable header = con.consultaCabeceraL(folio, "");
                            IRfcTable detail = con.consultaDetalleL(folio, "");
                            List<SolicitudesL> ss = new List<SolicitudesL>();

                            if (header.Count > 0)
                            {
                                txtFolio.InnerText = "Autorización de Solicitud " + header.GetString("ID_SOLICITUD");
                                txtPERNR.InnerText = header.GetString("PERNR") + " " + con.nombrePERNR(header.GetString("PERNR"));
                                txtDATE.InnerText = f.fechaToOUT(f.fecha(header.GetString("FECHA")));
                                txtCOMM.InnerText = header.GetString("COMMENTS");

                                string vkorg = header.GetString("VKORG");
                                string vtweg = header.GetString("VTWEG");
                                string spart = header.GetString("SPART");
                                string kunnr = header.GetString("KUNNR");
                                string pltyp = header.GetString("PLTYP");
                                string matnr = header.GetString("MATNR");

                                string cabecera = "<table style='position: relative; left: 50%; margin-left: -200px'>";
                                //cabecera += "<tr>";
                                if (!vkorg.Equals(""))
                                {
                                    cabecera += "<tr><td class='cell03'>Org. Compras</td>";
                                    cabecera += "<td class='cell05'>" + vkorg + " " + con.ListaVKORG(vkorg, "").GetString("VTEXT") + "</td></tr>";
                                }
                                if (!vtweg.Equals(""))
                                {
                                    cabecera += "<tr><td class='cell03'>Canal Distribución</td>";
                                    cabecera += "<td class='cell05'>" + vtweg + " " + con.ListaVTWEG(vtweg, "", "").GetString("VTEXT") + "</td></tr>";
                                }
                                if (!spart.Equals(""))
                                {
                                    cabecera += "<tr><td class='cell03'>Sector</td>";
                                    cabecera += "<td class='cell05'>" + spart + " " + con.ListaSPART(spart, "", "", "", "").GetString("VTEXT") + "</td></tr>";
                                }
                                if (!kunnr.Equals(""))
                                {
                                    cabecera += "<tr><td class='cell03'>Cliente</td>";
                                    cabecera += "<td class='cell05'>" + kunnr + " " + con.getCliente(vkorg, vtweg, spart, kunnr).GetString("NAME1") + "</td></tr>";
                                }
                                if (!pltyp.Equals(""))
                                {
                                    cabecera += "<tr><td class='cell03'>Lista de precios</td>";
                                    cabecera += "<td class='cell05'>" + con.getLP_Desc("", pltyp) + "</td></tr>";
                                }
                                if (!matnr.Equals(""))
                                {
                                    cabecera += "<tr><td class='cell03'>Material</td>";
                                    cabecera += "<td class='cell05'>" + matnr + " " + con.columnaMatnr(matnr, "MAKTG", "X") + "</td></tr>";
                                }

                                cabecera += "<tr><td class='cell03'>Porcentaje</td>";
                                cabecera += "<td class='cell05'>" + header.GetString("PORCENTAJE") + " %" + "</td></tr>";
                                cabecera += "</table>";

                                lblFolio.InnerHtml = cabecera;


                                string tipo = header.GetString("TIPO");
                                string obj = "";
                                string titulo = "";
                                if (tipo.Substring(1, 1).Equals("M"))
                                {
                                    obj = "MATNR";
                                    titulo = "Material";
                                }
                                else if (tipo.Substring(1, 1).Equals("G"))
                                {
                                    obj = "MATKL";
                                    titulo = "Grupo de artículos";
                                }
                                else if (tipo.Substring(1, 1).Equals("P"))
                                {
                                    obj = "EBELN";
                                    titulo = "Pedido de cliente";
                                }
                                else if (tipo.Substring(1, 1).Equals("L"))
                                {
                                    obj = "CHARG";
                                    titulo = "Lote";
                                }

                                for (int i = 0; i < detail.Count; i++)
                                {
                                    detail.CurrentIndex = i;
                                    SolicitudesL s = new SolicitudesL();
                                    s.obj = detail.GetString(obj);
                                    //s.desc = detail.GetString("VTWEG");
                                    s.desc2 = detail.GetString("MEINS");//ADD RSG 02/06/2017
                                    s.importe = detail.GetString("PR_ANT");
                                    s.moneda = detail.GetString("MON_ANT");
                                    s.importe_n = detail.GetString("PR_NVO");
                                    s.moneda_n = detail.GetString("MON_NVO");
                                    s.fecha_a = f.fecha(detail.GetString("FECHA_INI"));
                                    s.fecha_b = f.fecha(detail.GetString("FECHA_FIN"));
                                    s.comentario = detail.GetString("COMMENTS");
                                    s.escala = detail.GetString("ESCALA");
                                    s.pos = detail.GetString("POS"); ;


                                    //if (tipo.Substring(1, 1).Equals("M"))
                                    //{
                                    //    s.desc = con.columnaMatnr(s.obj, "MAKTG");
                                    //    s.desc2 = con.columnaMatnr(s.obj, "MEINS");
                                    //}
                                    //else if (tipo.Substring(1, 1).Equals("G"))
                                    //{
                                    //    s.desc = con.columnaMatkl(s.obj, "WGBEZ");
                                    //}

                                    ss.Add(s);
                                }

                                ss = obtenerDatos(ss, tipo.Substring(1, 1));
                                string tab = "";

                                tab = "<table border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                                tab += "<tr>";
                                tab += "<td class='cell10'>" + titulo + "</td>";
                                if (!tipo.Substring(1, 1).Equals("P") & !tipo.Substring(1, 1).Equals("L"))
                                    tab += "<td class='cell11'>Denominación</td>";
                                ////if (tipo.Substring(1, 1).Equals("M"))
                                    tab += "<td class='cell12'>Unidad</td>";
                                tab += "<td class='cell12'>Precio Anterior</td>";
                                tab += "<td class='cell12'>Moneda</td>";
                                tab += "<td class='cell12'>Precio nuevo</td>";
                                tab += "<td class='cell12'>Moneda</td>";
                                tab += "<td class='cell12'>Válido de</td>";
                                tab += "<td class='cell12'>Válido a</td>";
                                if (tipo.Substring(1, 1).Equals("L"))
                                    tab += "<td class='cell12'>Comentarios</td>";
                                tab += "<td class='tablahead'>E</td>";
                                tab += "</tr>";
                                foreach (SolicitudesL s in ss)
                                {
                                    string style = "";
                                    if (s.error)
                                        style = "background-color: red !important;";

                                    tab += "<tr style='" + style + "' id='tr-" + s.pos + "' ondblclick='escalas(this.id)'>";

                                    if (s.error)
                                        style = "white-text";
                                    tab += "<td class='tablaCent " + style + "'>" + s.obj + "</td>";
                                    if (!tipo.Substring(1, 1).Equals("P") & !tipo.Substring(1, 1).Equals("L"))
                                        tab += "<td class='tablaIzq " + style + "'>" + s.desc + "</td>";
                                    //if (tipo.Substring(1, 1).Equals("M"))
                                    tab += "<td class='tablaCent " + style + "' id='MEINS-" + s.pos + "'>" + MeinsEsp(s.desc2) + "</td>";
                                    tab += "<td class='tablaCent " + style + "'>" + s.importe + "</td>";
                                    tab += "<td class='tablaCent " + style + "'>" + s.moneda + "</td>";
                                    tab += "<td class='tablaCent " + style + "'>" + s.importe_n + "</td>";
                                    tab += "<td class='tablaCent " + style + "' id='KONWA-" + s.pos + "'>" + s.moneda_n + "</td>";
                                    tab += "<td class='tablaCent " + style + "'>" + s.fecha_a.ToString("dd/MM/yyyy") + "</td>";
                                    tab += "<td class='tablaCent " + style + "'>" + s.fecha_b.ToString("dd/MM/yyyy") + "</td>";
                                    if (tipo.Substring(1, 1).Equals("L"))
                                        tab += "<td class='tablaCent " + style + "'>" + s.comentario + "</td>";
                                    if (s.escala.Equals("X"))
                                        tab += "<td><input type='checkbox' checked disabled id='chk-" + s.pos + "'/></td>";
                                    else
                                        tab += "<td><input type='checkbox' disabled id='chk-" + s.pos + "'/></td>";
                                    tab += "</tr>";
                                }
                                tab += "</tbody></table>";
                                lblTabla.InnerHtml = tab;
                            }
                        }

                        IRfcTable bitacora = con.getBitacora(tipoF, folio);
                        string tabla = "";

                        tabla = "<table border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                        tabla += "<tr><td class='cell12'>Empleado</td><td class='cell12'>Evento</td><td class='cell12'>Fecha autorizar</td><td class='cell12'>Fecha Procesada</td><td class='cell12'>Status</td><td class='cell12'>Comentario</td></tr>";

                        for (int i = 0; i < bitacora.Count; i++)
                        {
                            bitacora.CurrentIndex = i;
                            tabla += "<tr><td class='cell17'>";
                            tabla += bitacora.GetString("ZUSRA") + " - " + bitacora.GetString("ZDUSA");
                            tabla += "</td>";
                            tabla += "<td class='cell18'>";
                            tabla += bitacora.GetString("ZDEEV");
                            tabla += "</td>";
                            tabla += "<td class='cell18'>";
                            tabla += bitacora.GetString("ZFEAL") + "[" + bitacora.GetString("ZHOAL") + "]";
                            tabla += "</td>";
                            tabla += "<td class='cell18'>";
                            tabla += bitacora.GetString("ZFEUM") + "[" + bitacora.GetString("ZHOUM") + "]";
                            tabla += "</td>";
                            tabla += "<td class='cell18'>";
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
                else if (oper.Equals("A"))
                {
                    hidOper.Value = oper;
                    if (con.conectar())
                    {
                        string links = "";
                        links += "<a href='Procesa.aspx";
                        links += "?Tipo=" + tipoF + "&Folio=" + folio + "&Oper=N&Posi=" + posi;
                        links += "'>Cancelar</a>&nbsp;&nbsp;";

                        lblLinks.InnerHtml = links;


                        if (tipoF.Equals("01"))
                        {
                            IRfcTable header = con.consultaCabecera(folio, "");
                            //IRfcTable detail = con.consultaDetalle(folio, "");
                            //List<Solicitudes> ss = new List<Solicitudes>();

                            if (header.Count > 0)
                            {
                                txtFolio.InnerText = "Ha Seleccionado Autorizar Solicitud " + header.GetString("ID_SOLICITUD");
                            }
                        }
                        else if (tipoF.Equals("02"))
                        {

                            IRfcTable header = con.consultaCabeceraL(folio, "");
                            //IRfcTable detail = con.consultaDetalleL(folio, "");
                            //List<SolicitudesL> ss = new List<SolicitudesL>();

                            if (header.Count > 0)
                            {
                                txtFolio.InnerText = "Ha Seleccionado Autorizar Solicitud " + header.GetString("ID_SOLICITUD");
                            }
                        }
                    }
                }
                else if (oper.Equals("R"))
                {
                    hidOper.Value = oper;
                    if (con.conectar())
                    {
                        string links = "";
                        links += "<a href='Procesa.aspx";
                        links += "?Tipo=" + tipoF + "&Folio=" + folio + "&Oper=N&Posi=" + posi;
                        links += "'>Cancelar</a>&nbsp;&nbsp;";

                        lblLinks.InnerHtml = links;


                        if (tipoF.Equals("01"))
                        {
                            IRfcTable header = con.consultaCabecera(folio, "");
                            //IRfcTable detail = con.consultaDetalle(folio, "");
                            //List<Solicitudes> ss = new List<Solicitudes>();

                            if (header.Count > 0)
                            {
                                txtFolio.InnerText = "Ha Seleccionado Rechazar Solicitud " + header.GetString("ID_SOLICITUD");
                            }
                        }
                        else if (tipoF.Equals("02"))
                        {

                            IRfcTable header = con.consultaCabeceraL(folio, "");
                            //IRfcTable detail = con.consultaDetalleL(folio, "");
                            //List<SolicitudesL> ss = new List<SolicitudesL>();

                            if (header.Count > 0)
                            {
                                txtFolio.InnerText = "Ha Seleccionado Rechazar Solicitud " + header.GetString("ID_SOLICITUD");
                            }
                        }
                    }
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
                        //s.desc2 = m.meins;
                    }
                }
                ss.Add(s);
            }

            return ss;
        }
        private string MeinsEsp(string meins)
        {
            string meins_nvo = meins;
            List<meins> mm = meinss;
            foreach (meins m in mm)
            {
                if (m.MSEHI.Equals(meins))
                    meins_nvo = m.MSEH3;
            }
            return meins_nvo;
        }
    }
}