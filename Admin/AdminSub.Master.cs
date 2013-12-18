using System;
using System.Web.UI;

namespace EnrollPortal.Admin
{
    public partial class AdminSub : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLocation.Text = Session["currentPath"].ToString();
        }
    }
}