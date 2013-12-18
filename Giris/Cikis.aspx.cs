using System;
using System.Web.Security;
using System.Web.UI;

namespace EnrollPortal.Giris
{
    public partial class Cikis : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cookies["PortalCookie"].Expires = DateTime.Now.AddMinutes(-1);
            Response.Redirect("../Default.aspx");
        }
    }
}