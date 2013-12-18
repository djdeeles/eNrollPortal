using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class SiteBilgileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Site Bilgileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 1))
                {
                    MultiView1.ActiveViewIndex = 0;
                    SiteBilgileriVer();
                }
                else
                {
                    MultiView1.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
        }

        private void Temizle()
        {
            TextBoxPageTitle.Text = string.Empty;
            TextBoxKeywords.Text = string.Empty;
            TextBoxIletisimEPosta.Text = string.Empty;
            TextBoxDescription.Text = string.Empty;
            RadEditorIcerik.Content = string.Empty;
            HiddenFieldId.Value = string.Empty;
        }

        private void SiteBilgileriVer()
        {
            // Site bilgilerini ver
            Temizle();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            SiteBilgileri SB = Veriler.SiteBilgileri.Where(p => p.DilId == DilId).FirstOrDefault();
            if (SB != null)
            {
                TextBoxPageTitle.Text = SB.PageTitle;
                TextBoxKeywords.Text = SB.Keywords;
                TextBoxIletisimEPosta.Text = SB.IletisimEPosta;
                TextBoxDescription.Text = SB.Description;
                RadEditorIcerik.Content = SB.Footer;
                HiddenFieldId.Value = SB.Id.ToString();
            }
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SiteBilgileri SB;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    SB = Veriler.SiteBilgileri.Where(p => p.Id == Id).First();
                    SB.PageTitle = TextBoxPageTitle.Text;
                    SB.Keywords = TextBoxKeywords.Text;
                    SB.IletisimEPosta = TextBoxIletisimEPosta.Text;
                    SB.Description = TextBoxDescription.Text;
                    SB.Footer = RadEditorIcerik.Content;
                    SB.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    SB.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                    SiteBilgileriVer();
                }
                else
                {
                    // Yeni kayıt
                    SB = new SiteBilgileri();
                    SB.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    SB.PageTitle = TextBoxPageTitle.Text;
                    SB.Keywords = TextBoxKeywords.Text;
                    SB.IletisimEPosta = TextBoxIletisimEPosta.Text;
                    SB.Description = TextBoxDescription.Text;
                    SB.Footer = RadEditorIcerik.Content;
                    SB.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    SB.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToSiteBilgileri(SB);
                    Veriler.SaveChanges();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                    SiteBilgileriVer();
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu!");
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            SiteBilgileriVer();
        }
    }
}