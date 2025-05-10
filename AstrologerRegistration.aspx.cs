using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class AstrologerRegistration : System.Web.UI.Page
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllSpecs();
            }
        }

        private void GetAllSpecs()
        {
            CheckSpecialization.Items.Clear();

            var res = oDataAccess.QuerySPDynamic<SpecializationModel>("sp_getall_specialization");

            CheckSpecialization.DataSource = res;
            CheckSpecialization.DataValueField = "category_id";
            CheckSpecialization.DataTextField = "name";
            CheckSpecialization.DataBind();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string uploadPath = Server.MapPath("~/Uploads/AstrologerPics/");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    if (CheckListLanguages.SelectedItem == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Select Atleast One Language', 'warning');", true);
                        return;
                    }
                    if (CheckSpecialization.SelectedItem == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Select Atleast One Specialization', 'warning');", true);
                        return;
                    }

                    string adhaarFront = "";
                    if (FileProfilePic.HasFile)
                    {
                        string extension = Path.GetExtension(FileProfilePic.FileName);
                        if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
                        {
                            adhaarFront = ImageCompressor.SaveFile(FileProfilePic.FileName, FileProfilePic.FileBytes, uploadPath);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Only Image is Allowed', 'warning');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Select Profile Pic', 'warning');", true);
                        return;
                    }

                    List<string> selectedItems = new List<string>();
                    foreach (ListItem item in CheckListLanguages.Items)
                    {
                        if (item.Selected)
                        {
                            selectedItems.Add(item.Value);
                        }
                    }
                    string selectedLanguages = string.Join(",", selectedItems);

                    List<string> selectedSpecs = new List<string>();
                    foreach (ListItem item in CheckSpecialization.Items)
                    {
                        if (item.Selected)
                        {
                            selectedSpecs.Add(item.Text);
                        }
                    }
                    string selectedSpecialization = string.Join(",", selectedSpecs);

                    var parameters = new DynamicParameters();
                    parameters.Add("p_name", txtName.Text);
                    parameters.Add("p_email", txtEmailId.Text);
                    parameters.Add("p_phone", txtMobileNo.Text);
                    parameters.Add("p_password_hash", "123456");
                    parameters.Add("p_profile_picture", adhaarFront);
                    parameters.Add("p_bio", "-");
                    parameters.Add("p_experience_years", txtExperienceYears.Text);
                    parameters.Add("p_specializations", selectedSpecialization);
                    parameters.Add("p_languages_known", selectedLanguages);
                    parameters.Add("p_hourly_rate", 0);
                    parameters.Add("p_per_minute_rate", 0);
                    parameters.Add("p_rating", 0);
                    parameters.Add("p_total_consultations", 0);
                    parameters.Add("p_is_online", 0);
                    parameters.Add("p_verification_status", "pending");
                    parameters.Add("p_created_at", indianTime);
                    parameters.Add("p_updated_at", indianTime);

                    oDataAccess.ExecuteSPDynamic("sp_insert_astrologer", parameters);

                    ShowSweetAlert();
                }
                catch (Exception ex)
                {
                    Log.LogError(ex);
                    if (ex.Message.Contains("Duplicate"))
                    {
                        if (ex.Message.Contains("phone"))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Mobile No. Already Exists!', 'warning');", true);
                        }
                        if (ex.Message.Contains("email"))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Email Id Already Exists!', 'warning');", true);
                        }
                    }
                }
            }
        }
        protected void ShowSweetAlert()
        {
            // Execute JavaScript code to show the SweetAlert and redirect after it is dismissed
            string script = @"
        Swal.fire({
            title: 'Success!',
            text: 'Registration Successfull',
            icon: 'success',
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                window.location.href = 'AstrologerRegistration.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
    }
}