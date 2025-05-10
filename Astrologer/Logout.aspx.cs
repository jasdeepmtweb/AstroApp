using System;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Astrologer
{
    public partial class Logout : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;
            UpdateOnlineStatus(oAstrologerModel);
            Session.Remove("astrousr");
            Response.Redirect("../AstrologerLogin.aspx");
        }

        private void UpdateOnlineStatus(AstrologerModel oAstrologerModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_is_online", 0);
            parameters.Add("p_astrologer_id", oAstrologerModel.astrologer_id);

            oDataAccess.ExecuteSPDynamic("sp_update_online_status", parameters);
        }
    }
}