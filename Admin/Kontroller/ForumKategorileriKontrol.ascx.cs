using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class ForumKategorileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Forum Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 12))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void Temizle()
        {
            TextBoxKategoriAdi.Text = string.Empty;
            TextBoxAciklama.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Forum Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ForumKategorileri FK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    FK = Veriler.ForumKategorileri.Where(p => p.Id == Id).First();
                    FK.KategoriAdi = TextBoxKategoriAdi.Text;
                    FK.Aciklama = TextBoxAciklama.Text;
                    FK.Durum = CheckBoxDurum.Checked;
                    FK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FK.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    FK = new ForumKategorileri();
                    FK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    FK.KategoriAdi = TextBoxKategoriAdi.Text;
                    FK.Aciklama = TextBoxAciklama.Text;
                    FK.Durum = CheckBoxDurum.Checked;
                    FK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToForumKategorileri(FK);
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                // Güncelle
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                ForumKategorileri FK = Veriler.ForumKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(FK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                ForumKategorileri FK = Veriler.ForumKategorileri.Where(p => p.Id == Id).First();
                Veriler.ForumKategorileri.DeleteObject(FK);
                Veriler.SaveChanges();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(ForumKategorileri FK)
        {
            // Güncelle
            TextBoxKategoriAdi.Text = FK.KategoriAdi;
            TextBoxAciklama.Text = FK.Aciklama;
            CheckBoxDurum.Checked = FK.Durum;
            HiddenFieldId.Value = FK.Id.ToString();
            LabelBaslik.Text = "Forum Kategori Düzenle";
        }
    }
}