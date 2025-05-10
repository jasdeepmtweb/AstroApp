using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Admin
{
    public partial class AddSpecialization : System.Web.UI.Page
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
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
            if (GridSpecialization.Rows.Count > 0)
            {
                GridSpecialization.UseAccessibleHeader = true;
                GridSpecialization.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridSpecialization.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        private void GetAllData()
        {
            //var parameters = new DynamicParameters();
            //parameters.Add("p_verification_status", "pending");
            //parameters.Add("p_delete_status", 1);

            var res = oDataAccess.QuerySPDynamic<SpecializationModel>("sp_getall_specialization");

            GridSpecialization.DataSource = res;
            GridSpecialization.DataBind();

            if (GridSpecialization.Rows.Count > 0)
            {
                GridSpecialization.UseAccessibleHeader = true;
                GridSpecialization.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridSpecialization.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    var parameters = new DynamicParameters();
                    parameters.Add("p_name", txtSpecialization.Text);
                    parameters.Add("p_description", "-");
                    parameters.Add("p_is_active", 1);
                    parameters.Add("p_display_order", 1);
                    parameters.Add("p_created_at", indianTime);
                    parameters.Add("p_updated_at", indianTime);

                    oDataAccess.ExecuteSPDynamic("sp_insert_specialization_categories", parameters);

                    ShowSweetAlert();
                }
                catch (Exception ex)
                {
                    Log.LogError(ex);
                }
            }
        }
        protected void ShowSweetAlert()
        {
            // Execute JavaScript code to show the SweetAlert and redirect after it is dismissed
            string script = @"
        Swal.fire({
            title: 'Success!',
            text: 'Added',
            icon: 'success',
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                window.location.href = 'AddSpecialization.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
        protected void ShowDeleteAlert()
        {
            // Execute JavaScript code to show the SweetAlert and redirect after it is dismissed
            string script = @"
        Swal.fire({
            title: 'Success!',
            text: 'Deleted',
            icon: 'success',
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                window.location.href = 'AddSpecialization.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            var parameters = new DynamicParameters();
            parameters.Add("p_category_id", button.CommandArgument);

            oDataAccess.ExecuteSPDynamic("sp_delete_Specialization", parameters);

            ShowDeleteAlert();
        }
    }
}