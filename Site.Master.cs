using System;
using AstroApp.App_Code;

namespace AstroApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usr"] != null)
            {
                GetUserData();
                liLogout.Visible = true;
            }
            else
            {
                liLogout.Visible = false;
            }
        }

        private void GetUserData()
        {
            //UserModel oUserModel = Session["usr"] as UserModel;

            //var parameters = new DynamicParameters();
            //parameters.Add("p_user_id", oUserModel.user_id);

            //var res = oDataAccess.QuerySingleOrDefaultSPDynamic<WalletModel>("sp_getuser_balance", parameters);
            //if (res != null)
            //{
            //    lblWalletBalance.Text = res.balance.ToString();
            //}
            //else
            //{
            //    lblWalletBalance.Text = "0";
            //}
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Index.aspx?qry={txtSearch.Text}");
        }
    }
}