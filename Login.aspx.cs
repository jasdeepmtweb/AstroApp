using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.UI;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class Login : System.Web.UI.Page
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDialingCodes();
            }
        }
        private void PopulateDialingCodes()
        {
            var countryCodes = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("IN +91", "91"),
        new KeyValuePair<string, string>("US +1", "1"),
        new KeyValuePair<string, string>("UK +44", "44"),
        new KeyValuePair<string, string>("AE +971", "971"),
        new KeyValuePair<string, string>("AU +61", "61"),
        new KeyValuePair<string, string>("BD +880", "880"),
        new KeyValuePair<string, string>("BR +55", "55"),
        new KeyValuePair<string, string>("CA +1", "1"),
        new KeyValuePair<string, string>("CN +86", "86"),
        new KeyValuePair<string, string>("DE +49", "49"),
        new KeyValuePair<string, string>("FR +33", "33"),
        new KeyValuePair<string, string>("HK +852", "852"),
        new KeyValuePair<string, string>("ID +62", "62"),
        new KeyValuePair<string, string>("IR +98", "98"),
        new KeyValuePair<string, string>("IT +39", "39"),
        new KeyValuePair<string, string>("JP +81", "81"),
        new KeyValuePair<string, string>("KR +82", "82"),
        new KeyValuePair<string, string>("MY +60", "60"),
        new KeyValuePair<string, string>("NZ +64", "64"),
        new KeyValuePair<string, string>("PK +92", "92"),
        new KeyValuePair<string, string>("PH +63", "63"),
        new KeyValuePair<string, string>("RU +7", "7"),
        new KeyValuePair<string, string>("SA +966", "966"),
        new KeyValuePair<string, string>("SG +65", "65"),
        new KeyValuePair<string, string>("TH +66", "66"),
        new KeyValuePair<string, string>("TR +90", "90"),
        new KeyValuePair<string, string>("VN +84", "84")
    };

            countryCodes.Sort((x, y) => x.Key.CompareTo(y.Key));

            ddlDialingCode.DataSource = countryCodes;
            ddlDialingCode.DataTextField = "Key";
            ddlDialingCode.DataValueField = "Value";
            ddlDialingCode.DataBind();

            ddlDialingCode.SelectedValue = "91";
        }

        protected void BtnGetOTP_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string otp = GenerateOTP(4);
                try
                {
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    var parameters = new DynamicParameters();
                    parameters.Add("p_name", "-");
                    parameters.Add("p_email", Guid.NewGuid());
                    parameters.Add("p_phone", txtMobileNo.Text);
                    parameters.Add("p_password_hash", otp);
                    parameters.Add("p_date_of_birth", indianTime);
                    parameters.Add("p_birth_place", "-");
                    parameters.Add("p_birth_time", indianTime);
                    parameters.Add("p_profile_picture", "-");
                    parameters.Add("p_wallet_balance", 0);
                    parameters.Add("p_remaining_free_minutes", 30);
                    parameters.Add("p_created_at", indianTime);
                    parameters.Add("p_updated_at", indianTime);

                    var res = oDataAccess.QuerySingleOrDefaultSPDynamic<int>("sp_insert_user", parameters);

                    Session["newid"] = txtMobileNo.Text.Trim();

                    string message = $"Dear User, {otp} is your One Time Password(OTP) for Phone Verification.-Vowel Digital";

                    Send_Msg(txtMobileNo.Text.Trim(), message);

                    Response.Redirect("OtpConfirm.aspx");
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("aborted"))
                    {
                        if (ex.Message.Contains("Duplicate"))
                        {
                            if (ex.Message.Contains("phone"))
                            {
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Mobile No. Already Exists!', 'warning');", true);

                                var parameters = new DynamicParameters();
                                parameters.Add("p_password_hash", otp);
                                parameters.Add("p_phone", txtMobileNo.Text);

                                oDataAccess.ExecuteSPDynamic("sp_update_otp", parameters);

                                Session["newid"] = txtMobileNo.Text.Trim();

                                string message = $"Dear User, {otp} is your One Time Password(OTP) for Phone Verification.-Vowel Digital";

                                Send_Msg(txtMobileNo.Text.Trim(), message);

                                Response.Redirect("OtpConfirm.aspx");
                            }
                            if (ex.Message.Contains("email"))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Email Id Already Exists!', 'warning');", true);
                            }
                        }
                    }
                    else
                    {
                        Log.LogError(ex);
                    }
                }
            }
        }
        public static string GenerateOTP(int length = 6)
        {
            Random generator = new Random();

            return generator.Next((int)Math.Pow(10, (length - 1)), (int)Math.Pow(10, length) - 1).ToString();
        }
        public static void Send_Msg(String mobileNumber, String message)
        {
            String strUrl = $"https://smsotp.in/index.php/smsapi/httpapi/?uname=solversolution&password=solversolution@45&sender=VOWELD&tempid=1607100000000340901&receiver={mobileNumber}&route=TA&msgtype=1&sms={message}";
            WebRequest request = WebRequest.Create(strUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
        }
    }
}