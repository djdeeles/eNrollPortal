using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Giris
{
    public partial class Giris : Page
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    CookieKontrol();
                    MultiView1.ActiveViewIndex = 0;
                    TextBoxTCNoEPosta.Focus();
                }
                else
                {
                    String a = FormsAuthentication.GetRedirectUrl(HttpContext.Current.User.Identity.Name, false);
                    Response.Redirect(a, false);
                }
            }
        }

        private void CookieKontrol()
        {
            HttpCookie C = Request.Cookies["PortalCookie"];
            if (C != null)
            {
                string TCNoEPosta = C["TCNoEPosta"];
                string Parola = C["Parola"];
                var Kullanici = (from p in Veriler.Kullanicilar
                                 where (p.TCNo == TCNoEPosta || p.EPosta == TCNoEPosta)
                                       && p.Parola == Parola
                                 select new
                                            {
                                                p.Id,
                                                p.EPosta,
                                                p.Parola
                                            }).FirstOrDefault();

                if (Kullanici != null)
                {
                    FormsAuthenticationTicket Ticket =
                        new FormsAuthenticationTicket(
                            1,
                            Kullanici.Id.ToString(),
                            DateTime.Now, DateTime.Now.AddDays(1),
                            false,
                            FormsAuthentication.FormsCookiePath);
                    String encTicket = FormsAuthentication.Encrypt(Ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if (Ticket.IsPersistent)
                    {
                        cookie.Expires = Ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    String a = FormsAuthentication.GetRedirectUrl(Kullanici.EPosta, false);
                    Response.Redirect(a, false);
                }
            }
        }

        protected void ImageButtonGiris_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string TCNoEPosta = TextBoxTCNoEPosta.Text;
                string Parola = TextBoxParola.Text;
                var Kullanici = (from p in Veriler.Kullanicilar
                                 where (p.TCNo == TCNoEPosta || p.EPosta == TCNoEPosta)
                                       && p.Parola == Parola
                                 select new
                                            {
                                                p.Id,
                                                p.EPosta,
                                                p.Parola
                                            }).FirstOrDefault();
                if (Kullanici != null)
                {
                    FormsAuthenticationTicket Ticket =
                        new FormsAuthenticationTicket(
                            1,
                            Kullanici.Id.ToString(),
                            DateTime.Now, DateTime.Now.AddDays(1),
                            false,
                            FormsAuthentication.FormsCookiePath);
                    String encTicket = FormsAuthentication.Encrypt(Ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if (Ticket.IsPersistent)
                    {
                        cookie.Expires = Ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    if (CheckBoxBeniHatirla.Checked)
                    {
                        HttpCookie C = new HttpCookie("PortalCookie");
                        C["TCNoEPosta"] = Kullanici.EPosta;
                        C["Parola"] = Kullanici.Parola;
                        C.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(C);
                    }
                    String a = FormsAuthentication.GetRedirectUrl(Kullanici.EPosta, false);
                    Response.Redirect(a, false);
                }
                else
                {
                    LabelMesaj.Text = "Bilgileriniz geçersizdir";
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                LabelMesaj.Text = "Hata oluştu!";
            }
        }

        protected void ImageButtonParolamiUnuttum_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void ImageButtonGonder2_Click(object sender, ImageClickEventArgs e)
        {
            string EPosta = TextBoxTCNoEPosta1.Text;
            Kullanicilar K =
                Veriler.Kullanicilar.Where(p => p.EPosta == EPosta && p.Durum && p.SilindiMi == false).FirstOrDefault();
            if (K != null)
            {
                try
                {
                    MailDefinition MD = new MailDefinition();
                    ListDictionary LD = new ListDictionary();
                    LD.Add("%%EPosta%%", K.EPosta);
                    LD.Add("%%Parola%%", K.Parola);
                    MD.IsBodyHtml = true;
                    MD.BodyFileName = "Templates/parolamiunuttum.htm";
                    MD.Subject = "Parolamı Unuttum";
                    MD.From = "localhost";
                    MailMessage MM = MD.CreateMailMessage(K.EPosta, LD, this);
                    MM.BodyEncoding = Encoding.Default;
                    MM.SubjectEncoding = Encoding.Default;
                    MM.Priority = MailPriority.High;
                    SmtpClient SC = new SmtpClient("localhost", 25);
                    //smtp.Credentials = new System.Net.NetworkCredential("kultursanat@pendik.bel.tr", "Pendik4918");
                    SC.Send(MM);
                    TextBoxTCNoEPosta1.Text = string.Empty;
                    LabelParolamiUnuttum.Text = "Parolanı e-posta adresinize gönderilmiştir.";
                }
                catch (Exception Hata)
                {
                    LabelParolamiUnuttum.Text = "Hata oluştu!";
                    EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                }
            }
            else
            {
                LabelParolamiUnuttum.Text = "Kullanıcı bulunamadı!";
            }
        }

        protected void ImageButtonGeri_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
}