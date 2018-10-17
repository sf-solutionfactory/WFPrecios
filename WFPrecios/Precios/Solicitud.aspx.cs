using System;

namespace WFPrecios.Precios
{
    public partial class Solicitud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string i = Session["Usuario"].ToString();
                string ii = Session["NumEmp"].ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("https://www.terzaonline.com/nworkflow/login/");
            }

        }
    }
}