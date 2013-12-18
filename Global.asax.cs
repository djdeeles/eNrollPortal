using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using EnrollPortal.Kutuphaneler;

namespace EnrollPortal
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
        }

        private void Application_End(object sender, EventArgs e)
        {
        }

        private void Application_Error(object sender, EventArgs e)
        {
        }

        private void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("Id", HttpContext.Current.Session.SessionID);
            EnrollContext enContext = new EnrollContext();
            HttpContext.Current.Session.Add("EnrollContext", enContext);
            HttpContext.Current.Session.Add("WorkingLanguageId", enContext.WorkingLanguage.languageId.ToString());
            HttpContext.Current.Session.Add("DataLanguageId", enContext.DataLanguage.languageId.ToString());
            HttpContext.Current.Session.Add("currentPath", "Yönetim Paneli");
        }

        private void Session_End(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        private void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        var roles = userData.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                    }
                }
            }
        }
    }
}