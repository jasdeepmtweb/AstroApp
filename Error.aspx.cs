using System;

namespace AstroApp
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["PreviousPageUrl"].ToString());
        }
    }
}