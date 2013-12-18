using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Admin.Kontroller
{
    public partial class EtkinlikKategorileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Etkinlik Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 9))
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
            var EtkinlikKategorileri = Veriler.EtkinlikKategorileri.Where(p => p.DilId == DilId).ToList();
            int Sayi = EtkinlikKategorileri.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenFieldId.Value == string.Empty)
            {
                Sayi = EtkinlikKategorileri.Count() + 1;
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
            LabelBaslik.Text = "Etkinlik Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                EtkinlikKategorileri EK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    EK = Veriler.EtkinlikKategorileri.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(EK.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    EK.KategoriAdi = TextBoxKategoriAdi.Text;
                    EK.SiraNo = YeniSiraNo;
                    EK.Durum = CheckBoxDurum.Checked;
                    EK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    EK.DuzenlemeTarihi = DateTime.Now;
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
                    EK = new EtkinlikKategorileri();
                    EK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    EK.KategoriAdi = TextBoxKategoriAdi.Text;
                    EK.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    EK.Durum = CheckBoxDurum.Checked;
                    EK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    EK.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToEtkinlikKategorileri(EK);
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
            var HKS = Veriler.HaberKategorileri.Where(p => p.SiraNo >= SiraNo && p.DilId == DilId).ToList();
            foreach (HaberKategorileri HK in HKS)
            {
                HK.SiraNo = HK.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            // Sira nolarını güncelle
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (EskiSiraNo != YeniSiraNo)
            {
                List<EtkinlikKategorileri> EKS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    EKS =
                        Veriler.EtkinlikKategorileri.Where(
                            p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (EtkinlikKategorileri EK in EKS)
                    {
                        EK.SiraNo = EK.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    EKS =
                        Veriler.EtkinlikKategorileri.Where(
                            p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (EtkinlikKategorileri EK in EKS)
                    {
                        EK.SiraNo = EK.SiraNo - 1;
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
                EtkinlikKategorileri EK = Veriler.EtkinlikKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(EK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                EtkinlikKategorileri EK = Veriler.EtkinlikKategorileri.Where(p => p.Id == Id).First();
                Veriler.EtkinlikKategorileri.DeleteObject(EK);
                Veriler.SaveChanges();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(EtkinlikKategorileri EK)
        {
            // Güncelle
            TextBoxKategoriAdi.Text = EK.KategoriAdi;
            CheckBoxDurum.Checked = EK.Durum;
            HiddenFieldId.Value = EK.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = EK.SiraNo.ToString();
            LabelBaslik.Text = "Etkinlik Kategori Düzenle";
        }
    }
}