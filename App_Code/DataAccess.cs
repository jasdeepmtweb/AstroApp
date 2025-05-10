using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace AstroApp.App_Code
{
    public class DataAccess
    {
        private readonly string _connectionString;
        public DataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
        }
        public void ExecuteDynamic(string sql, DynamicParameters parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }
        public T QuerySingleOrDefaultDynamic<T>(string sql, DynamicParameters parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<T>(sql, parameters);
            }
        }
        public List<T> QueryDynamic<T>(string sql, DynamicParameters parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.Query<T>(sql, parameters).ToList();
            }
        }
        public object ExecuteSPDynamic(string spName, DynamicParameters parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int ExeSP(String QueryText, DynamicParameters paras)
        {
            int Result;

            using (var connection = new MySqlConnection(_connectionString))
            {
                Result = connection.Execute(QueryText, paras, commandType: CommandType.StoredProcedure);
            }
            return Result;
        }
        public T QuerySingleOrDefaultSPDynamic<T>(string spName, DynamicParameters parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<T>(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public List<T> QuerySPDynamic<T>(string spName, DynamicParameters parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.Query<T>(spName, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public List<T> QueryListSPDynamic<T>(string spName, DynamicParameters parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.Query<T>(spName, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
    public class Log
    {
        private const long MaxFileSizeBytes = 1000000;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public static void LogError(Exception ex, int z = 0)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            string FileName = indianTime.ToString("yyyyMMdd") + ".txt";
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string FullFileName = Path.Combine(projectDirectory, "Logs", FileName);

            string logMessage = $"{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE)}: {ex}";

            FileInfo fileInfo = new FileInfo(FullFileName);

            if (fileInfo.Exists && fileInfo.Length >= MaxFileSizeBytes)
            {
                // Backup the current log file before clearing it
                string backupFilePath = $"{FullFileName}.{DateTime.Now:yyyyMMddHHmmss}.bak";
                File.Move(FullFileName, backupFilePath);

                // Clear the log file and write the new log message
                File.WriteAllText(FullFileName, logMessage);
            }
            else
            {
                // Append the new log message to the existing log file
                string existingContent = fileInfo.Exists ? File.ReadAllText(FullFileName) : "";
                string newContent = $"{logMessage}\n\n{existingContent}";
                File.WriteAllText(FullFileName, newContent);
            }
        }
    }
    public class AdminModel
    {
        public int admin_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public string full_name { get; set; }
        public int is_active { get; set; }
        public DateTime last_login { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class AstrologerModel
    {
        public int astrologer_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password_hash { get; set; }
        public string profile_picture { get; set; }
        public string bio { get; set; }
        public int experience_years { get; set; }
        public string specializations { get; set; }
        public string languages_known { get; set; }
        public decimal hourly_rate { get; set; }
        public decimal per_minute_rate { get; set; }
        public decimal rating { get; set; }
        public int total_consultations { get; set; }
        public bool is_online { get; set; }
        public string verification_status { get; set; } // "pending", "verified", "rejected"
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int delete_status { get; set; }
    }
    public class SpecializationModel//specialization_categories
    {
        public int category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int is_active { get; set; }
        public int display_order { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class UserModel
    {
        public int user_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password_hash { get; set; }
        public DateTime date_of_birth { get; set; }
        public string birth_place { get; set; }
        public TimeSpan birth_time { get; set; }
        public string profile_picture { get; set; }
        public decimal wallet_balance { get; set; }
        public int remaining_free_minutes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public bool is_online { get; set; }
    }
    public class WalletModel
    {
        public int wallet_id { get; set; }
        public int user_id { get; set; }
        public decimal balance { get; set; }
        public DateTime last_updated { get; set; }
    }
    public class ChatMessagesModel//chat_messages
    {
        public int message_id { get; set; }
        public int session_id { get; set; }
        public string sender_type { get; set; }
        public int sender_id { get; set; }
        public string message_text { get; set; }
        public string message_type { get; set; }
        public bool is_read { get; set; }
        public DateTime created_at { get; set; }
        public int receiver_id { get; set; }
    }
    public class RecentChatsAstrologerModel
    {
        public int user_id { get; set; }
        public string UserName { get; set; }
        public string profile_picture { get; set; }
        public string phone { get; set; }
        public bool is_online { get; set; }
    }
    public class RecentChatsUserModel
    {
        public int astrologer_id { get; set; }
        public string UserName { get; set; }
        public string profile_picture { get; set; }
        public string phone { get; set; }
        public bool is_online { get; set; }
    }
    public class ChatSessionModel
    {
        public int session_id { get; set; }
        public int user_id { get; set; }
        public int astrologer_id { get; set; }
        public string session_type { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public int minutes_used { get; set; }
        public bool is_active { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
    }
}