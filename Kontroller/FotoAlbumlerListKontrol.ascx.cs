using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class FotoAlbumlerListKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        AlbumleriVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                    else
                    {
                        AlbumleriVer();
                    }
                }
                else
                {
                    AlbumleriVer();
                }
                MenuBelediyeAlbumKategoriler.Items.Clear();
                BeldiyeAlbumKategorileriVer(MenuBelediyeAlbumKategoriler.Items);
                MenuPersonelAlbumKategoriler.Items.Clear();
                PersonelAlbumKategorileriVer(MenuPersonelAlbumKategoriler.Items);
            }
        }

        private void AlbumleriVer(int FotoAlbumKategoriId)
        {
            // Albümleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (FotoAlbumKategoriId == 0)
            {
                var AlbumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    join K in Veriler.Kullanicilar
                                        on p1.KaydedenKullaniciId equals K.Id
                                    where p.DilId == DilId
                                          && p.Durum
                                          && p1.Durum
                                          && p.PersonelKategorisiMi
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                               {
                                                   p1.Id,
                                                   Ad = p1.FotoAlbumAdi,
                                                   Tarih = p1.KaydetmeTarihi,
                                                   Ekleyen = "<b>Ekleyen:</b>" + K.Ad + " " + K.Soyad,
                                                   p1.Aciklama,
                                                   GorselThumbnail =
                                        p1.GorselThumbnail != null
                                            ? p1.GorselThumbnail
                                            : "/App_Themes/MainTheme/Images/noimage.png"
                                               }).ToList();
                LiteralSiteMap.Text = "<a href=\"../TumFotoAlbumler.aspx?Code=0\">Personel'den Tüm Albümler</a>";
                if (AlbumlerList.Count != 0)
                {
                    ListView1.DataSource = AlbumlerList;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
                    Page.MetaKeywords = AlbumlerList.FirstOrDefault().Aciklama;
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
                    Page.MetaKeywords = string.Empty;
                }
            }
            else
            {
                var AlbumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    join K in Veriler.Kullanicilar
                                        on p1.KaydedenKullaniciId equals K.Id
                                    where p1.FotoAlbumKategoriId == FotoAlbumKategoriId
                                          && p.DilId == DilId
                                          && p.Durum
                                          && p1.Durum
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                               {
                                                   p1.Id,
                                                   Ad = p1.FotoAlbumAdi,
                                                   Tarih = p1.KaydetmeTarihi,
                                                   Ekleyen = "",
                                                   p1.Aciklama,
                                                   GorselThumbnail =
                                        p1.GorselThumbnail != null
                                            ? p1.GorselThumbnail
                                            : "/App_Themes/MainTheme/Images/noimage.png",
                                               }).ToList();
                if (
                    Veriler.FotoAlbumKategorileri.Where(p => p.Id == FotoAlbumKategoriId).FirstOrDefault().
                        PersonelKategorisiMi)
                {
                    LiteralSiteMap.Text = "<a href=\"../TumFotoAlbumler.aspx?Code=0\">Personel'den Tüm Albümler</a> / ";
                }
                else
                {
                    LiteralSiteMap.Text = "<a href=\"../TumFotoAlbumler.aspx\">Kurum'dan Tüm Albümler</a> / ";
                }
                LiteralSiteMap.Text += "<a href=\"../TumFotoAlbumler.aspx?Code=" + FotoAlbumKategoriId + "\"> "
                                       +
                                       Veriler.FotoAlbumKategorileri.Where(p => p.Id == FotoAlbumKategoriId).
                                           FirstOrDefault().FotoAlbumKategoriAdi + "</a>";
                if (AlbumlerList.Count != 0)
                {
                    ListView1.DataSource = AlbumlerList;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
                    Page.MetaKeywords = AlbumlerList.FirstOrDefault().Aciklama;
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
                    Page.MetaKeywords = string.Empty;
                }
            }
        }

        private void AlbumleriVer()
        {
            // Albümleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var AlbumlerList = (from p in Veriler.FotoAlbumKategorileri
                                join p1 in Veriler.FotoAlbumler
                                    on p.Id equals p1.FotoAlbumKategoriId
                                join K in Veriler.Kullanicilar
                                    on p1.KaydedenKullaniciId equals K.Id
                                where p.DilId == DilId
                                      && p.Durum
                                      && p1.Durum
                                      && p.PersonelKategorisiMi == false
                                orderby p.FotoAlbumKategoriAdi ascending
                                select new
                                           {
                                               p1.Id,
                                               Ad = p1.FotoAlbumAdi,
                                               Tarih = p1.KaydetmeTarihi,
                                               Ekleyen = "",
                                               p1.Aciklama,
                                               GorselThumbnail =
                                    p1.GorselThumbnail != null
                                        ? p1.GorselThumbnail
                                        : "/App_Themes/MainTheme/Images/noimage.png"
                                           }).ToList();
            if (AlbumlerList.Count != 0)
            {
                ListView1.DataSource = AlbumlerList;
                ListView1.DataBind();
                Page.Title = Page.Header.Title + " :: Foto Albümler";
                Page.MetaKeywords = AlbumlerList.FirstOrDefault().Aciklama;
            }
            else
            {
                ListView1.DataSource = null;
                ListView1.DataBind();
                Page.Title = Page.Header.Title + " :: Foto Albümler";
                Page.MetaKeywords = string.Empty;
            }
            LiteralSiteMap.Text = "<a href=\"../TumFotoAlbumler.aspx\">Kurum'dan Tüm Albümler</a>";
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            FotoAlbumler FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/FotoAlbumDetay.aspx?Code=" + Id;
        }

        private void BeldiyeAlbumKategorileriVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAKList =
                Veriler.FotoAlbumKategorileri.Where(p => p.Durum && p.DilId == DilId && p.PersonelKategorisiMi == false)
                    .ToList();
            MenuItem MII = new MenuItem();
            MII.NavigateUrl = "../TumFotoAlbumler.aspx";
            MII.Text = "Kurum'dan Tüm Albümler";
            Items.Add(MII);
            foreach (FotoAlbumKategorileri FAK in FAKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumFotoAlbumler.aspx?Code=" + FAK.Id.ToString();
                MI.Text = FAK.FotoAlbumKategoriAdi;
                Items.Add(MI);
            }
        }

        private void PersonelAlbumKategorileriVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAKList =
                Veriler.FotoAlbumKategorileri.Where(p => p.Durum && p.DilId == DilId && p.PersonelKategorisiMi).ToList();

            MenuItem M1 = new MenuItem();
            M1.NavigateUrl = "../Portal/AlbumListeleri.aspx?Code=AlbumEkle";
            M1.Text = "Yeni Albüm Ekle";
            Items.Add(M1);

            MenuItem M2 = new MenuItem();
            M2.NavigateUrl = "../Portal/AlbumListeleri.aspx";
            M2.Text = "Tüm Albümlerim";
            Items.Add(M2);

            MenuItem M3 = new MenuItem();
            M3.NavigateUrl = "../TumFotoAlbumler.aspx?Code=0";
            M3.Text = "Personel'den Tüm Albümler";
            Items.Add(M3);

            foreach (FotoAlbumKategorileri FAK in FAKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumFotoAlbumler.aspx?Code=" + FAK.Id.ToString();
                MI.Text = FAK.FotoAlbumKategoriAdi;
                Items.Add(MI);
            }
        }
    }
}