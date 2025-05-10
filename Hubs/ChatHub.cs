using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AstroApp.App_Code;
using Dapper;
using Microsoft.AspNet.SignalR;

namespace AstroApp.Hubs
{
    public class ChatHub : Hub
    {
        // Store connection IDs for users and astrologers
        private static Dictionary<int, string> UserConnections = new Dictionary<int, string>();
        private static Dictionary<int, string> AstrologerConnections = new Dictionary<int, string>();
        private static Dictionary<string, long> ActiveSessions = new Dictionary<string, long>();
        //private static Dictionary<int, DateTime> SessionStartTimes = new Dictionary<int, DateTime>();
        //private static Dictionary<int, decimal> UserBalances = new Dictionary<int, decimal>(); // Store user balances
        //private static Dictionary<int, decimal> AstrologerRates = new Dictionary<int, decimal>(); // Stores astrologer IDs and their rates per minute
        DataAccess oDataAccess = new DataAccess();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private static readonly string[] AllowedFileTypes = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx" };
        private static readonly int MaxFileSize = 5 * 1024 * 1024; // 5MB
        public void RegisterConnection(int userId, int astrologerId, bool isAstrologer)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            if (isAstrologer)
            {
                if (!AstrologerConnections.ContainsKey(astrologerId))
                    AstrologerConnections[astrologerId] = Context.ConnectionId;
            }
            else
            {
                //SessionStartTimes[userId] = indianTime;

                if (!UserConnections.ContainsKey(userId))
                    UserConnections[userId] = Context.ConnectionId;
            }
            string connectionId = Context.ConnectionId;

            long existingSessionId = GetActiveSessionId(userId, astrologerId);

            //int totalMinutesUsed = GetTotalMinutesUsed(userId);
            string sessionType = "free_trial";
            //if (totalMinutesUsed > 30)
            //{
            //    sessionType = "paid";
            //}
            //else
            //{
            //    sessionType = "free_trial";
            //}

            //if (totalMinutesUsed > 30)
            //{
            //    decimal userBalance = GetUserBalance(userId);

            //    if (userBalance < 0) // Check if user has sufficient balance before starting a session
            //    {
            //        Clients.Client(connectionId).sendInsufficientBalanceNotification();
            //        return;
            //    }
            //}

            if (existingSessionId > 0)
            {
                ActiveSessions[connectionId] = existingSessionId;
            }
            else
            {
                long sessionId = SaveSession(userId, astrologerId, sessionType);
                ActiveSessions[connectionId] = sessionId;
            }
        }
        //private decimal GetUserBalance(int userId)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("p_user_id", userId);
        //    //if (!UserBalances.ContainsKey(userId))
        //    //{
        //    decimal balance = oDataAccess.QuerySingleOrDefaultSPDynamic<decimal>("sp_get_user_wallet_balance", parameters);
        //    //UserBalances[userId] = balance;
        //    //}
        //    //return UserBalances[userId];
        //    return balance;
        //}
        //private decimal GetAstrologerRate(int astrologerId)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("p_user_id", astrologerId);
        //    //if (!AstrologerRates.ContainsKey(astrologerId))
        //    //{
        //    var astrologer = oDataAccess.QuerySingleOrDefaultSPDynamic<AstrologerModel>("sp_get_astrologer_by_id", parameters);
        //    if (astrologer != null)
        //    {
        //        //AstrologerRates[astrologerId] = astrologer.per_minute_rate;
        //        return astrologer.per_minute_rate;
        //    }
        //    //}
        //    return 0;
        //    //return AstrologerRates[astrologerId];
        //}
        private int GetTotalMinutesUsed(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", userId);

            return oDataAccess.QuerySingleOrDefaultSPDynamic<int>("sp_get_total_minutes_used_by_user", parameters);
        }
        private int GetActiveSessionId(int userId, int astrologerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", userId);
            parameters.Add("p_astrologer_id", astrologerId);

            return oDataAccess.QuerySingleOrDefaultSPDynamic<int>("sp_get_active_sessionid", parameters);
        }
        public int SaveSession(int userId, int astrologerId, string sessionType)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", userId);
            parameters.Add("p_astrologer_id", astrologerId);
            parameters.Add("p_session_type", sessionType);
            parameters.Add("p_start_time", indianTime);
            parameters.Add("p_end_time", indianTime);
            parameters.Add("p_minutes_used", 0);
            parameters.Add("p_is_active", 1);
            parameters.Add("p_status", "ongoing");
            parameters.Add("p_created_at", indianTime);

            int sessionId = oDataAccess.QuerySingleOrDefaultSPDynamic<int>("sp_insert_chat_sessions", parameters);
            return sessionId;
        }
        public void EndSession(long sessionId)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            var parameters = new DynamicParameters();
            parameters.Add("p_session_id", sessionId);

            var session = oDataAccess.QuerySingleOrDefaultSPDynamic<ChatSessionModel>("sp_get_chat_session_by_id", parameters);

            if (session != null)
            {
                int minutesUsed = (indianTime - session.start_time).Minutes;

                parameters.Add("p_end_time", indianTime);
                parameters.Add("p_is_active", 0);
                parameters.Add("p_status", "completed");
                parameters.Add("p_minutes_used", minutesUsed);

                oDataAccess.ExecuteSPDynamic("sp_end_session", parameters);
            }
        }
        public ChatSessionModel GetSession(long sessionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_session_id", sessionId);

            return oDataAccess.QuerySingleOrDefaultSPDynamic<ChatSessionModel>("sp_get_chat_session_by_id", parameters);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            string connectionId = Context.ConnectionId;

            // Remove from UserConnections
            var userKey = UserConnections.FirstOrDefault(kvp => kvp.Value == connectionId).Key;
            if (userKey > 0) UserConnections.Remove(userKey);

            // Remove from AstrologerConnections
            var astrologerKey = AstrologerConnections.FirstOrDefault(kvp => kvp.Value == connectionId).Key;
            if (astrologerKey > 0) AstrologerConnections.Remove(astrologerKey);

            // End session in DB
            if (ActiveSessions.TryGetValue(connectionId, out long sessionId))
            {
                EndSession(sessionId);
                ActiveSessions.Remove(connectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
        public void SendMessage(int senderId, int receiverId, string message, bool isSenderAstrologer)
        {
            //DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string connectionId = Context.ConnectionId;
            if (!ActiveSessions.TryGetValue(connectionId, out long sessionId))
            {
                return; // Exit if no active session is found
            }

            //int totalMinutesUsed = 0;
            //TimeSpan timeSpent = TimeSpan.Zero;
            //decimal userBalance = 0;
            //if (!isSenderAstrologer)
            //{
            //    userBalance = GetUserBalance(senderId);
            //    if (userBalance <= 0)
            //    {
            //        Clients.Client(connectionId).sendInsufficientBalanceNotification();
            //        return;
            //    }
            //    totalMinutesUsed = GetTotalMinutesUsed(senderId);

            //    DateTime sessionStartTime = SessionStartTimes[senderId];
            //    timeSpent = indianTime - sessionStartTime;
            //}

            //ChatSessionModel oChatSessionModel = GetSession(sessionId);
            //if (oChatSessionModel != null)
            //{
            //    if (oChatSessionModel.session_type.Equals("free_trial"))
            //    {
            //        if (totalMinutesUsed >= 30)
            //        {
            //            Clients.Client(connectionId).sendFreeTrialExpiredNotification();
            //            return;
            //        }
            //    }
            //    if (oChatSessionModel.session_type.Equals("paid"))
            //    {
            //        decimal astrologerRate = GetAstrologerRate(oChatSessionModel.astrologer_id);
            //        int totalMinutesAlotted = (int)Math.Floor(userBalance / astrologerRate);
            //        if (totalMinutesAlotted <= timeSpent.Minutes)
            //        {
            //            Clients.Client(connectionId).sendInsufficientBalanceNotification();
            //            return;
            //        }
            //    }
            //}

            // Save message to the database
            int messageId = SaveMessageToDatabase(senderId, receiverId, message, isSenderAstrologer, sessionId, "text");

            // Determine receiver's connection
            string receiverConnectionId = isSenderAstrologer
                ? (UserConnections.ContainsKey(receiverId) ? UserConnections[receiverId] : null)
                : (AstrologerConnections.ContainsKey(receiverId) ? AstrologerConnections[receiverId] : null);

            if (!string.IsNullOrEmpty(receiverConnectionId))
            {
                // Send the message to the specific receiver
                Clients.Client(receiverConnectionId).receiveMessage(messageId, senderId, message, "text");
            }
            Clients.Caller.receiveMessage(messageId, senderId, message, "text");
        }
        private int SaveMessageToDatabase(int senderId, int receiverId, string message, bool isSenderAstrologer, long sessionId, string messageType)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            string senderType = "user";
            if (isSenderAstrologer)
            {
                senderType = "astrologer";
            }

            var parameters = new DynamicParameters();
            parameters.Add("p_session_id", sessionId);
            parameters.Add("p_sender_type", senderType);
            parameters.Add("p_sender_id", senderId);
            parameters.Add("p_message_text", message);
            parameters.Add("p_message_type", messageType);
            parameters.Add("p_is_read", 0);
            parameters.Add("p_created_at", indianTime);
            parameters.Add("p_receiver_id", receiverId);

            return oDataAccess.QuerySingleOrDefaultSPDynamic<int>("sp_insert_chat_messages", parameters);
        }
        public void MarkMessageAsRead(List<int> messageIds)
        {
            if (messageIds == null || !messageIds.Any())
                return;

            foreach (var messageId in messageIds)
            {
                MarkAsRead(messageId);
            }

            // Notify sender that the message was read
            Clients.All.notifyReadReceipt(messageIds);
        }
        public void MarkAsRead(int messageId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_is_read", 1);
            parameters.Add("p_message_id", messageId);

            oDataAccess.ExecuteSPDynamic("sp_update_read_reciept", parameters);
        }
        public void SendMediaMessage(int senderId, int receiverId, string base64Data, string fileName, string fileType, bool isSenderAstrologer)
        {
            string connectionId = Context.ConnectionId;
            if (!ActiveSessions.TryGetValue(connectionId, out long sessionId))
            {
                return;
            }

            if (fileType == "audio/wav")
            {
                // Ensure the uploads directory exists
                string voiceUploadPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/VoiceNotes"));
                //if (!Directory.Exists(voiceUploadPath))
                //{
                //    Directory.CreateDirectory(voiceUploadPath);
                //}

                // Generate unique filename for voice note
                string uniqueFileName = $"voice_{DateTime.Now.Ticks}.wav";
                string uploadPath = Path.Combine(voiceUploadPath, uniqueFileName);
                //string fileUrl = $"/Uploads/VoiceNotes/{uniqueFileName}";

                // Save the voice note
                byte[] audioBytes = Convert.FromBase64String(base64Data.Split(',')[1]);
                File.WriteAllBytes(uploadPath, audioBytes);

                // Save message to database with voice note type
                int messageId = SaveMessageToDatabase(senderId, receiverId, uniqueFileName, isSenderAstrologer, sessionId, "media");

                // Determine receiver's connection
                string receiverConnectionId = isSenderAstrologer
                    ? (UserConnections.ContainsKey(receiverId) ? UserConnections[receiverId] : null)
                    : (AstrologerConnections.ContainsKey(receiverId) ? AstrologerConnections[receiverId] : null);

                if (!string.IsNullOrEmpty(receiverConnectionId))
                {
                    Clients.Client(receiverConnectionId).receiveMessage(messageId, senderId, uniqueFileName, "media");
                }
                Clients.Caller.receiveMessage(messageId, senderId, uniqueFileName, "media");
            }
            else
            {
                // Validate file type
                string extension = Path.GetExtension(fileName).ToLower();
                string onlyFileName = Path.GetFileNameWithoutExtension(fileName).ToLower();
                if (!AllowedFileTypes.Contains(extension))
                {
                    Clients.Caller.SendAsync("errorMessage", "File type not allowed");
                    return;
                }

                // Convert base64 to byte array and validate size
                byte[] fileBytes = Convert.FromBase64String(base64Data.Split(',')[1]);
                if (fileBytes.Length > MaxFileSize)
                {
                    Clients.Caller.SendAsync("errorMessage", "File size exceeds limit of 5mb");
                    return;
                }
                //string uniqueIdentifier = Guid.NewGuid().ToString(); // For a GUID
                // OR: Use a random number
                string uniqueIdentifier = new Random().Next(1000, 9999).ToString();
                // OR: Use timestamp
                //string uniqueIdentifier = DateTime.Now.ToString("yyyyMMddHHmmss");
                // Generate unique filename
                string uniqueFileName = $"{onlyFileName}_{uniqueIdentifier}{extension}";
                string uploadPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/Chats"), uniqueFileName);
                //string fileUrl = $"/Uploads/Chats/{uniqueFileName}";

                // Save file
                File.WriteAllBytes(uploadPath, fileBytes);

                // Save message to database
                int messageId = SaveMessageToDatabase(senderId, receiverId, uniqueFileName, isSenderAstrologer, sessionId, "media");

                // Determine receiver's connection
                string receiverConnectionId = isSenderAstrologer
                    ? (UserConnections.ContainsKey(receiverId) ? UserConnections[receiverId] : null)
                    : (AstrologerConnections.ContainsKey(receiverId) ? AstrologerConnections[receiverId] : null);

                if (!string.IsNullOrEmpty(receiverConnectionId))
                {
                    Clients.Client(receiverConnectionId).receiveMessage(messageId, senderId, uniqueFileName, "media");
                }
                Clients.Caller.receiveMessage(messageId, senderId, uniqueFileName, "media");
            } 
        }
    }
}