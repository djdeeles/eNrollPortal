using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal
{
    public partial class Master : MasterPage
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*var ip = HttpContext.Current.Request.UserHostAddress;
                 var I = Veriler.Ipler.FirstOrDefault(p => p.Ip == ip && p.Durum);
                if (I == null)
                {
                    Response.Redirect("Giris/Giris.aspx");
                }
                else
                {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {*/
                SiteBilgileriniVer();
                KullaniciBilgileriniVer();
                TextBoxArama.Text = "Ne aramıştınız?";
                /*}
                else
                {
                    Response.Redirect("Portal");
                }
                    }*/
            }
        }

        private void SiteBilgileriniVer()
        {
            var dilId = EnrollContext.Current.WorkingLanguage.languageId;
            var sb = Veriler.SiteBilgileri.FirstOrDefault(p => p.DilId == dilId);
            if (sb != null)
            {
                Page.Title = sb.PageTitle;
                Page.MetaDescription = sb.Description;
                Page.MetaKeywords = sb.Keywords;
                LabelSiteBilgileriFooter.Text = sb.Footer;
            }
        }

        private void KullaniciBilgileriniVer()
        {
            var hosgeldinizMesaji = "Hosgeldiniz";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var kullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                var k = Veriler.Kullanicilar.FirstOrDefault(p => p.Id == kullaniciId);
                if (k != null) hosgeldinizMesaji += ", " + k.Ad + " " + k.Soyad;
            }
            ltHosgeldiniz.Text = hosgeldinizMesaji;
        }

        protected void ButtonGoogledaAraClick(object sender, EventArgs e)
        {
            Response.Redirect("http://www.google.com/search?q=" + TextBoxArama.Text + "");
        }

        protected void ButtonSiteIcindeAraClick(object sender, EventArgs e)
        {
            Response.Redirect("Ara.aspx?Code=" + EnrollArama.QueryStringeCevir(TextBoxArama.Text));
        }
    }
}