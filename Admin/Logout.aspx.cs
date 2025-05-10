using System;

namespace AstroApp.Admin
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("adm");
            Response.Redirect("../AdminLogin.aspx");
        }
    }
}