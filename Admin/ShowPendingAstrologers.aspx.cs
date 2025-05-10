using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Admin
{
    public partial class ShowPendingAstrologers : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                Response.Redirect("../AdminLogin.aspx");
            }
            if (!IsPostBack)
            {
                GetAllData();
            }
        }

        private void GetAllData()
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_verification_status", "pending");
            parameters.Add("p_delete_status", 1);

            var res = oDataAccess.QuerySPDynamic<AstrologerModel>("sp_getallastrologers", parameters);

            foreach (var item in res)
            {
                item.profile_picture = $"../Uploads/AstrologerPics/{item.profile_picture}";
            }

            rptAstrolgers.DataSource = res;
            rptAstrolgers.DataBind();
        }

        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            var parameters = new DynamicParameters();
            parameters.Add("p_verification_status", "verified");
            parameters.Add("p_astrologer_id", btn.CommandArgument);

            oDataAccess.ExecuteSPDynamic("sp_update_astrologer_verified_status", parameters);

            ShowSweetAlert();
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            var parameters = new DynamicParameters();
            parameters.Add("p_verification_status", "rejected");
            parameters.Add("p_astrologer_id", btn.CommandArgument);

            oDataAccess.ExecuteSPDynamic("sp_update_astrologer_verified_status", parameters);

            ShowUpdateSweetAlert();
        }
        protected void ShowSweetAlert()
        {
            // Execute JavaScript code to show the SweetAlert and redirect after it is dismissed
            string script = @"
        Swal.fire({
            title: 'Success!',
            text: 'Approved',
            icon: 'success',
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                window.location.href = 'ShowPendingAstrologers.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
        protected void ShowUpdateSweetAlert()
        {
            // Execute JavaScript code to show the SweetAlert and redirect after it is dismissed
            string script = @"
        Swal.fire({
            title: 'Success!',
            text: 'Rejected',
            icon: 'success',
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                window.location.href = 'ShowPendingAstrologers.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
    }
}