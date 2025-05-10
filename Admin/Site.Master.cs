using System;

namespace AstroApp.Admin
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                Response.Redirect("../AdminLogin.aspx");
            }
        }
    }
}