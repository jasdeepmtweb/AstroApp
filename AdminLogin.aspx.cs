using System;
using System.Web.UI;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_username", txtUsername.Text);
                parameters.Add("p_password_hash", txtPassword.Text.Trim());

                var res = oDataAccess.QuerySingleOrDefaultSPDynamic<AdminModel>("sp_admin_login", parameters);
                if (res != null)
                {
                    Session["adm"] = res;
                    Response.Redirect("Admin/ShowPendingAstrologers.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Invalid Username Or Password!', 'warning');", true);
                }
            }
        }
    }
}