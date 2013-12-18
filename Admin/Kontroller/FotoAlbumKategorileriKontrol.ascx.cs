using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class FotoAlbumKategorileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Albüm Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 10))
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
            TextBoxFotoAlbumKategoriAdi.Text = string.Empty;
            TextBoxAciklama.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            CheckBoxPersonelKategorisiMi.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Foto Albüm Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                FotoAlbumKategorileri FAK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    FAK = Veriler.FotoAlbumKategorileri.Where(p => p.Id == Id).First();
                    FAK.FotoAlbumKategoriAdi = TextBoxFotoAlbumKategoriAdi.Text;
                    FAK.Aciklama = TextBoxAciklama.Text;
                    FAK.Durum = CheckBoxDurum.Checked;
                    FAK.PersonelKategorisiMi = CheckBoxPersonelKategorisiMi.Checked;
                    FAK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FAK.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    FAK = new FotoAlbumKategorileri();
                    FAK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    FAK.FotoAlbumKategoriAdi = TextBoxFotoAlbumKategoriAdi.Text;
                    FAK.Aciklama = TextBoxAciklama.Text;
                    FAK.Durum = CheckBoxDurum.Checked;
                    FAK.PersonelKategorisiMi = CheckBoxPersonelKategorisiMi.Checked;
                    FAK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FAK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToFotoAlbumKategorileri(FAK);
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
                FotoAlbumKategorileri FAK = Veriler.FotoAlbumKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(FAK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                FotoAlbumKategorileri FAK = Veriler.FotoAlbumKategorileri.Where(p => p.Id == Id).First();
                var FAList = Veriler.FotoAlbumler.Where(p => p.FotoAlbumKategoriId == FAK.Id).ToList();
                foreach (FotoAlbumler FA in FAList)
                {
                    FotoAlbumler FASil = Veriler.FotoAlbumler.Where(p => p.Id == FA.Id).First();
                    if (!string.IsNullOrEmpty(FASil.Gorsel))
                    {
                        GorselSil(FASil.Gorsel);
                    }
                    if (!string.IsNullOrEmpty(FASil.GorselThumbnail))
                    {
                        GorselSil(FASil.GorselThumbnail);
                    }
                    var FAGList = Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == FA.Id).ToList();
                    foreach (FotoAlbumGorselleri FAG in FAGList)
                    {
                        FotoAlbumGorselleri FAGSil = Veriler.FotoAlbumGorselleri.Where(p => p.Id == FAG.Id).First();
                        if (!string.IsNullOrEmpty(FAGSil.Gorsel))
                        {
                            GorselSil(FAGSil.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(FAGSil.GorselThumbnail))
                        {
                            GorselSil(FAGSil.GorselThumbnail);
                        }
                        Veriler.FotoAlbumGorselleri.DeleteObject(FAGSil);
                        Veriler.SaveChanges();
                    }
                    Veriler.FotoAlbumler.DeleteObject(FASil);
                    Veriler.SaveChanges();
                }
                Veriler.FotoAlbumKategorileri.DeleteObject(FAK);
                Veriler.SaveChanges();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void GorselSil(string Resim)
        {
            // Sil
            if (File.Exists(Server.MapPath(Resim)))
            {
                File.Delete(Server.MapPath(Resim));
            }
        }

        private void Guncelle(FotoAlbumKategorileri FAK)
        {
            // Sil
            TextBoxFotoAlbumKategoriAdi.Text = FAK.FotoAlbumKategoriAdi;
            TextBoxAciklama.Text = FAK.Aciklama;
            CheckBoxDurum.Checked = FAK.Durum;
            CheckBoxPersonelKategorisiMi.Checked = FAK.PersonelKategorisiMi;
            HiddenFieldId.Value = FAK.Id.ToString();
            LabelBaslik.Text = "Foto Albüm Kategori Düzenle";
        }
    }
}