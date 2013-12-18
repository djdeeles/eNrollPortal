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
    public partial class IlanKategorileriKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "İlan Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 8))
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
            var IK = Veriler.IlanKategorileri.Where(p => p.DilId == DilId).ToList();
            int Sayi = IK.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenFieldId.Value == string.Empty)
            {
                Sayi = IK.Count() + 1;
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
            LabelBaslik.Text = "İlan Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                IlanKategorileri IK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    // Güncelle
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    IK = Veriler.IlanKategorileri.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(IK.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    IK.KategoriAdi = TextBoxKategoriAdi.Text;
                    IK.SiraNo = YeniSiraNo;
                    IK.Durum = CheckBoxDurum.Checked;
                    IK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    IK.DuzenlemeTarihi = DateTime.Now;
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
                    IK = new IlanKategorileri();
                    IK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    IK.KategoriAdi = TextBoxKategoriAdi.Text;
                    IK.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    IK.Durum = CheckBoxDurum.Checked;
                    IK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    IK.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToIlanKategorileri(IK);
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
            var IKS = Veriler.IlanKategorileri.Where(p => p.SiraNo >= SiraNo && p.DilId == DilId).ToList();
            foreach (IlanKategorileri IK in IKS)
            {
                IK.SiraNo = IK.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            // Sıra nolarını güncelle
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (EskiSiraNo != YeniSiraNo)
            {
                List<IlanKategorileri> IKS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    IKS =
                        Veriler.IlanKategorileri.Where(
                            p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (IlanKategorileri IK in IKS)
                    {
                        IK.SiraNo = IK.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    IKS =
                        Veriler.IlanKategorileri.Where(
                            p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (IlanKategorileri IK in IKS)
                    {
                        IK.SiraNo = IK.SiraNo - 1;
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
                IlanKategorileri IK = Veriler.IlanKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(IK);
            }
            else if (e.CommandName == "Sil")
            {
                // Sil
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                IlanKategorileri IK = Veriler.IlanKategorileri.Where(p => p.Id == Id).First();
                KategoriSil(IK);
                Veriler.IlanKategorileri.DeleteObject(IK);
                Veriler.SaveChanges();
                var IKList = Veriler.IlanKategorileri.Where(p => p.SiraNo >= IK.SiraNo && p.DilId == DilId).ToList();
                foreach (IlanKategorileri IKK in IKList)
                {
                    IlanKategorileri IlanKategorileri = Veriler.IlanKategorileri.Where(p => p.Id == IKK.Id).First();
                    IlanKategorileri.SiraNo = IlanKategorileri.SiraNo - 1;
                    Veriler.SaveChanges();
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void KategoriSil(IlanKategorileri IK)
        {
            // Kategori sil
            var ITList = Veriler.IlanlarTablosu.Where(p => p.KategoriId == IK.Id).ToList();
            foreach (IlanlarTablosu IT in ITList)
            {
                Ilanlar I = Veriler.Ilanlar.Where(p => p.Id == IT.IlanId).First();
                var ITList2 = Veriler.IlanlarTablosu.Where(p => p.IlanId == I.Id).ToList();
                if (ITList2.Count == 1)
                {
                    GorselSil(I.Gorsel1);
                    GorselSil(I.GorselThumbnail1);
                    GorselSil(I.Gorsel2);
                    GorselSil(I.GorselThumbnail2);
                    GorselSil(I.Gorsel3);
                    GorselSil(I.GorselThumbnail3);
                    GorselSil(I.Gorsel4);
                    GorselSil(I.GorselThumbnail4);
                    Veriler.Ilanlar.DeleteObject(I);
                    Veriler.SaveChanges();
                }
                Veriler.IlanlarTablosu.DeleteObject(IT);
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

        private void Guncelle(IlanKategorileri IK)
        {
            // Güncelle
            TextBoxKategoriAdi.Text = IK.KategoriAdi;
            CheckBoxDurum.Checked = IK.Durum;
            HiddenFieldId.Value = IK.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = IK.SiraNo.ToString();
            LabelBaslik.Text = "İlan Kategori Ekle Düzenle";
        }
    }
}