using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.UI;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class AstrologerLogin : System.Web.UI.Page
    {
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

                var parameters = new DynamicParameters();
                parameters.Add("p_delete_status", 1);
                parameters.Add("p_phone", txtMobileNo.Text.Trim());
                parameters.Add("p_verification_status", "verified");

                var res = oDataAccess.QuerySingleOrDefaultSPDynamic<AstrologerModel>("sp_astrologer_login", parameters);

                if (res != null)
                {
                    Session["astromob"] = txtMobileNo.Text.Trim();
                    Session["astrootp"] = otp;

                    string message = $"Dear User, {otp} is your One Time Password(OTP) for Phone Verification.-Vowel Digital";

                    Send_Msg(txtMobileNo.Text.Trim(), message);

                    Response.Redirect("AstroOtpConfirm.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Invalid Mobile No. OR Profile Not Verified!', 'warning');", true);
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