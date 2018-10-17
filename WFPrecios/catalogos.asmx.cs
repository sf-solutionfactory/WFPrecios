using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using WFPrecios.Models;

namespace WFPrecios
{
    /// <summary>
    /// Descripción breve de catalogos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [ScriptService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class catalogos : System.Web.Services.WebService
    {

        #region CLIENTE
        [WebMethod]
        public string auto_clientes(string vk, string vt, string sp, string ku, string em, string tp) //Autocompleta de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            string t = "";
            //if (!tp.Trim().Equals("A"))
            if (tp.Trim().Equals("V") | tp.Trim().Equals("C")) //ADD RSG 23.06.2017
                    t = em;
            IRfcTable tabla = con.AutoKUNNR(vk, vt, sp, ku.ToUpper(), t);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("KUNNR");
                lista[i] += " - " + tabla.GetString("NAME1");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }
        [WebMethod]
        public string auto_clientesB(string vk, string vt, string sp, string ku) //Autocompleta de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.AutoKUNNR(vk, vt, sp, ku.ToUpper(), "");
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("KUNNR");
                lista[i] += " - " + tabla.GetString("NAME1");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }
        [WebMethod]
        public string auto_pernr(string pr) //Autocompleta de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.AutoPERNR(pr);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("ZUEMP");
                lista[i] += " - " + tabla.GetString("SNAME");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string auto_lote(string ch, string ma) //Autocompleta de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.AutoCHARG(ma, ch);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("CHARG");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string existe_lote(string ch, string ma) //Autocompleta de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            string tabla = con.CHARG(ma, ch);

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return tabla;
        }
        [WebMethod]
        public string getColumnaCliente(string vk, string vt, string sp, string kunnr, string column) //Obtener columna de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.getCliente(vk, vt, sp, kunnr.ToUpper());
            string[] lista = new string[tabla.Count];

            if (tabla.Count > 0)
            {
                for (int i = 0; i < 1 & i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;
                    lista[i] = tabla.GetString(column);
                }
                if (lista[0] != null)
                {
                    return lista[0];
                }
            }
            else
            {
                return "";
            }
            return "";
        }
        [WebMethod]
        public string getEmailCliente(string vk, string vt, string sp, string kunnr) //Obtiene cadena mail y teléfono
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            return con.clienteEmail(vk, vt, sp, kunnr.ToUpper());

        }
        [WebMethod]
        public string getLPCliente(string vk, string vt, string sp, string kunnr) //Obtiene la lista de precio del cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.getCliente(vk, vt, sp, kunnr.ToUpper());
            string pltyp = "";
            for (int i = 0; i < tabla.Count & i < 1; i++)
            {
                pltyp = tabla.GetString("PLTYP");
            }
            if (!pltyp.Equals(""))
            {
                return con.getLP_Desc("ES", pltyp);
            }
            else
            {
                return "";
            }
        }

        [WebMethod]
        public string getLPSCliente(string sp, string kunnr, string em) //Obtiene la lista de precio del cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.ListaPLTYP(em, sp, kunnr);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("PLTYP") + " " + tabla.GetString("PTEXT");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }
        #endregion CLIENTE

        #region LISTAS DE PRECIOS
        //[WebMethod]
        //public string getDescLP(string lp) //Obtiene descripción de la lista de precio
        //{
        //    Conexion con = new Conexion("E-DESARROLL2", "Initial02");
        //    return con.getLP_Desc("ES", lp);

        //}
        [WebMethod]
        public string getDescLP(string lp, string vk, string vt, string sp, string ku) //Obtiene descripción de la lista de precio
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");

            IRfcTable tabla = con.getCliente(vk, vt, sp, ku.ToUpper());
            string[] cl = new string[tabla.Count];
            string desc = "";
            if (tabla.Count > 0)
            {
                for (int i = 0; i < 1 & i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;
                    cl[i] = tabla.GetString("NAME1");
                }
                if (cl[0] != null)
                {
                    desc = cl[0];
                }
            }
            else
            {
                desc = "";
            }
            if (!desc.Equals(""))
                return con.getLP_Desc("ES", lp);
            else
                return "";

        }
        #endregion LISTAS DE PRECIOS


        [WebMethod]
        public string auto_materiales(string ma, string sp) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.AutoMATNR(ma.ToUpper(), sp);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("MATNR");
                lista[i] += " " + tabla.GetString("MAKTG");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string llena_matnr(string ma, string co) //Obtener columna de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            string cad = con.columnaMatnr(ma, co, "");

            if (cad != null)
            {
                return cad;
            }
            else
            {
                return "";
            }
        }


        [WebMethod]
        public string auto_matkl(string mk, string sp) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.AutoMATKL(mk.ToUpper(), sp);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("MATKL");
                lista[i] += " " + tabla.GetString("WGBEZ");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string llena_matkl(string ma, string co) //Obtener columna de cliente
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            string cad = con.columnaMatkl(ma, co);

            if (cad != null)
            {
                return cad;
            }
            else
            {
                return "";
            }
        }

        [WebMethod]
        public string auto_pltyp(string pl, string sp, string pr) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.AutoPLTYP(pl.ToUpper(), sp, pr);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("PLTYP");
                lista[i] += " " + tabla.GetString("PTEXT");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }
        [WebMethod]
        public string auto_pltypB(string pl) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.AutoPLTYP(pl.ToUpper(), "", "");
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("PLTYP");
                lista[i] += " " + tabla.GetString("PTEXT");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }
        [WebMethod]
        public string llenaPltyp(string pr, string sp) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.ListaPLTYP(pr, sp, "");
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("PLTYP");
                lista[i] += " " + tabla.GetString("PTEXT");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string llenaVtweg(string pr, string vk) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.ListaVTWEG("", pr, vk);
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("VTWEG");
                lista[i] += " " + tabla.GetString("VTEXT");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string llenaSpart(string pr, string vk, string vt) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.ListaSPART("", pr, vk, vt, "02");
            string[] lista = new string[tabla.Count];
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista[i] = tabla.GetString("SPART");
                lista[i] += " " + tabla.GetString("VTEXT");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string tablaEscalas(string kn) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.tablaEscalas(kn);
            string lista = "";
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                //lista += tabla.GetString("KNUMH") + "|";
                lista += tabla.GetString("KONMS") + "|";//ADD RSG 23.05.2017
                lista += tabla.GetString("KLFN1") + "|";
                lista += tabla.GetString("KSTBM") + "|";
                lista += tabla.GetString("KBETR") + "|";
            }

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            //return list;
            return lista;
        }
        [WebMethod]
        public string tablaEscalasSol(string id, string pos) //Autocompleta de material
        {
            Conexion con = new Conexion("E-DESARROLL2", "Initial02");
            IRfcTable tabla = con.tablaEscalasSol(id, pos);
            string lista = "";
            for (int i = 0; i < tabla.Count; i++)
            {
                tabla.CurrentIndex = i;
                lista += tabla.GetString("ID_SOLICITUD") + "|";
                lista += tabla.GetString("KLFN1") + "|";
                lista += tabla.GetString("KSTBM") + "|";
                lista += tabla.GetString("KBETR") + "|";
                lista += tabla.GetString("KONMS") + "|";
            }

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            //return list;
            return lista;
        }
    }
}
