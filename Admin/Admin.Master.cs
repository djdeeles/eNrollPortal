using System;
using System.Linq;
using System.Web.UI;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin
{
    public partial class Admin : MasterPage
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LokasyonlariVer();
                lblLocation.Text = Session["currentPath"].ToString();
                Page.Title = "eNroll Web Çözümleri";
            }
        }

        private void LokasyonlariVer()
        {
            var Lokasyolar = Veriler.MenuLokasyonlari.ToList();
            RepeaterLokasyonlar.DataSource = Lokasyolar;
            RepeaterLokasyonlar.DataBind();
        }
    }
}