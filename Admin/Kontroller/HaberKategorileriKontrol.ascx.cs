using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class HaberKategorileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Haber Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 5))
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
            SiraNolariniVer();
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void SiraNolariniVer()
        {
            // Sıra nolarını ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HaberKategorileri = Veriler.HaberKategorileri.Where(p => p.DilId == DilId).ToList();
            int Sayi = HaberKategorileri.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenFieldId.Value == string.Empty)
            {
                Sayi = HaberKategorileri.Count() + 1;
            }
            for (int i = 1; i <= Sayi; i++)
            {
                DropDownListSiraNolari.Items.Add(i.ToString());
            }
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Haber Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                HaberKategorileri HK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    HK = Veriler.HaberKategorileri.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(HK.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    HK.KategoriAdi = TextBoxKategoriAdi.Text;
                    HK.SiraNo = YeniSiraNo;
                    HK.Durum = CheckBoxDurum.Checked;
                    HK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    HK.DuzenlemeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(EskiSiraNo, YeniSiraNo);
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    // Yeni kayıt
                    HK = new HaberKategorileri();
                    HK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    HK.KategoriAdi = TextBoxKategoriAdi.Text;
                    HK.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    HK.Durum = CheckBoxDurum.Checked;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    HK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    HK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToHaberKategorileri(HK);
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

        private void SiraNolariniGuncelle(int SiraNo)
        {
            // Sıra nolarını Güncelle
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HKS = Veriler.HaberKategorileri.Where(p => p.SiraNo >= SiraNo && p.DilId == DilId).ToList();
            foreach (HaberKategorileri HK in HKS)
            {
                HK.SiraNo = HK.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            // Sıra nolarını güncelle
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (EskiSiraNo != YeniSiraNo)
            {
                List<HaberKategorileri> HKS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    HKS =
                        Veriler.HaberKategorileri.Where(
                            p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (HaberKategorileri HK in HKS)
                    {
                        HK.SiraNo = HK.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    HKS =
                        Veriler.HaberKategorileri.Where(
                            p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (HaberKategorileri HK in HKS)
                    {
                        HK.SiraNo = HK.SiraNo - 1;
                        Veriler.SaveChanges();
                    }
                }
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
                HaberKategorileri HK = Veriler.HaberKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(HK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                HaberKategorileri HK = Veriler.HaberKategorileri.Where(p => p.Id == Id).First();
                KategoriSil(HK);
                Veriler.HaberKategorileri.DeleteObject(HK);
                Veriler.SaveChanges();
                var HKList = Veriler.HaberKategorileri.Where(p => p.SiraNo >= HK.SiraNo && p.DilId == DilId).ToList();
                foreach (HaberKategorileri HKK in HKList)
                {
                    HaberKategorileri HaberKategorileri = Veriler.HaberKategorileri.Where(p => p.Id == HKK.Id).First();
                    HaberKategorileri.SiraNo = HaberKategorileri.SiraNo - 1;
                    Veriler.SaveChanges();
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void KategoriSil(HaberKategorileri HK)
        {
            // Kategori sil
            var HTList = Veriler.HaberlerTablosu.Where(p => p.KategoriId == HK.Id).ToList();
            foreach (HaberlerTablosu HT in HTList)
            {
                Haberler H = Veriler.Haberler.Where(p => p.Id == HT.HaberId).First();
                var HTList2 = Veriler.HaberlerTablosu.Where(p => p.HaberId == H.Id).ToList();
                if (HTList2.Count == 1)
                {
                    GorselSil(H.Gorsel1);
                    GorselSil(H.GorselThumbnail1);
                    GorselSil(H.Gorsel2);
                    GorselSil(H.GorselThumbnail2);
                    GorselSil(H.Gorsel3);
                    GorselSil(H.GorselThumbnail3);
                    GorselSil(H.Gorsel4);
                    GorselSil(H.GorselThumbnail4);
                    Veriler.Haberler.DeleteObject(H);
                    Veriler.SaveChanges();
                }
                Veriler.HaberlerTablosu.DeleteObject(HT);
                Veriler.SaveChanges();
            }
        }

        private void GorselSil(string Resim)
        {
            // Görsel sil
            if (File.Exists(Server.MapPath(Resim)))
            {
                File.Delete(Server.MapPath(Resim));
            }
        }

        private void Guncelle(HaberKategorileri HK)
        {
            // Güncelle
            TextBoxKategoriAdi.Text = HK.KategoriAdi;
            CheckBoxDurum.Checked = HK.Durum;
            HiddenFieldId.Value = HK.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = HK.SiraNo.ToString();
            LabelBaslik.Text = "Haber Kategori Düzenle";
        }
    }
}