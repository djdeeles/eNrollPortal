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

namespace EnrollPortal.Admin.Kontroller
{
    public partial class EnrollDestek : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnemDurumlariniVer();
            }
        }

        private void OnemDurumlariniVer()
        {
            DropDownListOnemDurumlari.Items.Add(new ListItem("Seçiniz", "0"));
            DropDownListOnemDurumlari.Items.Add(new ListItem("Düşük", "Düşük"));
            DropDownListOnemDurumlari.Items.Add(new ListItem("Orta", "Orta"));
            DropDownListOnemDurumlari.Items.Add(new ListItem("Yüksek", "Yes"));
        }

        private void Temizle()
        {
            OnemDurumlariniVer();
            TextBoxKonu.Text = string.Empty;
            RadEditorMesaj.Content = string.Empty;
        }

        protected void ImageButtonGonder_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int Id = Convert.ToInt16(HttpContext.Current.User.Identity.Name);
                Kullanicilar K = Veriler.Kullanicilar.Where(p => p.Id == Id).FirstOrDefault();
                MailDefinition MD = new MailDefinition();
                ListDictionary LD = new ListDictionary();
                LD.Add("%%ProjeAdi%%", Request.Url.Host);
                LD.Add("%%EPosta%%", K.EPosta);
                LD.Add("%%OnemDurumu%%", DropDownListOnemDurumlari.SelectedValue);
                LD.Add("%%Konu%%", TextBoxKonu.Text);
                LD.Add("%%Mesaj%%", RadEditorMesaj.Content);
                MD.IsBodyHtml = true;
                MD.BodyFileName = "Templates/enrolldestek.htm";
                MD.Subject = "eNroll Destek Talep Formu";
                MD.From = "localhost";
                MailMessage MM = MD.CreateMailMessage("destek@sayajans.com", LD, this);
                MM.BodyEncoding = Encoding.Default;
                MM.SubjectEncoding = Encoding.Default;
                MM.Priority = MailPriority.High;
                SmtpClient SC = new SmtpClient("localhost", 25);
                //smtp.Credentials = new System.Net.NetworkCredential("kultursanat@pendik.bel.tr", "Pendik4918");
                SC.Send(MM);
                MesajKontrol1.Mesaj(true, "Mesajınız gönderildi.");
            }
            catch (Exception Hata)
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu!");
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }
    }
}