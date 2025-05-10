using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Astrologer
{
    public partial class ChatMenu : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["astrousr"] == null)
            {
                Response.Redirect("../AstrologerLogin.aspx");
            }
            if (!IsPostBack)
            {
                GetRecentChats();
            }
        }

        private void GetRecentChats()
        {
            AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;

            var parameters = new DynamicParameters();
            parameters.Add("p_astrologer_id", oAstrologerModel.astrologer_id);

            var res = oDataAccess.QuerySPDynamic<RecentChatsAstrologerModel>("sp_get_recent_chats_for_astrologer", parameters);

            if (Request.QueryString["qry"] != null && !Request.QueryString["qry"].ToString().Equals(""))
            {
                res = res.Where(x => x.UserName.ToLower().Contains(Request.QueryString["qry"].ToString().ToLower())).ToList();
            }

            foreach (var item in res)
            {
                item.profile_picture = $"../Uploads/UserPics/{item.profile_picture}";
            }

            rptRecentChats.DataSource = res;
            rptRecentChats.DataBind();
        }
        public string GetOnlineStatus(object user)
        {
            RecentChatsAstrologerModel oRecentChatsAstrologerModel = user as RecentChatsAstrologerModel;

            if (oRecentChatsAstrologerModel.is_online)
            {
                return "p-3 online";
            }
            else
            {
                return "p-3 offline";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            GetRecentChats();
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AstroHome.aspx?qry={txtSearch.Text}");
        }

        protected void LinkChat_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            Response.Redirect($"AstrologerChat.aspx?id={btn.CommandArgument}");
        }
    }
}