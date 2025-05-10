using System;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class LogoutUser : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserModel oUserModel = Session["usr"] as UserModel;
            UpdateOnlineStatus(oUserModel);
            Session.Remove("usr");
            Response.Redirect("Index.aspx");
        }
        private void UpdateOnlineStatus(UserModel oUserModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_is_online", 0);
            parameters.Add("p_user_id", oUserModel.user_id);

            oDataAccess.ExecuteSPDynamic("sp_update_user_online_status", parameters);
        }
    }
}