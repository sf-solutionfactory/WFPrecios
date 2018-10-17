using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WFPrecios.Models
{
    public class Conexion
    {
        RfcDestination oDestino;

        //Keys en Web.config-------------------------------------------------------------
        private string sapName = ConfigurationManager.AppSettings["sapName"];
        private string sapUser = ConfigurationManager.AppSettings["sapUser"];
        private string sapPass = ConfigurationManager.AppSettings["sapPass"];
        ////private string sapUser = "";
        ////private string sapPass = "";
        private string sapClient = ConfigurationManager.AppSettings["MANDT"];
        private string sapServer = ConfigurationManager.AppSettings["sapServer"];
        private string sapNumber = ConfigurationManager.AppSettings["sapNumber"];
        private string sapID = ConfigurationManager.AppSettings["sapID"];
        private string sapRouter = ConfigurationManager.AppSettings["sapRouter"];
        //-------------------------------------------------------------------------------

        public Conexion(string User, string Pass)
        {
            //sapUser = User;
            //sapPass = Pass;
        }
        public Conexion()
        {
        }

        #region conexionSAP
        public bool conectar()
        {
            RfcConfigParameters oParametros = new RfcConfigParameters();
            oParametros.Add(RfcConfigParameters.Name, sapName);
            oParametros.Add(RfcConfigParameters.User, sapUser);
            oParametros.Add(RfcConfigParameters.Password, sapPass);
            oParametros.Add(RfcConfigParameters.Client, sapClient);
            //oParametros.Add(RfcConfigParameters.Language, "EN");
            oParametros.Add(RfcConfigParameters.Language, "ES");
            oParametros.Add(RfcConfigParameters.AppServerHost, sapServer);
            oParametros.Add(RfcConfigParameters.SystemNumber, sapNumber);
            if (!sapRouter.Equals("") && !sapRouter.Equals(null))
            {
                oParametros.Add(RfcConfigParameters.SAPRouter, sapRouter);
            }
            if (!sapID.Equals("") && !sapID.Equals(null))
            {
                oParametros.Add(RfcConfigParameters.SystemID, sapID);
            }
            oParametros.Add(RfcConfigParameters.PoolSize, "5");

            oDestino = RfcDestinationManager.GetDestination(oParametros);

            try
            {
                oDestino.Ping();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;

        }
        #endregion

        #region SOLICITUD

        public string InsertaSolicitud(Cabecera c, List<Detalle> ds)
        {
            string folio = "";
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_008");//Módulo de función

                IRfcStructure header = bapi.GetStructure("HEADER");
                header.SetValue("ID_SOLICITUD", c.folio);
                //header.SetValue("TIPO", c.tipo);
                header.SetValue("FECHA", c.fecha);
                header.SetValue("HORA", c.hora);
                header.SetValue("USUARIO", c.usuario);
                header.SetValue("PERNR", c.pernr);
                header.SetValue("ESTATUS", c.estatus);
                //header.SetValue("VISTO", c.visto);
                header.SetValue("COMMENTS", c.comentario);

                IRfcTable detail = bapi.GetTable("IT_DETALLE");
                foreach (Detalle d in ds)
                {
                    detail.Append();
                    detail.SetValue("ID_SOLICITUD", d.folio);
                    detail.SetValue("POS", d.pos);
                    detail.SetValue("VKORG", d.vkorg);
                    detail.SetValue("VTWEG", d.vtweg);
                    detail.SetValue("SPART", d.spart);
                    detail.SetValue("KUNNR", d.kunnr);
                    detail.SetValue("LP_ANT", d.pltyp);
                    detail.SetValue("LP_NVO", d.pltyp_n);
                    //detail.SetValue("MATNR", d.matnr);
                    //detail.SetValue("MATKL", d.matkl);
                    //detail.SetValue("EBELN", d.ebeln);
                    //detail.SetValue("PR_ANT", d.pr_ant);
                    //detail.SetValue("PR_NVO", d.pr_nvo);
                    //detail.SetValue("PORCENTAJE", d.porcentaje);
                    detail.SetValue("ESTATUS", d.estatus);
                    detail.SetValue("FECHA", d.date);
                }

                bapi.Invoke(oDestino);

                ////folio = bapi.GetString("FOLIO");
                IRfcTable folios = bapi.GetTable("IT_FOLIO");
                for (int i = 0; i < folios.Count; i++)
                {
                    folios.CurrentIndex = i;
                    folio += folios.GetString("ID_SOLICITUD") + ", ";
                }
            }
            return folio;
        }
        public IRfcTable tablaCabecera(string tipo, string visto, string pernr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_009");//Módulo de función

                bapi.SetValue("TIPO", tipo);
                //bapi.SetValue("VISTO", visto);
                bapi.SetValue("PERNR", pernr);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_CABECERA");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable consultaCabecera(string folio, string pernr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_010");//Módulo de función

                bapi.SetValue("FOLIO", folio);
                bapi.SetValue("PERNR", pernr);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_CABECERA");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable consultaDetalle(string folio, string pernr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_010");//Módulo de función

                bapi.SetValue("FOLIO", folio);
                bapi.SetValue("PERNR", pernr);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_DETALLE");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        #endregion SOLICITUD

        #region SOLICITUDL

        public string InsertaSolicitudL(CabeceraL c, List<DetalleL> ds, List<Escala> es)
        {
            string folio = "";
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_017");//Módulo de función

                IRfcStructure header = bapi.GetStructure("HEADER");
                header.SetValue("ID_SOLICITUD", c.folio);
                header.SetValue("TIPO", c.tipo);
                header.SetValue("FECHA", c.fecha);
                header.SetValue("HORA", c.hora);
                header.SetValue("USUARIO", c.usuario);
                header.SetValue("PERNR", c.pernr);
                header.SetValue("ESTATUS", c.estatus);
                //header.SetValue("VISTO", c.visto);
                header.SetValue("COMMENTS", c.comentario);
                header.SetValue("VKORG", c.vkorg);
                header.SetValue("VTWEG", c.vtweg);
                header.SetValue("SPART", c.spart);
                header.SetValue("KUNNR", c.kunnr);
                header.SetValue("PLTYP", c.pltyp);
                header.SetValue("PORCENTAJE", c.porcentaje);
                header.SetValue("MATNR", c.matnr);

                IRfcTable detail = bapi.GetTable("IT_DETALLE");

                foreach (DetalleL d in ds)
                {
                    d.pr_nvo = d.pr_nvo.Replace('$', ' ');
                    d.pr_nvo = d.pr_nvo.Trim();
                    d.pr_ant = d.pr_ant.Replace('$', ' ');
                    d.pr_ant = d.pr_ant.Trim();
                }
                foreach (DetalleL d in ds)
                {
                    detail.Append();
                    detail.SetValue("ID_SOLICITUD", d.folio);
                    detail.SetValue("POS", d.pos);
                    detail.SetValue("ESTATUS", d.estatus);
                    detail.SetValue("FECHA_INI", d.date);
                    detail.SetValue("FECHA_FIN", d.dateA);
                    detail.SetValue("MATNR", d.matnr);
                    detail.SetValue("MATKL", d.matkl);
                    detail.SetValue("EBELN", d.ebeln);
                    detail.SetValue("CHARG", d.lote);
                    detail.SetValue("PR_ANT", d.pr_ant);
                    detail.SetValue("MON_ANT", d.mon_ant);
                    detail.SetValue("PR_NVO", d.pr_nvo);
                    detail.SetValue("MON_NVO", d.mon_nvo);
                    //detail.SetValue("PORCENTAJE", d.porcentaje);
                    detail.SetValue("PORCENTAJE", "0.00");
                    detail.SetValue("KNUMH", d.knumh);
                    detail.SetValue("COMMENTS", d.comentario);
                    detail.SetValue("MEINS", d.meins);
                }

                IRfcTable escalas = bapi.GetTable("IT_ESCALAS");
                foreach (Escala e in es)
                {
                    escalas.Append();
                    escalas.SetValue("OBJECT", e.obj);
                    escalas.SetValue("POS", e.id);
                    escalas.SetValue("KLFN1", e.pos);
                    escalas.SetValue("KSTBM", e.cantidad);
                    escalas.SetValue("KBETR", e.importe);
                    escalas.SetValue("KONMS", e.meins);
                }

                bapi.Invoke(oDestino);

                folio = bapi.GetString("FOLIO");
            }
            return folio;
        }

        public IRfcTable tablaCabeceraL(string visto, string pernr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_018");//Módulo de función

                bapi.SetValue("VISTO", visto);
                //bapi.SetValue("USUARIO", usuario);
                bapi.SetValue("PERNR", pernr);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_CABECERA");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable consultaCabeceraL(string folio, string pernr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_019");//Módulo de función

                bapi.SetValue("FOLIO", folio);
                bapi.SetValue("PERNR", pernr);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_CABECERA");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable consultaDetalleL(string folio, string pernr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_019");//Módulo de función

                bapi.SetValue("FOLIO", folio);
                bapi.SetValue("PERNR", pernr);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_DETALLE");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable tablaEscalas(string knumh)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_043");//Módulo de función

                bapi.SetValue("KNUMH", knumh);
                //bapi.SetValue("USUARIO", usuario);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("KONM");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable tablaEscalasSol(string folio, string pos)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_044");//Módulo de función

                bapi.SetValue("FOLIO", folio);
                bapi.SetValue("POS", pos);
                //bapi.SetValue("USUARIO", usuario);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_ESCALAS");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        #endregion SOLICITUDL

        #region COMBOBOX
        public IRfcTable ListaVKORG(string v, string p) //Trae TODAS las Organizaciones de compra
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_001");//Módulo de función

                if (!v.Equals(""))
                    bapi.SetValue("VKORG", v);
                if (!p.Equals(""))
                    bapi.SetValue("PERNR", p);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_TVKO");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable ListaVTWEG(string v, string p, string k) //Trae TODOS los canales de distribución
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_002");//Módulo de función

                if (!v.Equals(""))
                    bapi.SetValue("VTWEG", v);
                if (!p.Equals(""))
                    bapi.SetValue("PERNR", p);
                if (!k.Equals(""))
                    bapi.SetValue("VKORG", k);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_TVTW");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable AutoKUNNR(string vk, string vt, string sp, string ku, string em) //Trae la lista de clientes para autocompletar
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_003");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("KUNNR", ku);
                bapi.SetValue("PERNR", em);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_KNAVV");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable ListaSPART(string s, string p, string k, string v, string t) //Trae TODOS los sectores
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_004");//Módulo de función

                if (!s.Equals(""))
                    bapi.SetValue("SPART", s);
                if (!p.Equals(""))
                    bapi.SetValue("PERNR", p);
                if (!k.Equals(""))
                    bapi.SetValue("VKORG", k);
                if (!v.Equals(""))
                    bapi.SetValue("VTWEG", v);
                if (!t.Equals(""))
                    bapi.SetValue("TIPO", t);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_TSPA");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable AutoMATNR(string ma, string sp) //Trae la lista de materiales para autocompletar
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_013");//Módulo de función

                bapi.SetValue("MATNR", ma);
                bapi.SetValue("SPART", sp);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_MAKT");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable AutoMATKL(string mk, string sp) //Trae la lista de clientes para autocompletar
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_014");//Módulo de función

                bapi.SetValue("MATKL", mk);
                if (!sp.Equals(""))
                    bapi.SetValue("SPART", sp);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_T023T");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable AutoPLTYP(string pl, string sp, string pr) //Trae la lista de clientes para autocompletar
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_016");//Módulo de función

                bapi.SetValue("PLTYP", pl);
                bapi.SetValue("PERNR", pr);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("ZTIWF", "005");
                bapi.SetValue("TIPO", "02");
                bapi.SetValue("CONDICION", "X");

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_T189T");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable AutoPERNR(string pr) //Trae la lista de clientes para autocompletar
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_029");//Módulo de función

                bapi.SetValue("ZUSRS", pr);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("it_009");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable AutoCHARG(string ma, string ch) //Trae la lista de clientes para autocompletar
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_046");//Módulo de función

                bapi.SetValue("MATNR", ma);
                bapi.SetValue("CHARG", ch);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("it_MCHB");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public string CHARG(string ma, string ch) //Trae la lista de clientes para autocompletar
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_047");//Módulo de función

                bapi.SetValue("MATNR", ma);
                bapi.SetValue("CHARG", ch);

                bapi.Invoke(oDestino);

                string tabla = bapi.GetString("CHARG_E");

                return tabla;
            }
            else
            {
                return null;
            }
        }
        #endregion COMBOBOX

        #region Datos Cliente
        public IRfcTable getCliente(string vk, string vt, string sp, string ku) //Trae el cliente consultado
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_005");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("KUNNR", ku);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_KNAVV");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable getClienteP(string vk, string vt, string sp, string ku, string em, string tp) //Trae el cliente consultado
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_005");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("KUNNR", ku);
                //if (!tp.Equals("A"))
                if (tp.Trim().Equals("V") | tp.Trim().Equals("C")) //ADD RSG 23.06.2017
                    bapi.SetValue("PERNR", em);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_KNVP");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public string clienteEmail(string vk, string vt, string sp, string ku) //Trae cadena de mail y teléfono
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_006");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("KUNNR", ku);

                bapi.Invoke(oDestino);

                string cadena = "";
                IRfcTable tabla = bapi.GetTable("IT_KNAVV");
                IRfcTable email = bapi.GetTable("IT_ADR6");
                for (int i = 0; i < 1 & i < email.Count; i++)
                {
                    email.CurrentIndex = i;
                    cadena = email.GetString("SMTP_ADDR");// + " /* " + tabla.GetString("TELF1");

                }
                if (!cadena.Equals(""))
                    cadena += " /* ";

                for (int i = 0; i < 1 & i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;
                    cadena += tabla.GetString("TELF1");

                }

                return cadena;
            }
            else
            {
                return null;
            }
        }
        public List<Solicitudes> llenaCliente(List<Solicitudes> sol, string em, string tp) //Trae el cliente consultado
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_028");//Módulo de función

                IRfcTable knvp = bapi.GetTable("IT_KNVP");

                for (int i = 0; i < sol.Count; i++)
                {
                    knvp.Append();
                    knvp.SetValue("KUNNR", sol[i].kunnr);
                    knvp.SetValue("VKORG", sol[i].vkorg);
                    knvp.SetValue("VTWEG", sol[i].vtweg);
                    knvp.SetValue("SPART", sol[i].spart);
                    knvp.SetValue("PERNR", em);

                }
                bapi.SetValue("TIPO", tp);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_KNAVV");
                List<Solicitudes> ss = new List<Solicitudes>();
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;
                    foreach (Solicitudes s in sol)
                    {
                        string kunnr = "";
                        try
                        {
                            kunnr = int.Parse(s.kunnr) + "";
                            int len = 10 - kunnr.Length;
                            kunnr = "";
                            do
                            {
                                kunnr += "0";
                                len--;
                            } while (len > 0);
                            kunnr += s.kunnr;
                        }
                        catch
                        {
                            kunnr = s.kunnr;
                        }
                        if (kunnr.Equals(tabla.GetString("KUNNR")) &
                            s.vkorg.Equals(tabla.GetString("VKORG")) &
                            s.vtweg.Equals(tabla.GetString("VTWEG")) &
                            s.spart.Equals(tabla.GetString("SPART")))
                        {
                            s.name1 = tabla.GetString("NAME1");
                            s.pltyp = tabla.GetString("PLTYP");
                        }
                    }
                }

                return sol;
            }
            else
            {
                return null;
            }
        }
        #endregion Datos Cliente

        #region Datos Lista de precios
        public string getLP_Desc(string idioma, string lista) //Trae la descripción lista de precio consultada
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_007");//Módulo de función

                //bapi.SetValue("SPRAS", ");
                bapi.SetValue("PLTYP", lista);

                bapi.Invoke(oDestino);

                //IRfcTable tabla = bapi.GetTable("IT_KNVP");
                string cadena = "";
                IRfcTable tabla = bapi.GetTable("IT_T189T");
                for (int i = 0; i < tabla.Count & i < 1; i++)
                {
                    tabla.CurrentIndex = i;
                    cadena = tabla.GetString("PLTYP") + " " + tabla.GetString("PTEXT");
                }

                return cadena;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable ListaPLTYP(string pr, string sp, string ku) //Trae TODAS las listas de precios
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_007");//Módulo de función

                //bapi.SetValue("SPRAS", ");
                bapi.SetValue("PERNR", pr);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("ZTIWF", "005");
                bapi.SetValue("TIPO", "01");
                if(!ku.Trim().Equals(""))
                    bapi.SetValue("KUNNR", ku);

                bapi.Invoke(oDestino);

                //IRfcTable tabla = bapi.GetTable("IT_KNVP");
                //string cadena = "";
                IRfcTable tabla = bapi.GetTable("IT_T189T");

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable ListaPLTYP() //Trae TODAS las listas de precios
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_007");//Módulo de función

                //bapi.SetValue("SPRAS", ");
                //bapi.SetValue("PERNR", pr);
                //bapi.SetValue("SPART", sp);
                //bapi.SetValue("ZTIWF", "005");

                bapi.Invoke(oDestino);

                //IRfcTable tabla = bapi.GetTable("IT_KNVP");
                string cadena = "";
                IRfcTable tabla = bapi.GetTable("IT_T189T");

                return tabla;
            }
            else
            {
                return null;
            }
        }
        #endregion Datos Lista de precios

        #region Datos Material
        public string columnaMatnr(string matnr, string columna, string lang) //Trae la desc o unidad de material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_012");//Módulo de función

                bapi.SetValue("MATNR", matnr);
                bapi.SetValue("LANGU", lang);

                bapi.Invoke(oDestino);

                string cadena = bapi.GetString(columna);

                return cadena;
            }
            else
            {
                return null;
            }
        }
        public List<material> TcolumnaMatnr(List<string> matnr) //Trae la desc o unidad de material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_022");//Módulo de función

                IRfcTable it_matnr = bapi.GetTable("IT_MATNR");

                foreach (string m in matnr)
                {
                    it_matnr.Append();
                    it_matnr.SetValue("MATNR", m);
                }

                bapi.Invoke(oDestino);

                IRfcTable tab = bapi.GetTable("IT_MARA");
                List<material> mm = new List<material>();

                for (int i = 0; i < tab.Count; i++)
                {
                    tab.CurrentIndex = i;
                    material m = new material();
                    m.matnr = tab.GetString("MATNR");
                    m.maktg = tab.GetString("MAKTG");
                    m.meins = tab.GetString("MEINS");

                    mm.Add(m);
                }

                return mm;
            }
            else
            {
                return null;
            }
        }
        #endregion Datos Material

        #region Cambiar Precios
        public IRfcTable consultaLP(string vk, string vt, string sp, string ku, string ta) //Trae cabecera KONH por lista de precio
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_011");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("KUNNR", ku);
                bapi.SetValue("TABLA", ta);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_KONH");

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable consultaLPDetail(string vk, string vt, string sp, string ku, string ma, string mk, string pl, string ta)//Trae detalle KONH por lista de precio
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_011");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("KUNNR", ku);
                bapi.SetValue("MATNR", ma);
                bapi.SetValue("MATKL", mk);
                bapi.SetValue("PLTYP", pl);
                bapi.SetValue("TABLA", ta);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_LP");

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable consultaLODetail(string sp, string ma, string ta, bool ch)//Trae detalle KONH por lista de precio
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_024");//Módulo de función

                bapi.SetValue("SPART", sp);
                bapi.SetValue("MATNR", ma);
                bapi.SetValue("TABLA", ta);
                if (ch)
                    bapi.SetValue("DEGRADADO", "X");

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_LP");

                return tabla;
            }
            else
            {
                return null;
            }
        }
        #endregion Cambiar Precios

        #region Datos Grupo Art
        public string columnaMatkl(string matkl, string columna) //Trae la desc o unidad de material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_015");//Módulo de función

                bapi.SetValue("MATKL", matkl);

                bapi.Invoke(oDestino);

                string cadena = bapi.GetString(columna);

                return cadena;
            }
            else
            {
                return null;
            }
        }

        public List<material> TcolumnaMatkl(List<string> matkl) //Trae la desc o unidad de material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_023");//Módulo de función

                IRfcTable it_matnr = bapi.GetTable("IT_MATKL");

                foreach (string m in matkl)
                {
                    it_matnr.Append();
                    it_matnr.SetValue("MATKL", m);
                }

                bapi.Invoke(oDestino);

                IRfcTable tab = bapi.GetTable("IT_WGBEZ");
                List<material> mm = new List<material>();

                for (int i = 0; i < tab.Count; i++)
                {
                    tab.CurrentIndex = i;
                    material m = new material();
                    m.matnr = tab.GetString("MATKL");
                    m.maktg = tab.GetString("WGBEZ");

                    mm.Add(m);
                }

                return mm;
            }
            else
            {
                return null;
            }
        }
        #endregion Datos Grupo Art

        #region Fecha y Moneda
        public string fechaLimite() //Trae cadena de mail y teléfono
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_020");//Módulo de función

                bapi.Invoke(oDestino);

                string cadena = bapi.GetString("FECHA_L");

                return cadena;
            }
            else
            {
                return null;
            }
        }


        public List<string> monedas() //Trae cadena de mail y teléfono
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_021");//Módulo de función

                bapi.Invoke(oDestino);

                IRfcTable tcurr = bapi.GetTable("IT_TCURC");
                List<string> m = new List<string>();

                for (int i = 0; i < tcurr.Count; i++)
                {
                    tcurr.CurrentIndex = i;
                    m.Add(tcurr.GetString("WAERS"));
                }

                return m;
            }
            else
            {
                return null;
            }
        }
        public List<meins> meinss() //Trae cadena de mail y teléfono
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_045");//Módulo de función

                bapi.Invoke(oDestino);

                IRfcTable tcurr = bapi.GetTable("IT_T006A");
                List<meins> mm = new List<meins>();

                for (int i = 0; i < tcurr.Count; i++)
                {
                    tcurr.CurrentIndex = i;
                    meins m = new meins();
                    m.MSEHI = tcurr.GetString("MSEHI");
                    //m.MSEHI = tcurr.GetString("MSEH3");
                    m.MSEH3 = tcurr.GetString("MSEH3");
                    m.desc = tcurr.GetString("MSEHL");
                    mm.Add(m);
                }
                //List<string> ms = new List<string>();
                //foreach(meins m in mm)
                //{
                //    ms.Add("<option value='"+m.MSEHI+"'>"+ m.MSEH3+ " - " + "</option>");
                //}

                return mm;
            }
            else
            {
                return null;
            }
        }

        #endregion Fecha y Moneda

        #region HISTORIAL
        public IRfcTable busquedaL(string vk, string vt, string sp, string pr, string p1, string p2, string fs, string fa, string k1, string k2)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_030");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("PERNR", pr);
                bapi.SetValue("KUNNR1", k1);
                bapi.SetValue("KUNNR2", k2);
                bapi.SetValue("FECHA_S", fs);
                bapi.SetValue("FECHA_A", fa);
                bapi.SetValue("PLTYP1", p1);
                bapi.SetValue("PLTYP2", p2);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_DATA");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable busquedaP(string vk, string vt, string sp, string pr, string ku, string inc, string dec, string fs, string fa, string fi, string ff, string mk, string pl, string ma)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_031");//Módulo de función

                bapi.SetValue("VKORG", vk);
                bapi.SetValue("VTWEG", vt);
                bapi.SetValue("SPART", sp);
                bapi.SetValue("PERNR", pr);
                bapi.SetValue("KUNNR", ku);
                bapi.SetValue("INC", inc);
                bapi.SetValue("DEC", dec);
                bapi.SetValue("FECHA_S", fs);
                bapi.SetValue("FECHA_A", fa);
                bapi.SetValue("FECHA_I", fi);
                bapi.SetValue("FECHA_F", ff);
                bapi.SetValue("MATKL", mk);
                bapi.SetValue("PLTYP", pl);
                bapi.SetValue("MATNR", ma);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("IT_DATA");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }
        #endregion HISTORIAL

        #region bitácora

        public IRfcTable getBitacora(string tipo, string folio) //Trae el cliente consultado
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                //IRfcFunction bapi = repo.CreateFunction("ZFWF_WFO_005");//Módulo de función
                IRfcFunction bapi = repo.CreateFunction("ZFWF_PFI_011");//Módulo de función

                bapi.SetValue("P_ZTIWF", "005");
                bapi.SetValue("P_ZKYWF", folio);

                bapi.Invoke(oDestino);

                IRfcTable tabla = bapi.GetTable("P_BITACORA");
                //IRfcTable tabla = bapi.GetTable("P_FLUJOBIT");
                for (int i = 0; i < tabla.Count; i++)
                {
                    tabla.CurrentIndex = i;

                }

                return tabla;
            }
            else
            {
                return null;
            }
        }

        public string nombrePERNR(string pernr) //Trae la desc o unidad de material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_034");//Módulo de función

                bapi.SetValue("PERNR", pernr);

                bapi.Invoke(oDestino);

                string cadena = bapi.GetString("SNAME");

                return cadena;
            }
            else
            {
                return null;
            }
        }
        public string estatusSol(string tipo, string folio) //Trae la desc o unidad de material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_WFO_004");//Módulo de función

                bapi.SetValue("P_ZTIWF", "005");
                bapi.SetValue("P_ZKYWF", folio);

                bapi.Invoke(oDestino);

                string oper = bapi.GetString("P_ZSWAR");
                string evto = bapi.GetString("P_ZEVTI");
                string cadena = "";

                if (oper.Equals("P"))
                {
                    cadena = "Pendiente";
                }
                else if (oper.Equals("R"))
                {
                    cadena = "Rechazada";
                }
                else if (oper.Equals("A"))
                {
                    cadena = "Aprobada";
                }
                return cadena;
            }
            else
            {
                return null;
            }
        }

        public string porAutorizar(string tipo, string folio)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_035");//Módulo de función

                bapi.SetValue("P_ZTIWF", "005");
                bapi.SetValue("P_ZKYWF", folio);

                bapi.Invoke(oDestino);

                string pernr = bapi.GetString("PERNR");
                string name = bapi.GetString("NAME1");
                string cadena = pernr + " - " + name;

                return cadena;
            }
            else
            {
                return null;
            }
        }
        public string accionBitacora(string tipo, string folio, string oper, string posi, string comentario) //Trae la desc o unidad de material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_036");//Módulo de función
                bapi.SetValue("TIPO", tipo);
                bapi.SetValue("FOLIO", folio);
                bapi.SetValue("OPER", oper);
                bapi.SetValue("POSI", posi);
                bapi.SetValue("COMMENT", comentario);

                bapi.Invoke(oDestino);

                string ret = bapi.GetString("RET");

                return ret;
            }
            else
            {
                return "0";
            }
        }

        public string sigFolio(string tipo, string folio)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_037");//Módulo de función

                bapi.SetValue("P_ZTIWF", "005");
                bapi.SetValue("P_ZKYWF", folio);

                bapi.Invoke(oDestino);

                string pernr = bapi.GetString("ZPOSI");
                string cadena = pernr;

                return cadena;
            }
            else
            {
                return null;
            }
        }
        #endregion bitácora

        #region delegar
        public IRfcTable usuariosDelega(string pr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZFWF_AP_049");//Módulo de función

                bapi.SetValue("PERNR", pr);

                bapi.Invoke(oDestino);

                IRfcTable it_009 = bapi.GetTable("IT_009");

                return it_009;
            }
            else
            {
                return null;
            }
        }
        #endregion delegar
    }
}