using System;
using System.Web.UI;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class OtpConfirm : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["newid"] == null)
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
            var parameters = new DynamicParameters();
            parameters.Add("p_phone", Session["newid"]);

            var res = oDataAccess.QuerySingleOrDefaultSPDynamic<UserModel>("sp_getuserby_phone", parameters);
            if (res != null)
            {
                lblMobileNo.Text = res.phone;
            }
        }

        protected void BtnVerify_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_phone", Session["newid"]);

                var res = oDataAccess.QuerySingleOrDefaultSPDynamic<UserModel>("sp_getuserby_phone", parameters);
                if (res != null)
                {

                    string userOTP = txtOTP1.Text + txtOTP2.Text + txtOTP3.Text + txtOTP4.Text;

                    if (userOTP.Equals(res.password_hash))
                    {
                        Session["usr"] = res;

                        Session.Remove("newid");

                        UpdateOnlineStatus(res);

                        Response.Redirect("Index.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Invalid OTP!', 'warning');", true);
                    }
                }
            }
        }

        private void UpdateOnlineStatus(UserModel oUserModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_is_online", 1);
            parameters.Add("p_user_id", oUserModel.user_id);

            oDataAccess.ExecuteSPDynamic("sp_update_user_online_status", parameters);
        }
    }
}