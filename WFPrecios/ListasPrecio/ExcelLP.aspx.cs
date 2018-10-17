using OfficeOpenXml;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using WFPrecios.Models;

namespace WFPrecios.ListasPrecio
{
    public partial class ExcelLP : System.Web.UI.Page
    {
        catalogos c = new catalogos();
        Fechas f = new Fechas();
        Conexion con = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hidUsuario.Value = Session["Usuario"].ToString();
                hidNumEmp.Value = Session["NumEmp"].ToString();
                hidTipoEmp.Value = Session["TipoEmp"].ToString();
                btnSubmit.Disabled = true;
                txtTabla.Value = "";

                HttpPostedFile file = Request.Files["xlsFile"]; //Trae input FILE
                string tab = "";
                //modal1.Visible = true;
                if (file != null && file.ContentLength > 0)
                {
                    if (con.conectar())
                    {
                        DateTime fecha = f.fecha(con.fechaLimite());
                        DataTable tbl = leerExcel(file); //leer Excel 

                        if (tbl.Columns.Count == 6)
                        {
                            List<Solicitudes> solicitud = formarSolicitud(tbl);

                            tab = "<table id='Table9' style='borde -width: 0px; border-style: None; width: 100 %; border-collapse: collapse;'><tbody><tr class='cell08'><td></td></tr></tbody></table>";
                            tab += "<table border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                            tab += "<tr><td class='tablahead'>Org. Compras</td><td class='tablahead'>Canal Dist.</td><td class='tablahead'>Sector</td><td class='tablahead'>Cliente</td><td class='tablahead'>Nombre</td><td class='tablahead'>Lista anterior</td><td class='tablahead'>Nueva Lista de precios</td><td class='tablahead'>Vigencia</td></tr>";
                            foreach (Solicitudes s in solicitud)
                            {
                                string style = "";
                                if (s.error)
                                    style = "red white-text";

                                tab += "<tr>";

                                //if (s.error)
                                //    style = "white-text";
                                tab += "<td class='tablaCent " + style + "'>" + s.vkorg + "</td>";
                                tab += "<td class='tablaCent " + style + "'>" + s.vtweg + "</td>";
                                tab += "<td class='tablaCent " + style + "'>" + s.spart + "</td>";
                                tab += "<td class='tablaCent " + style + "'>" + s.kunnr + "</td>";
                                tab += "<td class='tablaIzq " + style + "'>" + s.name1 + "</td>";
                                tab += "<td class='tablaCent " + style + "'>" + s.pltyp_desc + "</td>";
                                style = "";
                                if (s.pltyp_n_desc.Trim().Equals(""))
                                {
                                    style = "red white-text";
                                    s.error = true;
                                }
                                tab += "<td class='tablaCent " + style + "'>" + s.pltyp_n_desc + "</td>";
                                style = "";
                                if (s.date > fecha)
                                {
                                    s.error = true;
                                    style = "red white-text";
                                }
                                tab += "<td class='tablaCent " + style + "'>" + s.date.ToString("dd/MM/yyyy") + "</td>";



                                tab += "</tr>";
                            }
                            tab += "</tbody></table>";

                            List<Solicitudes> sol_final = quitarRepetidos(solicitud);

                            if (sol_final.Count > 0)
                            {
                                btnSubmit.Disabled = false;
                                txtTabla.Value = "";
                                foreach (Solicitudes s in sol_final)
                                {
                                    txtTabla.Value += s.vkorg + "|" + s.vtweg + "|" + s.spart + "|" +
                                                     s.kunnr + "|" + s.pltyp + "|" + s.pltyp_n + "|" + f.fechaToOUT(s.date) + "|";
                                }
                            }
                        }
                        else
                        {
                            tab = "<div class='center red-text'>Seleccione un archivo válido.</div>";
                        }
                    }
                    else
                    {
                        tab = "<div class='center red-text'>No hay conexión a SAP.</div>";
                    }
                }
                lblTabla.InnerHtml = tab;
            }
            //modal1.Visible = false;
            catch (Exception ex)
            {
                Response.Redirect("https://www.terzaonline.com/nworkflow/login/");
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

        private List<Solicitudes> formarSolicitud(DataTable tbl)
        {
            List<Solicitudes> ss = new List<Solicitudes>();

            foreach (DataRow row in tbl.Rows)
            {
                for (int i = 0; i < tbl.Columns.Count; i += 6)
                {
                    DataColumnCollection columns = tbl.Columns;
                    Solicitudes s = new Solicitudes();
                    s.vkorg = row[columns[i]].ToString();
                    s.vtweg = row[columns[i + 1]].ToString();
                    s.spart = row[columns[i + 2]].ToString();
                    s.kunnr = row[columns[i + 3]].ToString();
                    s.pltyp_n = row[columns[i + 4]].ToString();
                    string fecha_temp = row[columns[i + 5]].ToString();

                    if (!s.vkorg.Trim().Equals("") & !s.vtweg.Trim().Equals("") & !s.spart.Trim().Equals("")
                        & !s.kunnr.Trim().Equals("") & !s.pltyp_n.Trim().Equals("") & !s.vtweg.Trim().Equals("") & !fecha_temp.Trim().Equals(""))
                    {
                        if (fecha_temp.Length.Equals(10))
                            s.date = f.fechaD(fecha_temp);
                        else
                            s.date = DateTime.MaxValue;
                        ss.Add(s);
                    }
                }
            }

            List<Solicitudes> sol_final = quitarRepetidos(ss);
            ss = sol_final;

            ss = llenaDatosCliente(ss, hidNumEmp.Value, hidTipoEmp.Value);

            IRfcTable p1 = con.ListaPLTYP();
            foreach (Solicitudes s in ss)
            {
                IRfcTable p2 = con.ListaPLTYP(hidNumEmp.Value, s.spart, s.kunnr); //ADD RSG 14.08.2017
                //IRfcTable p2 = con.ListaPLTYP(hidNumEmp.Value, s.spart); //ADD RSG 14.08.2017
                //s.name1 = c.getColumnaCliente(s.vkorg, s.vtweg, s.spart, s.kunnr, "NAME1");
                if (s.name1.Equals(""))
                    s.error = true;
                //else
                //    s.pltyp = c.getColumnaCliente(s.vkorg, s.vtweg, s.spart, s.kunnr, "PLTYP");
                if (!s.pltyp.Trim().Equals(""))
                    s.pltyp_desc = getDescLP(s.pltyp, p1);
                else
                    s.pltyp_desc = "Sin lista de precios";
                s.pltyp_n_desc = getDescLP(s.pltyp_n, p2);
            }

            return ss;
        }

        private List<Solicitudes> quitarRepetidos(List<Solicitudes> ss)
        {
            List<Solicitudes> s = new List<Solicitudes>();

            if (ss.Count > 0)
            {
                if (s.Count == 0)
                {
                    if (!ss[0].error)
                        s.Add(ss[0]);
                }
                for (int i = 0; i < ss.Count; i++)
                {
                    bool ban = false;
                    for (int j = 0; j < s.Count; j++)
                    {
                        if (s[j].vkorg.Equals(ss[i].vkorg) & s[j].vtweg.Equals(ss[i].vtweg) & s[j].spart.Equals(ss[i].spart) & s[j].kunnr.Equals(ss[i].kunnr))
                        {
                            ban = true;
                        }
                    }
                    if (!ban)
                    {
                        if (!ss[i].error)
                            s.Add(ss[i]);
                    }
                }
            }

            return s;
        }
        private List<Solicitudes> llenaDatosCliente(List<Solicitudes> ss, string em, string tp)
        {
            List<Solicitudes> s = new List<Solicitudes>();
            s = con.llenaCliente(ss, em, tp);
            return s;
        }

        private string getDescLP(string pltyp, IRfcTable p)
        {
            for (int i = 0; i < p.Count; i++)
            {
                p.CurrentIndex = i;
                if (p.GetString("PLTYP").Equals(pltyp))
                {
                    return p.GetString("PLTYP") + " " + p.GetString("PTEXT");
                }
            }
            return "";
        }

    }
}