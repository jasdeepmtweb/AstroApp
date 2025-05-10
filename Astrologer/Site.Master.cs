using System;
using AstroApp.App_Code;

namespace AstroApp.Astrologer
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["astrousr"] == null)
            {
                Response.Redirect("../AstrologerLogin.aspx");
            }
            if (!IsPostBack)
            {
                GetUserProfile();
            }
        }

        private void GetUserProfile()
        {
            AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;

            lblAstrologerName.Text = oAstrologerModel.name;
        }
    }
}