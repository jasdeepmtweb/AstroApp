using System;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Admin
{
    public partial class ApprovedAstrologers : System.Web.UI.Page
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
            parameters.Add("p_verification_status", "verified");
            parameters.Add("p_delete_status", 1);

            var res = oDataAccess.QuerySPDynamic<AstrologerModel>("sp_getallastrologers", parameters);

            foreach (var item in res)
            {
                item.profile_picture = $"../Uploads/AstrologerPics/{item.profile_picture}";
            }

            rptAstrolgers.DataSource = res;
            rptAstrolgers.DataBind();
        }
        protected void BtnDetail_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            Response.Redirect($"AstrologerDetails.aspx?id={button.CommandArgument}");
        }
    }
}