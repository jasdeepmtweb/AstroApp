using System;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class WalletTransactions : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usr"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                GetUserData();
            }
        }
        private void GetUserData()
        {
            UserModel oUserModel = Session["usr"] as UserModel;

            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", oUserModel.user_id);

            var res = oDataAccess.QuerySingleOrDefaultSPDynamic<WalletModel>("sp_getuser_balance", parameters);
            if (res != null)
            {
                lblWalletBalance.Text = res.balance.ToString();
            }
            else
            {
                lblWalletBalance.Text = "0";
            }
        }
    }
}