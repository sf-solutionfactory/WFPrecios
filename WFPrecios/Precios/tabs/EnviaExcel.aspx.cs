using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPrecios.Models;

namespace WFPrecios.Precios.tabs
{
    public partial class EnviaExcel : System.Web.UI.Page
    {
        Fechas f = new Fechas();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string[] tabla = Request.Form["txtTabla"].Split('|');
            //string tipo = Request.Form["txtTipo"];
            //List<SolicitudesL> solicitud = new List<SolicitudesL>();
            string[] tabla = Request.Form["txtTabla"].Split('|');
            string[] escalas = Request.Form["txtEscalas"].Split('|');
            string tipo = Request.Form["txtTipo"];
            List<SolicitudesL> solicitud = new List<SolicitudesL>();
            List<Escala> escala = new List<Escala>();

            int longitud = 0;
            //if (tipo.Equals("1") | tipo.Equals("2"))
            //{
            longitud = 9;
            longitud = 10; //ADD RSG 23.05.2017
            //}

            for (int i = 0; i < tabla.Length - 1; i += longitud)
            {
                SolicitudesL s = new SolicitudesL();
                s.obj = tabla[i];
                s.importe = tabla[i + 1];
                s.moneda = tabla[i + 2];
                s.importe_n = tabla[i + 3];
                s.moneda_n = tabla[i + 4];
                s.fecha_a = f.fechaD(tabla[i + 5]);
                s.fecha_b = f.fechaD(tabla[i + 6]);
                s.id = tabla[i + 7];
                s.modif = tabla[i + 8];
                s.desc2 = tabla[i + 9];

                solicitud.Add(s);
            }
            for (int i = 0; i < escalas.Length - 1; i += 6)
            {
                Escala s = new Escala();

                s.obj = escalas[i];
                s.id = escalas[i + 1];
                s.pos = escalas[i + 2];
                s.cantidad = escalas[i + 3];
                s.importe = escalas[i + 4];
                s.meins = escalas[i + 5];

                //ADD RSG 10.08.2018 ----------------------------------------------
                foreach(SolicitudesL ss in solicitud)
                {
                    if(ss.obj == s.obj)
                    {
                        escala.Add(s);
                        break;
                    }
                }
                //ADD RSG 10.08.2018----------------------------------------------

                //escala.Add(s); //DELETE RSG 10.08.2018----------------------------------------------
            }
            Conexion con = new Conexion();
            CabeceraL c = new CabeceraL();
            c.folio = "";
            switch (tipo)
            {
                case "1":
                    c.tipo = "CM";
                    break;
                case "2":
                    c.tipo = "SM";
                    break;
                case "3":
                    c.tipo = "CG";
                    break;
                case "4":
                    c.tipo = "LM";
                    break;
                case "5":
                    c.tipo = "LG";
                    break;
                case "6":
                    c.tipo = "SG";
                    break;
            }

            c.fecha = f.fechaToSAP(DateTime.Now);
            c.hora = f.hora(DateTime.Now);
            c.usuario = Request.Form["hidUsuario"];
            c.pernr = Request.Form["hidNumEmp"];
            c.estatus = "P";
            c.visto = " ";
            string comentarios = Request.Form["txtCOMM"];
            c.comentario = comentarios;

            string p_inc = Request.Form["P_inc"];
            string p_dec = "";
            if (p_inc.Equals(""))
                p_dec = "-" + Request.Form["P_dec"];

            string cambio = p_inc + p_dec;
            c.porcentaje = cambio;

            c.spart = Request.Form["txtSPART"];

            if (tipo.Equals("1") | tipo.Equals("3"))
            {
                c.vkorg = Request.Form["txtVKORG"];
                c.vtweg = Request.Form["txtVTWEG"];
                c.kunnr = Request.Form["txtKUNNR"];
            }
            else if (tipo.Equals("4") | tipo.Equals("5"))
            {
                c.vkorg = Request.Form["txtVKORG"];
                c.vtweg = Request.Form["txtVTWEG"];
                c.pltyp = Request.Form["txtPLTYP"].Split(' ')[0];
            }

            List<DetalleL> ds = new List<DetalleL>();
            for (int i = 0; i < solicitud.Count; i++)
            {
                DetalleL d = new DetalleL();
                d.folio = "";
                d.pos = i + 1;
                d.estatus = " ";
                if (tipo.Equals("1") | tipo.Equals("2") | tipo.Equals("4"))
                {
                    d.matnr = solicitud[i].obj;
                }
                else
                {
                    d.matkl = solicitud[i].obj;
                }
                d.pr_ant = solicitud[i].importe;
                d.mon_ant = solicitud[i].moneda;
                d.pr_nvo = solicitud[i].importe_n;
                d.mon_nvo = solicitud[i].moneda_n;
                d.porcentaje = Request.Form["P_inc"];
                if (d.porcentaje == null)
                {
                    d.porcentaje = "-" + Request.Form["P_dec"];
                }
                d.date = f.fechaToSAP(solicitud[i].fecha_a);
                d.dateA = f.fechaToSAP(solicitud[i].fecha_b);
                d.knumh = solicitud[i].id;
                d.meins = solicitud[i].desc2.ToUpper(); //ADD RSG 23.05.2017
                ds.Add(d);

                //ADD RSG 10.08.2018 ----------------------------------------------
                foreach (Escala ss in escala)
                {
                    if (tipo.Equals("1") | tipo.Equals("2") | tipo.Equals("4"))
                    {
                        if (ss.obj == d.matnr)
                        {
                            ss.id = d.pos + "";
                        }
                    }else
                    {
                        if (ss.obj == d.matkl)
                        {
                            ss.id = d.pos + "";
                        }
                    }
                }
                //ADD RSG 10.08.2018----------------------------------------------
            }


            //string folio = con.InsertaSolicitudL(c, ds);
            //lblFolio.InnerHtml = "<p class=''>" + folio + "</p>";
            string folio = con.InsertaSolicitudL(c, ds, escala);
            if (!folio.Equals(""))
                folio = "La solicitud de modificación de Listas de precio ha sido recibida, y será procesada con el folio<br />" + folio;
            else
                folio = "Hubo un error en la creación de la Solicitud.";
            lblFolio.InnerHtml = "<p class=''>" + folio + "</p>";
        }
    }
}