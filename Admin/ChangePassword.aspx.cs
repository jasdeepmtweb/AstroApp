using System;
using System.Web.UI;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Admin
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                Response.Redirect("../AdminLogin.aspx");
            }
            if (!IsPostBack)
            {
                AdminModel oAdminModel = Session["adm"] as AdminModel;

                cmpOldPassword.ValueToCompare = oAdminModel.password_hash;
            }
        }
        protected void ChangePasswordAlert()
        {
            // Execute JavaScript code to show the SweetAlert and redirect after it is dismissed
            string script = @"
        Swal.fire({
            title: 'Success!',
            text: 'Password Changed.Please Login Again',
            icon: 'success',
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                window.location.href = '../AdminLogin.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    AdminModel oAdminModel = Session["adm"] as AdminModel;

                    var parameters = new DynamicParameters();
                    parameters.Add("p_admin_id", oAdminModel.admin_id);
                    parameters.Add("p_password_hash", txtConfirmNewPassword.Text.Trim());
                    DataAccess oDataAccess = new DataAccess();
                    oDataAccess.ExecuteSPDynamic("sp_changeadmin_password", parameters);

                    ChangePasswordAlert();

                    Session.Remove("adm");
                }
                catch (Exception ex)
                {
                    Log.LogError(ex);
                }
            }
        }
    }
}