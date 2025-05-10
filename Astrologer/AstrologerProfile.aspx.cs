using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp.Astrologer
{
    public partial class AstrologerProfile : System.Web.UI.Page
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
                GetAllSpecs();
                GetUserProfile();
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
        private void GetUserProfile()
        {
            AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;

            lblName.Text = oAstrologerModel.name;
            lblMobileNo.Text = oAstrologerModel.phone;
            txtBio.Text = oAstrologerModel.bio;
            txtEmailId.Text = oAstrologerModel.email;
            txtExperience.Text = oAstrologerModel.experience_years.ToString();
            txtName.Text = oAstrologerModel.name;
            //txtRatePerMinute.Text = oAstrologerModel.per_minute_rate.ToString();

            ImgUser.ImageUrl = $"../Uploads/AstrologerPics/{oAstrologerModel.profile_picture}";
            lblImage.Text = oAstrologerModel.profile_picture;

            string[] specs = oAstrologerModel.specializations.Split(',');
            for (int i = 0; i < specs.Length; i++)
            {
                foreach (ListItem item in CheckSpecialization.Items)
                {
                    if (item.Text.Equals(specs[i]))
                    {
                        item.Selected = true;
                    }
                }
            }

            string[] languages = oAstrologerModel.languages_known.Split(',');
            for (int i = 0; i < languages.Length; i++)
            {
                foreach (ListItem item in CheckListLanguages.Items)
                {
                    if (item.Text.Equals(languages[i]))
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    AstrologerModel oAstrologerModel = Session["astrousr"] as AstrologerModel;

                    string uploadPath = Server.MapPath("~/Uploads/AstrologerPics/");

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
                    if (FilePic.HasFile)
                    {
                        string extension = Path.GetExtension(FilePic.FileName);
                        if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
                        {
                            adhaarFront = ImageCompressor.SaveFile(FilePic.FileName, FilePic.FileBytes, uploadPath);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Only Image is Allowed', 'warning');", true);
                            return;
                        }
                    }
                    else
                    {
                        adhaarFront = lblImage.Text;
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
                    parameters.Add("p_profile_picture", adhaarFront);
                    parameters.Add("p_bio", txtBio.Text);
                    parameters.Add("p_experience_years", txtExperience.Text);
                    parameters.Add("p_specializations", selectedSpecialization);
                    parameters.Add("p_languages_known", selectedLanguages);
                    parameters.Add("p_per_minute_rate", 0);
                    parameters.Add("p_astrologer_id", oAstrologerModel.astrologer_id);

                    oDataAccess.ExecuteSPDynamic("sp_update_astrologer_profile", parameters);

                    oAstrologerModel.name = txtName.Text;
                    oAstrologerModel.email = txtEmailId.Text;
                    oAstrologerModel.profile_picture = adhaarFront;
                    oAstrologerModel.bio = txtBio.Text;
                    oAstrologerModel.experience_years = Convert.ToInt32(txtExperience.Text);
                    oAstrologerModel.specializations = selectedSpecialization;
                    oAstrologerModel.languages_known = selectedLanguages;
                    //oAstrologerModel.per_minute_rate = Convert.ToInt32(txtRatePerMinute.Text);

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
            text: 'Profile Updated',
            icon: 'success',
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                window.location.href = 'AstrologerProfile.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
    }
}