using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using WFPrecios.Models;
using SAP.Middleware.Connector;

namespace WFPrecios.Precios.tabs
{
    public partial class ExcelTab : System.Web.UI.Page
    {
        catalogos c = new catalogos();
        Fechas f = new Fechas();
        Conexion con = new Conexion();
        public string tipo = "";
        List<string> monedas;
        List<meins> meinss;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hidUsuario.Value = Session["Usuario"].ToString();
                hidNumEmp.Value = Session["NumEmp"].ToString();
                btnSubmit.Disabled = true;
                tipo = Request.Form["txtTipo"];
                if (tipo == null)
                    tipo = Request.QueryString["tipo"];
                string tab = "";
                switch (tipo)
                {
                    case "1":
                        lblTipo.InnerText = "Sector/Cliente/Material";
                        lblLink.InnerHtml = "<a href='../../files/LAYOUT%20CLIENTE-MATERIAL.xlsx'>Descargar Layout</a>";
                        break;
                    case "2":
                        lblTipo.InnerText = "Sector/Material";
                        lblLink.InnerHtml = "<a href='../../files/LAYOUT%20SECTOR-MATERIAL.xlsx'>Descargar Layout</a>";
                        break;
                    case "3":
                        lblTipo.InnerText = "Sector/Cliente/Grupo de artículos";
                        lblLink.InnerHtml = "<a href='../../files/LAYOUT%20CLIENTE-GPO%20ARTICULOS.xlsx'>Descargar Layout</a>";
                        break;
                    case "4":
                        lblTipo.InnerText = "Sector/Lista de precios/Material";
                        lblLink.InnerHtml = "<a href='../../files/LAYOUT%20LP-MATERIAL.xlsx'>Descargar Layout</a>";
                        break;
                    case "5":
                        lblTipo.InnerText = "Sector/Lista de precios/Grupo de artículos";
                        lblLink.InnerHtml = "<a href='../../files/LAYOUT%20LP-GPO%20ARTICULOS.xlsx'>Descargar Layout</a>";
                        break;
                    case "6":
                        lblTipo.InnerText = "Sector/Grupo de artículos";
                        lblLink.InnerHtml = "<a href='../../files/LAYOUT%20SECTOR-GPO%20ARTICULOS.xlsx'>Descargar Layout</a>";
                        break;
                    default:
                        lblTipo.InnerText = "Sector/Cliente/Material";
                        lblLink.InnerHtml = "<a href='../../files/LAYOUT%20CLIENTE-MATERIAL.xlsx'>Descargar Layout</a>";
                        tipo = "1";
                        break;
                }

                tr1.Attributes.Add("style", "visibility:hidden; display: none;");
                tr2.Attributes.Add("style", "visibility:hidden; display: none;");
                tr3.Attributes.Add("style", "visibility:hidden; display: none;");
                tr4.Attributes.Add("style", "visibility:hidden; display: none;");
                tr5.Attributes.Add("style", "visibility:hidden; display: none;");

                HttpPostedFile file = Request.Files["xlsFile"]; //Trae input FILE
                if (file != null && file.ContentLength > 0)
                {
                    if (con.conectar())
                    {
                        monedas = con.monedas();
                        meinss = con.meinss();
                        DataTable tbl = leerExcel(file); //leer Excel 
                        DateTime fecha = f.fecha(con.fechaLimite());

                        List<CabeceraL> cabeceras = formarCabecera(tbl, tipo);

                        if (cabeceras.Count > 0)
                        {
                            List<SolicitudesL> solicitudEsc = formarSolicitud(tbl, tipo);
                            List<SolicitudesL> solicitud = formarSolEsc(solicitudEsc);
                            solicitud = completa(solicitud, solicitudEsc);

                            if (solicitud.Count > 0)
                            {
                                tab = "<table id='Table9' style='borde -width: 0px; border-style: None; width: 100 %; border-collapse: collapse;'><tbody><tr class='cell08'><td></td></tr></tbody></table>";
                                tab += "<table border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                                tab += obtenerEncabezado(tipo);

                                int pos = 0;
                                foreach (SolicitudesL s in solicitud)
                                {
                                    //pos++;
                                    string style = "";
                                    //-------------EXISTE OBJETO
                                    if (s.tipo_error == "1" | s.desc.Trim().Equals(""))
                                        style = "red white-text";
                                    pos++;
                                    tab += "<tr id='tr-" + pos + "'><td class='tablaCent " + style + "' id='OBJ-" + pos + "' ondblclick='escalas(this.id)'>";
                                    tab += "<input type='hidden' id='MATNR-" + pos + "' value='" + s.obj + "' />";

                                    tab += s.obj + "</td>";
                                    tab += "<td class='tablaCent " + style + "'>" + s.desc + "</td>";

                                    //-------------SI ES MATERIAL
                                    if (!existeMeins(s.desc2))
                                        style = "red white-text";
                                    //if (tipo.Equals("1") | tipo.Equals("2") | tipo.Equals("4"))
                                    tab += "<td class='tablaCent " + style + "' id='DESC2-" + pos + "' ondblclick='escalas(this.id)'>";
                                    tab += "<input type='hidden'  id='MEINS-" + pos + "' value='" +s.desc2 + "' />";
                                    tab += MeinsEsp(s.desc2) + "</td>";
                                    //ADD RSG 27.11.2017
                                    tab += "<input type='hidden'  id='MEINSE-" + pos + "' value='" + MeinsEsp(s.desc2) + "' />";

                                    style = "";

                                    //-------------CANTIDAD
                                    if (!esCantidad(s.importe_n))
                                        style = "red white-text";
                                    tab += "<td class='tablaCent " + style + "'>" + s.importe_n + "</td>";

                                    style = "";

                                    //-------------MONEDA
                                    if (!existeMoneda(s.moneda_n))
                                        style = "red white-text";
                                    tab += "<td class='tablaCent " + style + "'>";
                                    tab += "<input type='hidden'  id='KONWA-" + pos + "' value='" + s.moneda_n + "' />";
                                    tab += s.moneda_n + "</td>";

                                    style = "";

                                    //-------------FECHA
                                    if (s.tipo_error.Equals("2") | s.tipo_error.Equals("4") | s.fecha_a > fecha)
                                        style = "red white-text";
                                    tab += "<td class='tablaCent " + style + "'>" + s.fecha_a.ToString("dd/MM/yyyy") + "</td>";
                                    style = "";
                                    if (s.tipo_error.Equals("3") | s.tipo_error.Equals("4") | s.fecha_b > fecha)
                                        style = "red white-text";
                                    tab += "<td class='tablaCent " + style + "'>" + s.fecha_b.ToString("dd/MM/yyyy") + "</td>";

                                    tab += "<td class='tablaCent " + style + "'>";
                                    tab += "<input id='chk-" + pos + "' type='checkbox' disabled='disabled' ";
                                    if (s.escala.Equals("X"))
                                        tab += "checked='checked'";
                                    tab += "/></td>";

                                    tab += "</tr>";
                                }
                                tab += "</tbody></table>";

                                List<SolicitudesL> sol_final = quitarErroneos(solicitud);

                                if (sol_final.Count > 0)
                                {
                                    btnSubmit.Disabled = false;
                                    txtTabla.Value = "";
                                    string tabla = "";
                                    foreach (SolicitudesL s in sol_final)
                                    {
                                        //txtTabla.Value += s.vkorg + "|" + s.vtweg + "|" + s.spart + "|" +
                                        //                 s.kunnr + "|" + s.pltyp + "|" + s.pltyp_n + "|" + f.fechaToOUT(s.date) + "|";}
                                        tabla += s.obj + "|0.00||" + s.importe_n + "|" + s.moneda_n + "|";
                                        tabla += s.fecha_a.ToString("dd/MM/yyyy") + "|" + s.fecha_b.ToString("dd/MM/yyyy") + "||X|";
                                        tabla += s.desc2 + "|";
                                    }

                                    txtTabla.Value = tabla;

                                    tabla = "";
                                    int pos2 = 0;
                                    string obj = "";
                                    foreach (SolicitudesL s in solicitudEsc)
                                    {
                                        if (s.escala.Equals("X"))
                                        {
                                            if (s.obj.Equals(obj))
                                                pos2++;
                                            else
                                            {
                                                pos2 = 1;
                                                obj = s.obj;
                                            }
                                            tabla += s.obj + "|" + s.pos + "|" + pos2 + "|" + s.menge + "|";
                                            //tabla += s.kbetr + "|" + s.um1 + "|";
                                            //ADD RSG 27.11.2017
                                            //tabla += s.kbetr + "|" + MeinsEsp(s.um1) + "|";
                                            tabla += s.kbetr + "|" + s.um1 + "|";//ADD RSG 12.10.2018 No existe CJ en PRD, solo CS
                                        }
                                    }

                                    txtEscalas.Value = tabla;
                                    lblEscala.InnerHtml = "<script>generaEscalas();</script>";

                                    if (tipo.Equals("1") | tipo.Equals("3"))
                                    {
                                        tr1.Attributes.Add("style", "visibility:visible");
                                        tr2.Attributes.Add("style", "visibility:visible");
                                        tr3.Attributes.Add("style", "visibility:visible");
                                        tr4.Attributes.Add("style", "visibility:visible");
                                    }
                                    else if (tipo.Equals("4") | tipo.Equals("5"))
                                    {
                                        tr3.Attributes.Add("style", "visibility:visible");
                                        tr5.Attributes.Add("style", "visibility:visible");
                                    }
                                    else
                                    {
                                        tr3.Attributes.Add("style", "visibility:visible");
                                    }

                                }
                            }
                        }
                        else
                        {
                            tab = "<div class='center red-text'>Debe coincidir Org de compras, canal de distribución, sector y cliente en cada posición.</div>";

                            switch (tipo)
                            {
                                case "1":
                                    //lblTipo.InnerText = "Sector/Cliente/Material";
                                    tab = "<div class='center red-text'>Debe coincidir Org de compras, canal de distribución, sector y cliente en cada posición.</div>";
                                    break;
                                case "2":
                                    //lblTipo.InnerText = "Sector/Material";
                                    tab = "<div class='center red-text'>Debe coincidir el sector en cada posición.</div>";
                                    break;
                                case "3":
                                    //lblTipo.InnerText = "Sector/Cliente/Grupo de artículos";
                                    tab = "<div class='center red-text'>Debe coincidir Org de compras, canal de distribución, sector y cliente en cada posición.</div>";
                                    break;
                                case "4":
                                    //lblTipo.InnerText = "Sector/Lista de precios/Material";
                                    tab = "<div class='center red-text'>Debe coincidir el sector y lista de precio en cada posición.</div>";
                                    break;
                                case "5":
                                    //lblTipo.InnerText = "Sector/Lista de precios/Grupo de artículos";
                                    tab = "<div class='center red-text'>Debe coincidir el sector y lista de precio en cada posición.</div>";
                                    break;
                                case "6":
                                    lblTipo.InnerText = "Sector/Grupo de artículos";
                                    tab = "<div class='center red-text'>Debe coincidir el sector en cada posición.</div>";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        tab = "<div class='center red-text'>No hay conexión a SAP</div>";
                    }
                }

                lblTabla.InnerHtml = tab;
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

        private DataTable leerExcel(HttpPostedFile file)
        {
            string fname = Path.GetFileName(file.FileName);

            ExcelPackage excel = new ExcelPackage(file.InputStream);

            DataTable tbl = new DataTable();
            var ws = excel.Workbook.Worksheets[1];
            bool hasHeader = true;

            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                tbl.Columns.Add(hasHeader ? firstRowCell.Text
                    : String.Format("Column {0}", firstRowCell.Start.Column));

            int startRow = hasHeader ? 2 : 1;
            int i = 0;
            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                try
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.NewRow();
                    foreach (var cell in wsRow)
                    {
                        i++;
                        try
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                        catch (Exception ex)
                        {
                            i = i;
                        }
                    }

                tbl.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    i = i;
                }
            }
            return tbl;
        }

        private List<SolicitudesL> formarSolicitud(DataTable tbl, string t)
        {
            List<SolicitudesL> ss = new List<SolicitudesL>();

            int columnas = 0;
            if (t.Equals("2") | t.Equals("6"))
            {
                columnas = 6;
                columnas = 12;//ADD RSG 15.05.2017
            }
            else if (t.Equals("4") | t.Equals("5"))
            {
                columnas = 7;
                columnas = 13;//ADD RSG 15.05.2017
            }
            else
            {
                columnas = 9;
                columnas = 15;//ADD RSG 15.05.2017
            }

            if (tbl.Columns.Count == columnas)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    for (int i = 0; i < tbl.Columns.Count; i += columnas)
                    {
                        DataColumnCollection columns = tbl.Columns;

                        SolicitudesL s = new SolicitudesL();
                        //s.obj = row[columns[i + (columnas - 5)]].ToString();
                        //s.importe_n = row[columns[i + (columnas - 4)]].ToString();
                        //s.moneda_n = row[columns[i + (columnas - 3)]].ToString().ToUpper();
                        //string fecha_A = row[columns[i + (columnas - 2)]].ToString();
                        //string fecha_B = row[columns[i + (columnas - 1)]].ToString();
                        s.obj = row[columns[i + (columnas - 11)]].ToString();//BEGIN RSG 15.05.2017
                        s.desc2 = MeinsValor(row[columns[i + (columnas - 10)]].ToString().ToUpper());
                        s.importe_n = row[columns[i + (columnas - 9)]].ToString();
                        s.moneda_n = row[columns[i + (columnas - 8)]].ToString().ToUpper();
                        string fecha_A = row[columns[i + (columnas - 7)]].ToString();
                        string fecha_B = row[columns[i + (columnas - 6)]].ToString();
                        s.escala = row[columns[i + (columnas - 5)]].ToString();
                        s.menge = row[columns[i + (columnas - 4)]].ToString();
                        s.um1 = MeinsValor(row[columns[i + (columnas - 3)]].ToString().ToUpper());
                        s.kbetr = row[columns[i + (columnas - 2)]].ToString();
                        s.um2 = MeinsValor(row[columns[i + (columnas - 1)]].ToString().ToUpper());
                        if (s.escala.Equals("X"))
                        {
                            s.desc2 = s.um2;
                            s.importe_n = s.kbetr;
                        }//END RSG 15.05.2017

                        if (!s.obj.Trim().Equals("") & !fecha_A.Trim().Equals("") & !fecha_B.Trim().Equals(""))
                        {
                            s.fecha_a = f.fechaD(fecha_A);
                            s.fecha_b = f.fechaD(fecha_B);
                            ss.Add(s);
                        }
                    }
                }

                List<SolicitudesL> sol_final = quitarRepetidos(ss);
                ss = sol_final;

                foreach (SolicitudesL s in ss)
                {
                    if (t.Equals("1") | t.Equals("2") | t.Equals("4"))
                    {
                        s.desc = con.columnaMatnr(s.obj, "MAKTG", "");
                        //s.desc2 = con.columnaMatnr(s.obj, "MEINS");//DELETE RSG 15.05.2017
                    }
                    else
                    {
                        if (s.obj.Trim().Length <= 9)
                            s.desc = con.columnaMatkl(s.obj, "WGBEZ");
                        else
                            s.desc = "";
                    }

                    if (s.desc.Equals(""))
                    {
                        s.error = true;
                        s.tipo_error = "1";
                    }
                    if (s.fecha_a > s.fecha_b)
                    {
                        s.error = true;
                        s.tipo_error = "4";
                    }

                    //VERIFICAR SI ES CANTIDAD
                    if (!esCantidad(s.importe_n))
                    {
                        s.error = true;
                    }

                    //VERIFICAR SI LA MONEDA EXISTE
                    if (!existeMoneda(s.moneda_n))
                    {
                        s.error = true;
                    }
                    //VERIFICAR SI LA MONEDA EXISTE
                    if (!existeMeins(s.desc2))
                    {
                        s.error = true;
                    }
                }

            }
            return ss;
        }
        private List<CabeceraL> formarCabecera(DataTable tbl, string t)
        {
            List<CabeceraL> ss = new List<CabeceraL>();

            int columnas = 0;
            if (t.Equals("2") | t.Equals("6"))
            {
                columnas = 6;
                columnas = 12;//ADD RSG 15.05.2017
            }
            else if (t.Equals("4") | t.Equals("5"))
            {
                columnas = 7;
                columnas = 13;//ADD RSG 15.05.2017
            }
            else
            {
                columnas = 9;
                columnas = 15;//ADD RSG 15.05.2017
            }


            if (tbl.Columns.Count == columnas)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    for (int i = 0; i < tbl.Columns.Count; i += columnas)
                    {
                        DataColumnCollection columns = tbl.Columns;

                        CabeceraL s = new CabeceraL();
                        if (t.Equals("1") | t.Equals("3"))
                        {
                            s.vkorg = row[columns[i]].ToString();
                            s.vtweg = row[columns[i + 1]].ToString();
                            s.spart = row[columns[i + 2]].ToString();
                            s.kunnr = row[columns[i + 3]].ToString();
                            s.pltyp = ".";
                        }
                        else if (t.Equals("2") | t.Equals("6"))
                        {
                            s.vkorg = ".";
                            s.vtweg = ".";
                            s.spart = row[columns[i]].ToString();
                            s.kunnr = ".";
                            s.pltyp = ".";
                        }
                        else if (t.Equals("4") | t.Equals("5"))
                        {
                            s.vkorg = ".";
                            s.vtweg = ".";
                            s.spart = row[columns[i]].ToString();
                            s.kunnr = ".";
                            s.pltyp = row[columns[i + 1]].ToString();
                        }

                        if (!s.vkorg.Trim().Equals("") & !s.vtweg.Trim().Equals("") & !s.spart.Trim().Equals("") & !s.kunnr.Trim().Equals("") & !s.pltyp.Trim().Equals(""))
                        {
                            if (t.Equals("1") | t.Equals("3"))
                            {
                                s.pltyp = "";
                            }
                            else if (t.Equals("2") | t.Equals("6"))
                            {
                                s.vkorg = "";
                                s.vtweg = "";
                                s.kunnr = "";
                                s.pltyp = "";
                            }
                            else if (t.Equals("4") | t.Equals("5"))
                            {
                                s.vkorg = "";
                                s.vtweg = "";
                                s.spart = row[columns[i]].ToString();
                                s.kunnr = "";
                                s.pltyp = row[columns[i + 1]].ToString();
                            }
                            ss.Add(s);
                        }
                    }
                }
                List<CabeceraL> sol_final = compararCab(ss);
                ss = sol_final;
            }
            return ss;
        }

        private List<SolicitudesL> quitarRepetidos(List<SolicitudesL> ss)
        {
            List<SolicitudesL> s = new List<SolicitudesL>();
            DateTime fecha = f.fecha(con.fechaLimite());
            if (ss.Count > 0)
            {
                if (s.Count == 0)
                {
                    //if (!ss[0].error)
                    //s.Add(ss[0]);
                }
                for (int i = 0; i < ss.Count; i++)
                {
                    bool ban = false;
                    for (int j = 0; j < s.Count; j++)
                    {
                        if (s[j].obj.Equals(ss[i].obj))
                        {
                            if (!s[j].escala.Equals("X"))
                                ban = true;
                        }
                    }

                    if (ss[i].fecha_a > fecha)
                    {
                        ss[i].error = true;
                        ss[i].tipo_error = "2";
                    }
                    if (ss[i].fecha_b > fecha)
                    {
                        ss[i].error = true;
                        ss[i].tipo_error = "3";
                    }
                    if (!ban)
                    {
                        //if (!ss[i].error)
                        s.Add(ss[i]);
                    }
                }
            }

            return s;
        }
        private List<SolicitudesL> quitarErroneos(List<SolicitudesL> ss)
        {
            List<SolicitudesL> sf = new List<SolicitudesL>();
            foreach (SolicitudesL s in ss)
            {
                if (!s.error)
                    sf.Add(s);
            }
            return sf;
        }

        private List<CabeceraL> compararCab(List<CabeceraL> cc)
        {
            List<CabeceraL> ca = new List<CabeceraL>();

            if (cc.Count > 0)
            {
                CabeceraL c = cc[0];
                for (int i = 0; i < cc.Count; i++)
                {
                    if (c.vkorg.Equals(cc[i].vkorg) & c.vtweg.Equals(cc[i].vtweg) & c.spart.Equals(cc[i].spart) & c.kunnr.Equals(cc[i].kunnr) & c.pltyp.Equals(cc[i].pltyp))
                    {
                        ca.Add(cc[i]);
                    }
                }
            }

            if (ca.Count != cc.Count)
            {
                ca = new List<CabeceraL>();
            }
            else
            {
                txtVKORG.Value = ca[0].vkorg;
                txtVTWEG.Value = ca[0].vtweg;
                txtSPART.Value = ca[0].spart;
                txtKUNNR.Value = ca[0].kunnr;
                txtPLTYP.Value = ca[0].pltyp;

                if (!ca[0].vkorg.Equals(""))
                {
                    IRfcTable vk = con.ListaVKORG(ca[0].vkorg, hidNumEmp.Value);
                    if (vk.Count > 0)
                        txtVKORG1.Value = ca[0].vkorg + " " + vk.GetString("VTEXT");
                    else
                        return new List<CabeceraL>();
                }
                if (!ca[0].vtweg.Equals(""))
                {
                    IRfcTable vk = con.ListaVTWEG(ca[0].vtweg, hidNumEmp.Value, ca[0].vkorg);
                    if (vk.Count > 0)
                        txtVTWEG1.Value = ca[0].vtweg + " " + vk.GetString("VTEXT");
                    else
                        return new List<CabeceraL>();
                }
                if (!ca[0].spart.Equals(""))
                {
                    IRfcTable vk = con.ListaSPART(ca[0].spart, hidNumEmp.Value, ca[0].vkorg, ca[0].vtweg, "02");
                    if (vk.Count > 0)
                        txtSPART1.Value = ca[0].spart + " " + vk.GetString("VTEXT");
                    else
                        return new List<CabeceraL>();
                }
                if (!ca[0].kunnr.Equals(""))
                {
                    txtKUNNR1.Value = ca[0].kunnr + " " + con.getCliente(ca[0].vkorg, ca[0].vtweg, ca[0].spart, ca[0].kunnr).GetString("NAME1");
                }
                txtPLTYP1.Value = ca[0].pltyp;
            }

            return ca;
        }

        private string obtenerEncabezado(string tipo)
        {
            string header = "";
            switch (tipo)
            {
                case "1":
                    header = "<tr><td class='tablahead'>Material</td><td class='tablahead'>Denominación</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                case "2":
                    header = "<tr><td class='tablahead'>Material</td><td class='tablahead'>Denominación</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                case "3":
                    header = "<tr><td class='tablahead'>Grupo de artículo</td><td class='tablahead'>Denominación</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                case "4":
                    header = "<tr><td class='tablahead'>Material</td><td class='tablahead'>Denominación</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                case "5":
                    header = "<tr><td class='tablahead'>Grupo de artículo</td><td class='tablahead'>Denominación</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                case "6":
                    header = "<tr><td class='tablahead'>Grupo de artículo</td><td class='tablahead'>Denominación</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                default:
                    header = "<tr><td class='tablahead'>Material</td><td class='tablahead'>Denominación</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    tipo = "1";
                    break;
            }

            return header;
        }

        private bool esCantidad(string cantidad)
        {
            if (cantidad.Trim().Equals(""))
            {
                return true;
            }
            else
            {
                decimal n;
                return decimal.TryParse(cantidad, out n);
            }
        }

        private bool existeMoneda(string moneda)
        {
            List<string> m = monedas;
            return m.Contains(moneda);
        }
        private bool existeMeins(string meins)
        {
            List<meins> mm = meinss;
            bool ban = false;
            foreach (meins m in mm)
            {
                if (m.MSEHI.Equals(meins))
                    ban = true;
                if (m.MSEH3.Equals(meins))
                {
                    ban = true;
                }
            }
                return ban;
        }
        private string MeinsValor(string meins)
        {
            string meins_nvo = meins;
            List<meins> mm = meinss;
            foreach (meins m in mm)
            {
                if (m.MSEHI.Equals(meins))
                    meins_nvo = m.MSEHI;
                if (m.MSEH3.Equals(meins))
                    meins_nvo = m.MSEHI;
            }
            return meins_nvo;
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

        private List<SolicitudesL> formarSolEsc(List<SolicitudesL> ss)
        {
            List<SolicitudesL> sol = new List<SolicitudesL>();
            int pos = 0;
            foreach (SolicitudesL s in ss)
            {
                bool ban = false;
                for (int i = 0; i < sol.Count; i++)
                {
                    if (sol[i].obj.Equals(s.obj))
                    {
                        ban = true;
                    }
                }
                if (!ban)
                {
                    pos++;
                    s.pos = pos + "";
                    sol.Add(s);
                }
            }
            return sol;
        }
        private List<SolicitudesL> completa(List<SolicitudesL> sol, List<SolicitudesL> esc)
        {
            foreach (SolicitudesL s in esc)
            {
                for (int i = 0; i < sol.Count; i++)
                {
                    if (sol[i].obj.Equals(s.obj))
                    {
                        s.pos = sol[i].pos;
                    }
                }
            }
            return sol;
        }
    }
}