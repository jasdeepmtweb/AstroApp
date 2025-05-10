using System;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Astrologer
{
    public partial class AstrologerChat : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        public string astrologerImage = "";
        public string userImage = "";
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["astrousr"] == null)
            {
                Response.Redirect("../AstrologerLogin.aspx");
            }
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("AstroHome.aspx");
            }
            if (!IsPostBack)
            {
                GetUserData();
                GetChatHistory();
            }
        }
        public string GetUserImage(object senderId)
        {
            AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;

            if ((int)senderId == oAstrologerModel.astrologer_id)
            {
                return astrologerImage;
            }
            return userImage;
        }
        public string GetReadUnreadClass(object messageModel)
        {
            ChatMessagesModel oChatMessagesModel = messageModel as ChatMessagesModel;

            AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;

            if (oChatMessagesModel.sender_id == oAstrologerModel.astrologer_id)
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
        private void GetChatHistory()
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_UserID", hdnUserID.Value);
            parameters.Add("p_AstrologerID", hdnAstrologerID.Value);

            var res = oDataAccess.QuerySPDynamic<ChatMessagesModel>("sp_get_chat_messages", parameters);

            rptChat.DataSource = res;
            rptChat.DataBind();
        }
        private void GetUserData()
        {
            AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;

            hdnAstrologerID.Value = oAstrologerModel.astrologer_id.ToString();
            astrologerImage = $"../Uploads/AstrologerPics/{oAstrologerModel.profile_picture}";

            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", Request.QueryString["id"]);

            var res = oDataAccess.QuerySingleOrDefaultSPDynamic<UserModel>("sp_getuserby_id", parameters);
            if (res != null)
            {
                lblUsername.Text = res.name;
                if (res.is_online)
                {
                    lblOnlineStatus.Text = "Active Now";
                }
                else
                {
                    lblOnlineStatus.Text = "Offline";
                }
                ImgUser.ImageUrl = $"../Uploads/UserPics/{res.profile_picture}";
                userImage = $"../Uploads/UserPics/{res.profile_picture}";

                hdnUserID.Value = res.user_id.ToString();
            }
        }
        public string GetOnlineStatus()
        {
            if (lblOnlineStatus.Text.Equals("Active Now"))
            {
                return "active-status";
            }
            return "active-status text-danger";
        }
        public string GetMediaHtml(string messageText)
        {
            string fileExtension = System.IO.Path.GetExtension(messageText).ToLower();
            string filePath = $"../Uploads/Chats/{messageText}";

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
                filePath = $"../Uploads/VoiceNotes/{messageText}";
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