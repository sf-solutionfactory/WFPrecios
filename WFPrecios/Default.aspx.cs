using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFPrecios
{
    public partial class Default : System.Web.UI.Page
    {
        public string pToken = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("NumEmp");
                Session.Remove("Usuario");
                Session.Remove("TipoEmp");
            }catch
            {

            }
            rObtParametros();

            if (!hidNumEmp.Value.Equals("") & !hidNumEmp.Value.Equals("00000000"))
            {
                //Session["NumEmp"] = hidNumEmp.Value;
                //Session["Usuario"] = hidUsuario.Value;
                //Session["TipoEmp"] = hidTipoEmp.Value;
                Session["NumEmpP"] = hidNumEmp.Value;
                Session["UsuarioP"] = hidUsuario.Value;
                Session["TipoEmpP"] = hidTipoEmp.Value;
                pToken = hidToken.Value;
            }else
            {
                //Response.Redirect("../Default.aspx");
            }
        }

        private void rObtParametros()
        {
            hidToken.Value = Request.QueryString["pToken"];

            try
            {
                string vToken = hidToken.Value.ToString().Trim();
                //string vParametros = vToken;
                string vParametros = "";
                byte[] vByte = Convert.FromBase64String(vToken);

                char[] cArray = System.Text.Encoding.ASCII.GetString(vByte).ToCharArray();
                foreach (char c in cArray)
                {
                    vParametros +=  c;
                }

                int j = vParametros.Trim().Length - 17;
                string[] vDescripcion = vParametros.Substring(17, j).ToString().Split('@');
                hidNumEmp.Value = vParametros.Substring(2, 8);                          //Numero de empleado
                //hidNumEmp.Value = "00032860";                                           //Numero de empleado
                //hidNumEmp.Value = "00030767";                                           //Numero de empleado
                //hidNumEmp.Value = "00010012";
                //hidNumEmp.Value = "00031340";
                //hidNumEmp.Value = "00030767";
                //hidNumEmp.Value = "00030767"; 
                //hidNumEmp.Value = "00030889"; //APROBADOR

                hidUsuario.Value = vDescripcion[0].Trim();                              //Usuario de red
                hidTipoEmp.Value = vParametros.Substring(13, 1);                        //Tipo de usuario M = Master
                hidTipoEmp.Value = "A";                        //Tipo de usuario M = Master
                hidDescUsuario.Value = vDescripcion[1].Trim();                          //Nombre del usuario
                hidToken.Value = vParametros;
            }
            catch (Exception ex)
            {
                string e = ex.Message.ToString();
            }
        }
    }
}