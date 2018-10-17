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
    public partial class DetalleSolicitud : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hidUsuario.Value = Session["Usuario"].ToString();
                hidNumEmp.Value = Session["NumEmp"].ToString();

                string folio = Request.QueryString["Folio"];

                Conexion con = new Conexion();
                catalogos c = new catalogos();
                IRfcTable header = con.consultaCabecera(folio, hidNumEmp.Value);
                IRfcTable detail = con.consultaDetalle(folio, hidNumEmp.Value);
                List<Solicitudes> ss = new List<Solicitudes>();

                if (header.Count > 0)
                {
                    txtFolio.Value = header.GetString("ID_SOLICITUD");
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
                    tab += "<tr><td class='tablahead'>Org. Compras</td><td class='tablahead'>Canal Dist.</td><td class='tablahead'>Sector</td><td class='tablahead'>Cliente</td><td class='tablahead'>Nombre</td><td class='tablahead'>Lista anterior</td><td class='tablahead'>Nueva Lista de precios</td><td class='tablahead'>Vigencia</td></tr>";
                    foreach (Solicitudes s in ss)
                    {
                        string style = "";
                        if (s.error)
                            style = "background-color: red !important;";

                        tab += "<tr style='" + style + "'>";

                        if (s.error)
                            style = "white-text";
                        tab += "<td class='tablaCent " + style + "'>" + s.vkorg + "</td>";
                        tab += "<td class='tablaCent " + style + "'>" + s.vtweg + "</td>";
                        tab += "<td class='tablaCent " + style + "'>" + s.spart + "</td>";
                        tab += "<td class='tablaCent " + style + "'>" + s.kunnr + "</td>";
                        tab += "<td class='tablaIzq " + style + "'>" + s.name1 + "</td>";
                        tab += "<td class='tablaCent " + style + "'>" + s.pltyp_desc + "</td>";
                        tab += "<td class='tablaCent " + style + "'>" + s.pltyp_n_desc + "</td>";
                        tab += "<td class='tablaCent " + style + "'>" + s.date.ToString("dd/MM/yyyy") + "</td>";


                        tab += "</tr>";
                    }
                    tab += "</tbody></table>";
                    lblTabla.InnerHtml = tab;

                    IRfcTable bitacora = con.getBitacora("01", folio);
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

    }
}