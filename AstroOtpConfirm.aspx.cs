using System;
using System.Web.UI;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class AstroOtpConfirm : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["astromob"] == null)
            {
                Response.Redirect("AstrologerLogin.aspx");
            }
            if (Session["astrootp"] == null)
            {
                Response.Redirect("AstrologerLogin.aspx");
            }
            if (!IsPostBack)
            {
                lblMobileNo.Text = Session["astromob"].ToString();
            }
        }

        protected void BtnVerify_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string userOTP = txtOTP1.Text + txtOTP2.Text + txtOTP3.Text + txtOTP4.Text;

                if (userOTP.Equals(Session["astrootp"].ToString()))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("p_delete_status", 1);
                    parameters.Add("p_phone", lblMobileNo.Text);
                    parameters.Add("p_verification_status", "verified");

                    var res = oDataAccess.QuerySingleOrDefaultSPDynamic<AstrologerModel>("sp_astrologer_login", parameters);

                    if (res != null)
                    {
                        Session["astrousr"] = res;

                        Session.Remove("astrootp");
                        Session.Remove("astromob");

                        UpdateOnlineStatus(res);

                        Response.Redirect("Astrologer/AstrologerProfile.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Profile Not Verified!', 'warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Invalid OTP!', 'warning');", true);
                }
            }
        }

        private void UpdateOnlineStatus(AstrologerModel oAstrologerModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_is_online", 1);
            parameters.Add("p_astrologer_id", oAstrologerModel.astrologer_id);

            oDataAccess.ExecuteSPDynamic("sp_update_online_status", parameters);
        }
    }
}