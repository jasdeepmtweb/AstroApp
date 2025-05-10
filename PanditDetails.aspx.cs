using System;
using System.Web.UI.WebControls;
using AstroApp.App_Code;
using Dapper;

namespace AstroApp
{
    public partial class PanditDetails : System.Web.UI.Page
    {
        DataAccess oDataAccess = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            if (!IsPostBack)
            {
                GetAstrologerDetail(Request.QueryString["id"]);
            }
        }

        private void GetAstrologerDetail(string astrologerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_astrologer_id", astrologerId);

            var res = oDataAccess.QuerySingleOrDefaultSPDynamic<AstrologerModel>("sp_get_astrologer_by_id", parameters);
            if (res != null)
            {
                lblBio.Text = res.bio;
                lblExp.Text = res.experience_years.ToString();
                lblLanguageKnown.Text = res.languages_known;
                lblName.Text = res.name;
                lblSpecializations.Text = res.specializations;

                ImgAstrologer.ImageUrl = $"Uploads/AstrologerPics/{res.profile_picture}";

                if (res.verification_status.Equals("verified"))
                {
                    ImgVerfied.ImageUrl = "img/icon-check.png";
                }
                else
                {
                    ImgVerfied.ImageUrl = "img/icon-warning.png";
                }
                if (res.is_online)
                {
                    LinkChat.Enabled = true;
                    LinkChat.CssClass = "btn m-1 btn-outline-success px-4 rounded-3";
                }
                else
                {
                    LinkChat.Enabled = false;
                    LinkChat.CssClass = "btn m-1 btn-outline-danger px-4 rounded-3";
                }
                LinkChat.CommandArgument = res.astrologer_id.ToString();
            }
        }

        protected void LinkChat_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            Response.Redirect($"Chat.aspx?id={btn.CommandArgument}");
        }
    }
}