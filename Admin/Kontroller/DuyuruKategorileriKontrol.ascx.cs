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
    public partial class DuyuruKategorileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Duyuru Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 6))
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
            var DuyuruKategorileri = Veriler.DuyuruKategorileri.Where(p => p.DilId == DilId).ToList();
            int Sayi = DuyuruKategorileri.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenFieldId.Value == string.Empty)
            {
                Sayi = DuyuruKategorileri.Count() + 1;
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
            LabelBaslik.Text = "Duyuru Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DuyuruKategorileri DK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelleme
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    DK = Veriler.DuyuruKategorileri.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(DK.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    DK.KategoriAdi = TextBoxKategoriAdi.Text;
                    DK.SiraNo = YeniSiraNo;
                    DK.Durum = CheckBoxDurum.Checked;
                    DK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DK.DuzenlemeTarihi = DateTime.Now;
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
                    DK = new DuyuruKategorileri();
                    DK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    DK.KategoriAdi = TextBoxKategoriAdi.Text;
                    DK.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    DK.Durum = CheckBoxDurum.Checked;
                    DK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DK.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToDuyuruKategorileri(DK);
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
            // Sıra nolarını güncelle
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DKS = Veriler.DuyuruKategorileri.Where(p => p.SiraNo >= SiraNo && p.DilId == DilId).ToList();
            foreach (DuyuruKategorileri DK in DKS)
            {
                DK.SiraNo = DK.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            // sira nolarını güncelle
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (EskiSiraNo != YeniSiraNo)
            {
                List<DuyuruKategorileri> DKS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    DKS =
                        Veriler.DuyuruKategorileri.Where(
                            p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (DuyuruKategorileri DK in DKS)
                    {
                        DK.SiraNo = DK.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    DKS =
                        Veriler.DuyuruKategorileri.Where(
                            p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (DuyuruKategorileri DK in DKS)
                    {
                        DK.SiraNo = DK.SiraNo - 1;
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
                DuyuruKategorileri DK = Veriler.DuyuruKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(DK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                DuyuruKategorileri DK = Veriler.DuyuruKategorileri.Where(p => p.Id == Id).First();
                KategoriSil(DK);
                Veriler.DuyuruKategorileri.DeleteObject(DK);
                Veriler.SaveChanges();
                var HKList = Veriler.DuyuruKategorileri.Where(p => p.SiraNo >= DK.SiraNo && p.DilId == DilId).ToList();
                foreach (DuyuruKategorileri HKK in HKList)
                {
                    DuyuruKategorileri DuyuruKategorileri =
                        Veriler.DuyuruKategorileri.Where(p => p.Id == HKK.Id).First();
                    DuyuruKategorileri.SiraNo = DuyuruKategorileri.SiraNo - 1;
                    Veriler.SaveChanges();
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void KategoriSil(DuyuruKategorileri DK)
        {
            // kategorileri sil
            var DTList = Veriler.DuyurularTablosu.Where(p => p.KategoriId == DK.Id).ToList();
            foreach (DuyurularTablosu DT in DTList)
            {
                Duyurular D = Veriler.Duyurular.Where(p => p.Id == DT.DuyuruId).First();
                var DTList2 = Veriler.DuyurularTablosu.Where(p => p.DuyuruId == D.Id).ToList();
                if (DTList2.Count == 1)
                {
                    GorselSil(D.Gorsel1);
                    GorselSil(D.GorselThumbnail1);
                    GorselSil(D.Gorsel2);
                    GorselSil(D.GorselThumbnail2);
                    GorselSil(D.Gorsel3);
                    GorselSil(D.GorselThumbnail3);
                    GorselSil(D.Gorsel4);
                    GorselSil(D.GorselThumbnail4);
                    Veriler.Duyurular.DeleteObject(D);
                    Veriler.SaveChanges();
                }
                Veriler.DuyurularTablosu.DeleteObject(DT);
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

        private void Guncelle(DuyuruKategorileri DK)
        {
            // Güncelle
            TextBoxKategoriAdi.Text = DK.KategoriAdi;
            CheckBoxDurum.Checked = DK.Durum;
            HiddenFieldId.Value = DK.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = DK.SiraNo.ToString();
            LabelBaslik.Text = "Duyuru Kategori Ekle Düzenle";
        }
    }
}