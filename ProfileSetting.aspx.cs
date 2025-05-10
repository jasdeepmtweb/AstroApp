using System;
using System.IO;
using System.Web.UI;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class ProfileSetting : System.Web.UI.Page
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
                GetUserData();
            }
        }

        private void GetUserData()
        {
            UserModel oUserModel = Session["usr"] as UserModel;

            txtBirthTime.Text = oUserModel.birth_time.ToString();
            txtDateOfBirth.Text = oUserModel.date_of_birth.ToString("yyyy-MM-dd");
            txtEmailId.Text = oUserModel.email;
            txtName.Text = oUserModel.name;
            txtPlaceOfBirth.Text = oUserModel.birth_place;
            lblMobileNo.Text = oUserModel.phone;
            lblName.Text = oUserModel.name;
            txtState.Text = oUserModel.state;
            txtAddress.Text = oUserModel.address;
            if (!oUserModel.profile_picture.Equals("-"))
            {
                ImgUser.ImageUrl = $"Uploads/UserPics/{oUserModel.profile_picture}";
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string uploadPath = Server.MapPath("~/Uploads/UserPics/");
            if (Page.IsValid)
            {
                try
                {
                    UserModel oUserModel = Session["usr"] as UserModel;
                    string adhaarFront = "-";
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
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", "Swal.fire('Error!', 'Select Profile Pic', 'warning');", true);
                        //return;
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("p_name", txtName.Text);
                    parameters.Add("p_email", txtEmailId.Text);
                    parameters.Add("p_date_of_birth", txtDateOfBirth.Text);
                    parameters.Add("p_birth_place", txtPlaceOfBirth.Text);
                    parameters.Add("p_birth_time", txtBirthTime.Text);
                    parameters.Add("p_profile_picture", adhaarFront);
                    parameters.Add("p_gender", RadioListGender.SelectedItem.Text);
                    parameters.Add("p_address", txtAddress.Text);
                    parameters.Add("p_state", txtState.Text);
                    parameters.Add("p_user_id", oUserModel.user_id);

                    oDataAccess.ExecuteSPDynamic("sp_update_user_profile", parameters);

                    oUserModel.name = txtName.Text;
                    oUserModel.email = txtEmailId.Text;
                    oUserModel.date_of_birth = Convert.ToDateTime(txtDateOfBirth.Text);
                    oUserModel.birth_place = txtPlaceOfBirth.Text;
                    oUserModel.birth_time = TimeSpan.Parse(txtBirthTime.Text);
                    oUserModel.profile_picture = adhaarFront;
                    oUserModel.gender = RadioListGender.SelectedItem.Text;
                    oUserModel.address = txtAddress.Text;
                    oUserModel.state = txtState.Text;

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
                window.location.href = 'ProfileSetting.aspx';
            }
        });
    ";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showSweetAlert", script, true);
        }
    }
}