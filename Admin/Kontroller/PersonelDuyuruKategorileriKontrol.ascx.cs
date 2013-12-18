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
    public partial class PersonelDuyuruKategorileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Personel Duyuru Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 7))
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
            var PDK = Veriler.PersonelDuyuruKategorileri.Where(p => p.DilId == DilId).ToList();
            int Sayi = PDK.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenFieldId.Value == string.Empty)
            {
                Sayi = PDK.Count() + 1;
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
            LabelBaslik.Text = "Personel Duyuru Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                PersonelDuyuruKategorileri PDK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    PDK = Veriler.PersonelDuyuruKategorileri.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(PDK.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    PDK.KategoriAdi = TextBoxKategoriAdi.Text;
                    PDK.SiraNo = YeniSiraNo;
                    PDK.Durum = CheckBoxDurum.Checked;
                    PDK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    PDK.DuzenlemeTarihi = DateTime.Now;
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
                    PDK = new PersonelDuyuruKategorileri();
                    PDK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    PDK.KategoriAdi = TextBoxKategoriAdi.Text;
                    PDK.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    PDK.Durum = CheckBoxDurum.Checked;
                    PDK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    PDK.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToPersonelDuyuruKategorileri(PDK);
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
            var PDKS = Veriler.PersonelDuyuruKategorileri.Where(p => p.SiraNo >= SiraNo && p.DilId == DilId).ToList();
            foreach (PersonelDuyuruKategorileri PDK in PDKS)
            {
                PDK.SiraNo = PDK.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            // Sıra nolarını güncelle
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (EskiSiraNo != YeniSiraNo)
            {
                List<PersonelDuyuruKategorileri> PDKS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    PDKS =
                        Veriler.PersonelDuyuruKategorileri.Where(
                            p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (PersonelDuyuruKategorileri PDK in PDKS)
                    {
                        PDK.SiraNo = PDK.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    PDKS =
                        Veriler.PersonelDuyuruKategorileri.Where(
                            p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (PersonelDuyuruKategorileri PDK in PDKS)
                    {
                        PDK.SiraNo = PDK.SiraNo - 1;
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
                PersonelDuyuruKategorileri PDK = Veriler.PersonelDuyuruKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(PDK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                PersonelDuyuruKategorileri PDK = Veriler.PersonelDuyuruKategorileri.Where(p => p.Id == Id).First();
                KategoriSil(PDK);
                Veriler.PersonelDuyuruKategorileri.DeleteObject(PDK);
                Veriler.SaveChanges();
                var PDKList =
                    Veriler.PersonelDuyuruKategorileri.Where(p => p.SiraNo >= PDK.SiraNo && p.DilId == DilId).ToList();
                foreach (PersonelDuyuruKategorileri PDKK in PDKList)
                {
                    PersonelDuyuruKategorileri PersonelDuyuruKategorileri =
                        Veriler.PersonelDuyuruKategorileri.Where(p => p.Id == PDKK.Id).First();
                    PersonelDuyuruKategorileri.SiraNo = PersonelDuyuruKategorileri.SiraNo - 1;
                    Veriler.SaveChanges();
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void KategoriSil(PersonelDuyuruKategorileri PDK)
        {
            // Kategorileri sil
            var PDTList = Veriler.PersonelDuyurularTablosu.Where(p => p.KategoriId == PDK.Id).ToList();
            foreach (PersonelDuyurularTablosu PDT in PDTList)
            {
                PersonelDuyurular PD = Veriler.PersonelDuyurular.Where(p => p.Id == PDT.DuyuruId).First();
                var PDTList2 = Veriler.PersonelDuyurularTablosu.Where(p => p.DuyuruId == PD.Id).ToList();
                if (PDTList2.Count == 1)
                {
                    GorselSil(PD.Gorsel1);
                    GorselSil(PD.GorselThumbnail1);
                    GorselSil(PD.Gorsel2);
                    GorselSil(PD.GorselThumbnail2);
                    GorselSil(PD.Gorsel3);
                    GorselSil(PD.GorselThumbnail3);
                    GorselSil(PD.Gorsel4);
                    GorselSil(PD.GorselThumbnail4);
                    Veriler.PersonelDuyurular.DeleteObject(PD);
                    Veriler.SaveChanges();
                }
                Veriler.PersonelDuyurularTablosu.DeleteObject(PDT);
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

        private void Guncelle(PersonelDuyuruKategorileri PDK)
        {
            // Güncelle
            TextBoxKategoriAdi.Text = PDK.KategoriAdi;
            CheckBoxDurum.Checked = PDK.Durum;
            HiddenFieldId.Value = PDK.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = PDK.SiraNo.ToString();
            LabelBaslik.Text = "Personel Duyuru Kategori Düzenle";
        }
    }
}