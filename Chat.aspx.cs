using System;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class Chat : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        public string astrologerImage = "";
        public string userImage = "";
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usr"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            if (!IsPostBack)
            {
                GetAstrologerData();
            }
            GetChatHistory();
        }

        private void GetChatHistory()
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_UserID", hdnUserID.Value);
            parameters.Add("p_AstrologerID", hdnAstrologerID.Value);

            var res = oDataAccess.QuerySPDynamic<ChatMessagesModel>("sp_get_chat_messages", parameters);

            rptChat.DataSource = res;
            rptChat.DataBind();
        }
        public string GetUserImage(object senderId)
        {
            UserModel oUserModel = Session["usr"] as UserModel;

            if ((int)senderId == oUserModel.user_id)
            {
                return userImage;
            }
            return astrologerImage;
        }
        public string GetReadUnreadClass(object messageModel)
        {
            ChatMessagesModel oChatMessagesModel = messageModel as ChatMessagesModel;

            UserModel oUserModel = Session["usr"] as UserModel;

            if (oChatMessagesModel.sender_id == oUserModel.user_id)
            {
                if (oChatMessagesModel.is_read)
                {
                    return "read";
                }
                else
                {
                    return "unread";
                }
            }
            return "";
        }
        public string GetOnlineStatus()
        {
            if (lblOnlineStatus.Text.Equals("Active Now"))
            {
                return "active-status";
            }
            return "active-status text-danger";
        }
        private void GetAstrologerData()
        {
            UserModel oUserModel = Session["usr"] as UserModel;

            hdnUserID.Value = oUserModel.user_id.ToString();
            userImage = $"Uploads/UserPics/{oUserModel.profile_picture}";

            var parameters = new DynamicParameters();
            parameters.Add("p_astrologer_id", Request.QueryString["id"]);

            var res = oDataAccess.QuerySingleOrDefaultSPDynamic<AstrologerModel>("sp_get_astrologer_by_id", parameters);
            if (res != null)
            {
                lblAstrologerName.Text = res.name;
                if (res.is_online)
                {
                    lblOnlineStatus.Text = "Active Now";
                }
                else
                {
                    lblOnlineStatus.Text = "Offline";
                }
                ImgAstrologer.ImageUrl = $"Uploads/AstrologerPics/{res.profile_picture}";
                astrologerImage = $"Uploads/AstrologerPics/{res.profile_picture}";

                hdnAstrologerID.Value = res.astrologer_id.ToString();
            }
        }
        public string GetMediaHtml(string messageText)
        {
            string fileExtension = System.IO.Path.GetExtension(messageText).ToLower();
            string filePath = $"Uploads/Chats/{messageText}";

            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
            {
                // Render image tag for image files
                return $@"
            <a class='portfolio-item large single-gallery-item image-zooming-in-out' title='{messageText}' 
               data-gall='gallery01' href='{filePath}'>
                <img src='{filePath}' alt=''>
<div class='fav-icon shadow'>
           <a id='downloadLink' class='bi bi-file-earmark-arrow-down-fill' href='{filePath}' download>
                </a>
</div>
            </a>";
            }
            else if (fileExtension == ".pdf")
            {
                // Render download button with PDF logo
                return $@"
            <a href='{filePath}' class='btn btn-danger' download>
                <i class='bi bi-file-earmark-pdf-fill'></i> {messageText}
            </a>";
            }
            else if (fileExtension == ".doc" || fileExtension == ".docx")
            {
                // Render download button with Word document logo
                return $@"
            <a href='{filePath}' class='btn btn-info' download>
                <i class='bi bi-file-earmark-word-fill'></i> {messageText}
            </a>";
            }
            else if (fileExtension == ".wav")
            {
                filePath = $"Uploads/VoiceNotes/{messageText}";
                // For unsupported file types, provide a generic download option
                return $@"
            <audio controls>
                        <source src='{filePath}' type='audio/wav'>
                        Your browser does not support the audio element.
                    </audio>
<br/>
<a href='{filePath}' download='{messageText}'>
 <i class='bi bi-file-earmark-arrow-down-fill'></i>
</a>";
            }
            else
            {
                // For unsupported file types, provide a generic download option
                return $@"
            <a href='{filePath}' class='btn btn-secondary' download>
                <i class='bi bi-file-earmark'></i> {messageText}
            </a>";
            }
        }
    }
}