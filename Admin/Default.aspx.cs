using System;
using System.Web.UI;

namespace EnrollPortal.Admin
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["currentPath"] = "Yönetim Paneli";
            }
        }
    }
}