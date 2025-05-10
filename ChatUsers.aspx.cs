using System;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class ChatUsers : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usr"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                GetRecentChats();
            }
        }

        private void GetRecentChats()
        {
            UserModel oUserModel = Session["usr"] as UserModel;

            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", oUserModel.user_id);

            var res = oDataAccess.QuerySPDynamic<RecentChatsUserModel>("sp_get_recent_chats_for_user", parameters);

            foreach (var item in res)
            {
                item.profile_picture = $"Uploads/AstrologerPics/{item.profile_picture}";
            }

            rptChatUsers.DataSource = res;
            rptChatUsers.DataBind();
        }
        public string GetOnlineStatus(object astro)
        {
            RecentChatsUserModel oRecentChatsUserModel = astro as RecentChatsUserModel;

            if (oRecentChatsUserModel.is_online)
            {
                return "p-3 online";
            }
            else
            {
                return "p-3 offline";
            }
        }

        protected void LinkChat_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            Response.Redirect($"Chat.aspx?id={btn.CommandArgument}");
        }
    }
}