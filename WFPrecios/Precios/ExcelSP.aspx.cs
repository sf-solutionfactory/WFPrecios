using OfficeOpenXml;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using WFPrecios.Models;

namespace WFPrecios.Precios.tabs
{
    public partial class ExcelSP : System.Web.UI.Page
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
                hidTipoEmp.Value = Session["TipoEmp"].ToString();
                btnSubmit.Disabled = true;
                tipo = Request.Form["txtTipo"];
                if (tipo == null)
                    tipo = Request.QueryString["tipo"];
                if (tipo == "")
                    tipo = "1";
                string tab = "";
                txtTipo.Value = tipo;

                tr1.Visible = false;
                tr2.Visible = false;
                tr3.Visible = false;
                tr4.Visible = false;
                tr6.Visible = false;

                if (tipo.Equals("2"))
                {
                    lblLink.InnerHtml = "<a href='../files/LAYOUT%20LOTE.xlsx'>Descargar Layout</a>";
                }else
                {
                    lblLink.InnerHtml = "<a href='../files/LAYOUT%20PEDIDO.xlsx'>Descargar Layout</a>";
                }

                HttpPostedFile file = Request.Files["xlsFile"]; //Trae input FILE
                if (file != null && file.ContentLength > 0)
                {
                    if (con.conectar())
                    {
                        monedas = con.monedas();
                        meinss = con.meinss();
                        DataTable tbl = leerExcel(file); //leer Excel 

                        List<CabeceraL> cabeceras = formarCabecera(tbl, tipo);

                        if (cabeceras.Count > 0)
                        {
                            CabeceraL c = cabeceras[0];
                            IRfcTable kunn;
                            string desc = "";
                            if (tipo.Equals("1"))
                            {
                                kunn = con.getClienteP(c.vkorg, c.vtweg, c.spart, c.kunnr, hidNumEmp.Value, hidTipoEmp.Value);
                                if (kunn.Count == 0)
                                    c.kunnr = "";
                            }
                            else
                            {
                                desc = con.columnaMatnr(c.matnr, "MAKTG", "X");
                                if (desc.Equals(""))
                                    c.kunnr = "";
                            }

                            if (!c.kunnr.Equals(""))
                            {
                                //List<SolicitudesL> solicitud = formarSolicitud(tbl, tipo);
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
                                        string style = "";
                                        pos++;
                                        //-------------EXISTE OBJETO
                                        if (s.tipo_error == "1")
                                            style = "red white-text";
                                        tab += "<tr id='tr-" + pos + "'><td class='tablaCent " + style + "' id='OBJ-" + pos + "' ondblclick='escalas(this.id)'>";
                                        tab += "<input type='hidden' id='MATNR-" + pos + "' value='" + s.obj + "' />";

                                        tab += s.obj + "</td>";
                                        //tab += "<td class='tablaCent " + style + "'>" + s.desc + "</td>";

                                        style = "";

                                        //-------------SI ES MATERIAL
                                        if (!existeMeins(s.desc2))
                                            style = "red white-text";
                                        //if (tipo.Equals("1") | tipo.Equals("2") | tipo.Equals("4"))
                                        tab += "<td class='tablaCent " + style + "' id='DESC2-" + pos + "' ondblclick='escalas(this.id)'>";
                                        tab += "<input type='hidden'  id='MEINS-" + pos + "' value='" + s.desc2 + "' />";
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
                                        if (s.tipo_error.Equals("2") | s.tipo_error.Equals("4"))
                                            style = "red white-text";
                                        tab += "<td class='tablaCent " + style + "'>" + s.fecha_a.ToString("dd/MM/yyyy") + "</td>";
                                        style = "";
                                        if (s.tipo_error.Equals("3") | s.tipo_error.Equals("4"))
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
                                            tabla += s.fecha_a.ToString("dd/MM/yyyy") + "|" + s.fecha_b.ToString("dd/MM/yyyy") + "||X||";
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
                                                tabla += s.kbetr + "|" + MeinsEsp(s.um1) + "|";
                                            }
                                        }
                                        txtEscalas.Value = tabla;
                                        lblEscala.InnerHtml = "<script>generaEscalas();</script>";

                                    }

                                    if (tipo.Equals("1"))
                                    {
                                        tr1.Visible = true;
                                        tr2.Visible = true;
                                        tr3.Visible = true;
                                        tr4.Visible = true;
                                    }
                                    else
                                    {
                                        tr3.Visible = true;
                                        tr6.Visible = true;
                                    }

                                }
                            }
                            else
                            {
                                if (tipo.Equals("1"))
                                    tab = "<div class='center red-text'>No existe el cliente o no le pertenece</div>";
                                else
                                    tab = "<div class='center red-text'>No existe el material</div>";
                            }
                        }
                        else
                        {
                            if (tipo.Equals("1"))
                                tab = "<div class='center red-text'>Debe coincidir Org de compras, canal de distribución, sector y cliente en cada posición.</div>";
                            else
                                tab = "<div class='center red-text'>Debe coincidir Sector y material en cada posición</div>";
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
            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                DataRow row = tbl.NewRow();
                foreach (var cell in wsRow)
                    row[cell.Start.Column - 1] = cell.Text;
                tbl.Rows.Add(row);
            }
            return tbl;
        }

        private List<SolicitudesL> formarSolicitud(DataTable tbl, string t)
        {
            List<SolicitudesL> ss = new List<SolicitudesL>();

            int columnas = 0;
            if (t.Equals("2"))
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
                        s.obj = row[columns[i + (columnas - 11)]].ToString();
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

                    //if (s.desc.Equals(""))
                    //{
                    //    s.error = true;
                    //    s.tipo_error = "1";
                    //}
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
                }
            }
            return ss;
        }
        private List<CabeceraL> formarCabecera(DataTable tbl, string t)
        {
            List<CabeceraL> ss = new List<CabeceraL>();

            int columnas = 0;
            if (t.Equals("2"))
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
                            s.matnr = ".";
                        }
                        else if (t.Equals("2"))
                        {
                            s.vkorg = ".";
                            s.vtweg = ".";
                            s.spart = row[columns[i]].ToString();
                            s.kunnr = ".";
                            s.pltyp = ".";
                            s.matnr = row[columns[i + 1]].ToString();
                        }

                        if (!s.vkorg.Trim().Equals("") & !s.vtweg.Trim().Equals("") & !s.spart.Trim().Equals("") & !s.kunnr.Trim().Equals("") & !s.pltyp.Trim().Equals(""))
                        {
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
                    s.Add(ss[0]);
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

                    if (c.vkorg.Equals(cc[i].vkorg) & c.vtweg.Equals(cc[i].vtweg) & c.spart.Equals(cc[i].spart) & c.kunnr.Equals(cc[i].kunnr) & c.pltyp.Equals(cc[i].pltyp) & c.matnr.Equals(cc[i].matnr))
                        ca.Add(cc[i]);

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
                txtMATNR.Value = ca[0].matnr;
                if (tipo.Equals("1"))
                {
                    txtVKORG1.Value = ca[0].vkorg + " " + con.ListaVKORG(ca[0].vkorg, "").GetString("VTEXT");
                    txtVTWEG1.Value = ca[0].vtweg + " " + con.ListaVTWEG(ca[0].vtweg, "", "").GetString("VTEXT");
                    txtSPART1.Value = ca[0].spart + " " + con.ListaSPART(ca[0].spart, "", "", "", "").GetString("VTEXT");
                    txtKUNNR1.Value = ca[0].kunnr + " " + con.getCliente(ca[0].vkorg, ca[0].vtweg, ca[0].spart, ca[0].kunnr).GetString("NAME1");
                }
                txtPLTYP1.Value = ca[0].pltyp;
                if (tipo.Equals("2"))
                {
                    txtSPART1.Value = ca[0].spart + " " + con.ListaSPART(ca[0].spart, "", "", "", "").GetString("VTEXT");
                    txtMATNR1.Value = ca[0].matnr + " " + con.columnaMatnr(ca[0].matnr, "MAKTG", "");
                }
            }

            return ca;
        }

        private string obtenerEncabezado(string tipo)
        {
            string header = "";
            switch (tipo)
            {
                case "1":
                    header = "<tr><td class='tablahead'>Pedido</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                case "2":
                    header = "<tr><td class='tablahead'>Lote</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    break;
                default:
                    header = "<tr><td class='tablahead'>Pedido</td><td class='tablahead'>Unidad</td><td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'></td></tr>";
                    tipo = "1";
                    break;
            }

            return header;
        }

        private bool esCantidad(string cantidad)
        {
            return true;
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