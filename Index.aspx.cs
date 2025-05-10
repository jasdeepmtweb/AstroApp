using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class Index : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllData();
            }
        }
        private void GetAllData()
        {
            var parameters = new DynamicParameters();
            //parameters.Add("p_verification_status", "verified");
            parameters.Add("p_delete_status", 1);

            var res = oDataAccess.QuerySPDynamic<AstrologerModel>("sp_getallastrologers_without_verify", parameters);
            //var res = oDataAccess.QuerySPDynamic<AstrologerModel>("sp_getallastrologers", parameters);

            if (Request.QueryString["qry"] != null && !Request.QueryString["qry"].ToString().Equals(""))
            {
                res = res.Where(x => x.name.ToLower().Contains(Request.QueryString["qry"].ToString().ToLower())).ToList();
            }

            foreach (var item in res)
            {
                item.profile_picture = $"../Uploads/AstrologerPics/{item.profile_picture}";
            }

            rptAstrolgers.DataSource = res;
            rptAstrolgers.DataBind();
        }
        public string GetOnlineStatus(object astro)
        {
            AstrologerModel oAstrologerModel = astro as AstrologerModel;

            if (oAstrologerModel.is_online)
            {
                return "btn m-1 btn-outline-success px-4 rounded-3";
            }
            return "btn m-1 btn-outline-danger px-4 rounded-3";
        }
        public string GetVerificationStatus(object verStatus)
        {
            if (verStatus.ToString().Equals("verified"))
            {
                return "img/icon-check.png";
            }
            return "img/icon-warning.png";
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            GetAllData();
        }

        protected void LinkChat_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            Response.Redirect($"Chat.aspx?id={btn.CommandArgument}");
        }
    }
}