using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class IletisimFormuKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Temizle();
            }
        }

        private void OnemDurumlariniVer()
        {
            DropDownListOnemDurumlari.Items.Add(new ListItem("Seçiniz", "0"));
            DropDownListOnemDurumlari.Items.Add(new ListItem("Öneri", "Öneri"));
            DropDownListOnemDurumlari.Items.Add(new ListItem("İstek", "İstek"));
            DropDownListOnemDurumlari.Items.Add(new ListItem("Şikayet", "Şikayet"));
        }

        private void Temizle()
        {
            OnemDurumlariniVer();
            TextBoxKonu.Text = string.Empty;
            RadEditorMesaj.Content = string.Empty;
        }

        protected void LinkButtonGonderClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(HttpContext.Current.User.Identity.Name);
                var kullanicilar = Veriler.Kullanicilar.FirstOrDefault(p => p.Id == id);
                var md = new MailDefinition();
                var ld = new ListDictionary();
                if (kullanicilar != null)
                {
                    ld.Add("%%Ad%%", kullanicilar.Ad);
                    ld.Add("%%Soyad%%", kullanicilar.Soyad);
                    ld.Add("%%EPosta%%", kullanicilar.EPosta);
                }
                ld.Add("%%KonuTipi%%", DropDownListOnemDurumlari.SelectedValue);
                ld.Add("%%Konu%%", TextBoxKonu.Text);
                ld.Add("%%Mesaj%%", RadEditorMesaj.Content);
                md.IsBodyHtml = true;
                md.BodyFileName = "Templates/iletisim.htm";
                md.Subject = " - Portal İletişim Formu";
                md.From = "localhost";
                var mm = md.CreateMailMessage(IletisimEPostaVer(), ld, this);
                mm.BodyEncoding = Encoding.Default;
                mm.SubjectEncoding = Encoding.Default;
                mm.Priority = MailPriority.High;
                var sc = new SmtpClient("localhost", 25);
                //smtp.Credentials = new System.Net.NetworkCredential("kultursanat@pendik.bel.tr", "Pendik4918");
                sc.Send(mm);
                MesajKontrol1.Mesaj(true, "Mesajınız gönderildi.");
            }
            catch (Exception hata)
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu!");
                EnrollExceptionManager.ManageException(hata, Request.RawUrl);
            }
        }

        private string IletisimEPostaVer()
        {
            int dilId = EnrollContext.Current.WorkingLanguage.languageId;
            var sb = Veriler.SiteBilgileri.FirstOrDefault(p => p.DilId == dilId);
            return (sb != null ? sb.IletisimEPosta : string.Empty);
        }
    }
}