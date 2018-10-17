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
    public partial class DetalleSolicitud : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        public string tipo;
        List<meins> meinss;
        protected void Page_Load(object sender, EventArgs e)
        {
            string folio = Request.QueryString["Folio"];

            try
            {
                hidUsuario.Value = Session["Usuario"].ToString();
                hidNumEmp.Value = Session["NumEmp"].ToString();
                Conexion con = new Conexion();
                catalogos c = new catalogos();
                IRfcTable header = con.consultaCabeceraL(folio, hidNumEmp.Value);
                IRfcTable detail = con.consultaDetalleL(folio, hidNumEmp.Value);
                meinss = con.meinss();
                List<SolicitudesL> ss = new List<SolicitudesL>();

                if (header.Count > 0)
                {
                    header.CurrentIndex = 0;
                    txtFolio.Value = header.GetString("ID_SOLICITUD");
                    txtCOMM.InnerText = header.GetString("COMMENTS");
                    tipo = header.GetString("TIPO");
                    string vkorg = header.GetString("VKORG");
                    string vtweg = header.GetString("VTWEG");
                    string spart = header.GetString("SPART");
                    string kunnr = header.GetString("KUNNR");
                    string pltyp = header.GetString("PLTYP");
                    string matnr = header.GetString("MATNR");

                    if (!vkorg.Equals(""))
                        txtVKORG.Value = vkorg + " " + con.ListaVKORG(vkorg, "").GetString("VTEXT");
                    if (!vtweg.Equals(""))
                        txtVTWEG.Value = vtweg + " " + con.ListaVTWEG(vtweg, "", "").GetString("VTEXT");
                    txtSPART.Value = spart + " " + con.ListaSPART(spart, "", "", "", "").GetString("VTEXT");

                    if (!kunnr.Equals(""))
                    {
                        IRfcTable kunn = con.getCliente(vkorg, vtweg, spart, kunnr);
                        if (kunn.Count > 0)
                            txtKUNNR.Value = kunnr + " " + con.getCliente(vkorg, vtweg, spart, kunnr).GetString("NAME1");
                    }
                    if (!pltyp.Equals(""))
                    {
                        txtPLTYP.Value = con.getLP_Desc("", pltyp);
                    }
                    if (!matnr.Equals(""))
                    {
                        txtMATNR.Value = matnr + " " + con.columnaMatnr(matnr, "MAKTG", "X");
                    }
                    txtPORC.Value = header.GetString("PORCENTAJE") + " %";

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
                        s.desc2 = detail.GetString("MEINS");
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
                    tab += "<td class='tablahead'>" + titulo + "</td>";
                    if (!tipo.Substring(1, 1).Equals("P") & !tipo.Substring(1, 1).Equals("L"))
                        tab += "<td class='tablahead'>Denominación</td>";
                    //if (tipo.Substring(1, 1).Equals("M"))
                        tab += "<td class='tablahead'>Unidad</td>";
                    tab += "<td class='tablahead'>Precio Anterior</td>";
                    tab += "<td class='tablahead'>Moneda</td>";
                    tab += "<td class='tablahead'>Precio nuevo</td>";
                    tab += "<td class='tablahead'>Moneda</td>";
                    tab += "<td class='tablahead'>Válido de</td>";
                    tab += "<td class='tablahead'>Válido a</td>";
                    if (tipo.Substring(1, 1).Equals("L"))
                        tab += "<td class='tablahead'>Comentario</td>";
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

                    IRfcTable bitacora = con.getBitacora("02", folio);
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
                        tabla += "<td class='cell18' style='max-width:200px;overflow-y:hidden;'>";
                        tabla += bitacora.GetString("ZCOME");
                        //tabla += "sad ads fas a s fasifu sa disad iasduasdiasud a sdaiud asi duiasdhasoidha soih sad sahdjashdiusahd sa ";
                        tabla += "</td>";
                        tabla += "</tr>";

                    }

                    lblBItacora.InnerHtml = tabla;

                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("https://www.terzaonline.com/nworkflow/login/");
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