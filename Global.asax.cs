using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AstroApp.App_Code;

namespace AstroApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            Log.LogError(exception);

            Server.ClearError();

            string previousPageUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : string.Empty;
            Session["PreviousPageUrl"] = previousPageUrl;

            Response.Redirect("~/Error.aspx?prevPage=" + Server.UrlEncode(previousPageUrl));
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}