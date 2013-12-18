using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;
using Telerik.Web.UI;

namespace EnrollPortal.Kontroller
{
    public partial class ForumKonuDetayKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["KonuId"] != null)
                    {
                        KonuVer(Convert.ToInt32(Request.QueryString["KonuId"]));
                        PanelKonuDuzenle.Visible = false;
                    }
                }
            }
            MesajKontrol1.Reset();
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["Sayfa"] != null)
                {
                    if (Request.QueryString["Sayfa"] == "1")
                    {
                        PanelKonu.Visible = true;
                    }
                    else
                    {
                        PanelKonu.Visible = false;
                    }
                }
                else
                {
                    PanelKonu.Visible = true;
                }
            }
            else
            {
                PanelKonu.Visible = true;
            }
        }

        private void KonuVer(int KonuId)
        {
            // Konu ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Konu = (from FKO in Veriler.ForumKonulari
                        join FKA in Veriler.ForumKategorileri
                            on FKO.ForumKategoriId equals FKA.Id
                        join K in Veriler.Kullanicilar
                            on FKO.KaydedenKullaniciId equals K.Id
                        where FKO.Durum
                              && FKA.Durum
                              && FKA.DilId == DilId
                        where FKO.Id == KonuId
                        select new
                                   {
                                       FKO.Id,
                                       FKO.Baslik,
                                       Ekleyen = K.Ad + " " + K.Soyad,
                                       Resim = "/App_Themes/MainTheme/Images/noimage.png",
                                       Tarih = FKO.KaydetmeTarihi,
                                       FKO.Icerik,
                                       KategoriId = FKA.Id,
                                       FKA.KategoriAdi,
                                       FKO.KaydedenKullaniciId,
                                       Duzenleme = FKO.DuzenlemeTarihi,
                                   }).FirstOrDefault();
            LabelBaslik.Text = Konu.Baslik;
            LabelKonuyuEkleyen.Text = Konu.Ekleyen;
            ImageResim.ImageUrl = Konu.Resim;
            LabelTarih.Text = "<b>Konu Tarihi:</b>" + Konu.Tarih.ToString();
            LabelIcerik.Text = Konu.Icerik;
            if (Konu.Duzenleme != null)
            {
                LabelDuzenleme.Text = Konu.Duzenleme.ToString() + " tarihide düzenlenmiştir.";
            }
            MesajlariVer(KonuId);
            LiteralSiteMap.Text = "<a href=\"../Forumlar.aspx\">Tüm Forumlar</a> / ";
            LiteralSiteMap.Text
                += "<a href=\"../Forumlar.aspx?KategoriId="
                   + Konu.KategoriId
                   + "\">"
                   + Konu.KategoriAdi
                   + "</a>";
            PanelKonuGuncelle.Visible = YetkiKontrol(Konu.KaydedenKullaniciId);
            if (!IsPostBack)
            {
                ForumKonulari FK = Veriler.ForumKonulari.Where(p => p.Id == KonuId).FirstOrDefault();
                FK.OkunmaSayisi = FK.OkunmaSayisi + 1;
                Veriler.SaveChanges();
            }
        }

        private bool YetkiKontrol(int KaydedenKullaniciId)
        {
            // Yetki kontrol
            bool Durum = false;
            int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            if (Enroll.YetkiAlaniKontrol(KullaniciId, 12))
            {
                Durum = true;
            }
            else
            {
                if (KullaniciId == KaydedenKullaniciId)
                {
                    Durum = true;
                }
            }
            return Durum;
        }

        private void MesajlariVer(int KonuId)
        {
            // Mesajları ver
            var FMList = new List<ForumMesajlarList>();
            var FList = (from FM in Veriler.ForumMesajlari
                         join K in Veriler.Kullanicilar
                             on FM.KaydedenKullaniciId equals K.Id
                         where FM.Durum
                               && FM.ForumKonuId == KonuId
                         orderby FM.KaydetmeTarihi ascending
                         select new
                                    {
                                        FM.Id,
                                        Ekleyen = K.Ad + " " + K.Soyad,
                                        Resim = "/App_Themes/MainTheme/Images/noimage.png",
                                        Tarih = FM.KaydetmeTarihi,
                                        FM.Mesaj,
                                        FM.KaydedenKullaniciId,
                                        FM.DuzenlemeTarihi,
                                    }).ToList();
            foreach (var Item in FList)
            {
                ForumMesajlarList F = new ForumMesajlarList();
                F.Id = Item.Id;
                F.Ekleyen = Item.Ekleyen;
                F.Resim = Item.Resim;
                F.Tarih = Item.Tarih;
                F.Mesaj = Item.Mesaj;
                F.KaydedenKullaniciId = Item.KaydedenKullaniciId;
                if (Item.DuzenlemeTarihi != null)
                {
                    F.Duzenleme = Item.DuzenlemeTarihi.ToString() + " tarihide düzenlenmiştir.";
                }
                FMList.Add(F);
            }
            ListViewVeriler.DataSource = FMList;
            ListViewVeriler.DataBind();
        }

        protected void LinkButtonCevapYaz_Click(object sender, EventArgs e)
        {
            // Cevap yaz
            ForumMesajlari FM = new ForumMesajlari();
            FM.ForumKonuId = Convert.ToInt32(Request.QueryString["KonuId"]);
            FM.Mesaj = RadEditorMesaj.Content;
            FM.Durum = true;
            FM.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            FM.KaydetmeTarihi = DateTime.Now;
            Veriler.AddToForumMesajlari(FM);
            Veriler.SaveChanges();
            MesajlariVer(Convert.ToInt32(Request.QueryString["KonuId"]));
            RadEditorMesaj.Content = string.Empty;
        }

        protected void LinkButtonDuzenle_Click(object sender, EventArgs e)
        {
            // Güncelle
            try
            {
                PanelKonu.Visible = false;
                PanelKonuDuzenle.Visible = true;
                int KonuId = Convert.ToInt32(Request.QueryString["KonuId"]);
                ForumKonulari FK = Veriler.ForumKonulari.Where(p => p.Id == KonuId).FirstOrDefault();
                TextBoxBaslik.Text = FK.Baslik;
                RadEditorKonu.Content = FK.Icerik;
            }
            catch
            {
                PanelKonu.Visible = true;
                PanelKonuDuzenle.Visible = false;
                MesajKontrol1.Mesaj(false, "Hata oluştu!. Daha sonra tekar deneyiniz.");
            }
        }

        protected void LinkButtonSil_Click(object sender, EventArgs e)
        {
            // Sil
            try
            {
                int KonuId = Convert.ToInt32(Request.QueryString["KonuId"]);
                ForumKonulari FK = Veriler.ForumKonulari.Where(p => p.Id == KonuId).FirstOrDefault();
                Veriler.ForumKonulari.DeleteObject(FK);
                Veriler.SaveChanges();
                Response.Redirect("../Forumlar.aspx");
            }
            catch
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu!. Daha sonra tekar deneyiniz.");
            }
        }

        protected void LinkButtonKonuKaydet_Click(object sender, EventArgs e)
        {
            // Kaydet
            try
            {
                int KonuId = Convert.ToInt32(Request.QueryString["KonuId"]);
                ForumKonulari FK = Veriler.ForumKonulari.Where(p => p.Id == KonuId).FirstOrDefault();
                FK.Baslik = TextBoxBaslik.Text;
                FK.Icerik = RadEditorKonu.Content;
                FK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                FK.DuzenlemeTarihi = DateTime.Now;
                Veriler.SaveChanges();
                KonuVer(Convert.ToInt32(Request.QueryString["KonuId"]));
                PanelKonuDuzenle.Visible = false;
                MesajKontrol1.Mesaj(true, "Kayıt edildi");
            }
            catch
            {
                PanelKonuDuzenle.Visible = true;
                MesajKontrol1.Mesaj(false, "Hata oluştu!. Daha sonra tekar deneyiniz.");
            }
        }

        protected void LinkButtonKonuIptal_Click(object sender, EventArgs e)
        {
            KonuVer(Convert.ToInt32(Request.QueryString["KonuId"]));
            PanelKonuDuzenle.Visible = false;
        }

        protected void ListViewVeriler_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                try
                {
                    int KaydedenKullaniciId =
                        Convert.ToInt32((e.Item.FindControl("HiddenFieldKaydedenKullaniciId") as HiddenField).Value);
                    (e.Item.FindControl("LinkButtonEdit") as LinkButton).Visible
                        = YetkiKontrol(KaydedenKullaniciId);
                    (e.Item.FindControl("LinkButtonDelete") as LinkButton).Visible
                        = YetkiKontrol(KaydedenKullaniciId);
                }
                catch
                {
                    //
                }
            }
        }

        protected void ListViewVeriler_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewVeriler.EditIndex = e.NewEditIndex;
            KonuVer(Convert.ToInt32(Request.QueryString["KonuId"]));
        }

        protected void ListViewVeriler_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ListViewVeriler.EditIndex = -1;
            KonuVer(Convert.ToInt32(Request.QueryString["KonuId"]));
        }

        protected void ListViewVeriler_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            int MesajId = (int) ListViewVeriler.DataKeys[e.ItemIndex].Value;
            int KullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            ForumMesajlari FM = Veriler.ForumMesajlari.Where(p => p.Id == MesajId).FirstOrDefault();
            FM.Mesaj = (ListViewVeriler.EditItem.FindControl("RadEditorMesaj") as RadEditor).Content;
            FM.DuzenleyenKullaniciId = KullaniciId;
            FM.DuzenlemeTarihi = DateTime.Now;
            Veriler.SaveChanges();
            ListViewVeriler.EditIndex = -1;
            KonuVer(Convert.ToInt32(Request.QueryString["KonuId"]));
        }

        protected void ListViewVeriler_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            int MesajId = (int) ListViewVeriler.DataKeys[e.ItemIndex].Value;
            ForumMesajlari FM = Veriler.ForumMesajlari.Where(p => p.Id == MesajId).FirstOrDefault();
            Veriler.ForumMesajlari.DeleteObject(FM);
            Veriler.SaveChanges();
            ListViewVeriler.EditIndex = -1;
            KonuVer(Convert.ToInt32(Request.QueryString["KonuId"]));
        }
    }
}