using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class FotoAlbumDetayKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
                {
                    FotoGorselleriVer(Convert.ToInt32(Request.QueryString["Code"]));
                }
                MenuBelediyeAlbumKategoriler.Items.Clear();
                BeldiyeAlbumKategorileriVer(MenuBelediyeAlbumKategoriler.Items);
                MenuPersonelAlbumKategoriler.Items.Clear();
                PersonelAlbumKategorileriVer(MenuPersonelAlbumKategoriler.Items);
            }
        }

        private void FotoGorselleriVer(int FotoAlbumId)
        {
            // Görselleri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FGList = (from p in Veriler.FotoAlbumGorselleri
                          join p1 in Veriler.FotoAlbumler
                              on p.FotoAlbumId equals p1.Id
                          join p2 in Veriler.FotoAlbumKategorileri
                              on p1.FotoAlbumKategoriId equals p2.Id
                          where
                              p.Durum
                              && p1.Durum
                              && p2.Durum
                              && p2.DilId == DilId
                              && p.FotoAlbumId == FotoAlbumId
                          select new
                                     {
                                         p.Id,
                                         GorselThumbnail =
                              p.GorselThumbnail != null ? p.GorselThumbnail : "/App_Themes/MainTheme/Images/noimage.png",
                                         p.GorselAdi,
                                         AlbumAdi = p1.FotoAlbumAdi,
                                     }).Distinct().ToList();
            var PersonelKategorisiMi = (from p in Veriler.FotoAlbumKategorileri
                                        join p1 in Veriler.FotoAlbumler
                                            on p.Id equals p1.FotoAlbumKategoriId
                                        where p1.Id == FotoAlbumId
                                        select new
                                                   {
                                                       p.PersonelKategorisiMi,
                                                       FotoAlbumKategoriId = p.Id,
                                                       p.FotoAlbumKategoriAdi,
                                                       AlbumId = p1.Id,
                                                       AlbumAdi = p1.FotoAlbumAdi,
                                                   }).FirstOrDefault();

            if (PersonelKategorisiMi.PersonelKategorisiMi)
            {
                LiteralSiteMap.Text = "<a href=\"../TumFotoAlbumler.aspx?Code=0\">Personel'den Tüm Albümler</a> / ";
            }
            else
            {
                LiteralSiteMap.Text = "<a href=\"../TumFotoAlbumler.aspx\">Kurum'dan Tüm Albümler</a> / ";
            }
            LiteralSiteMap.Text += "<a href=\"../TumFotoAlbumler.aspx?Code="
                                   + PersonelKategorisiMi.FotoAlbumKategoriId + "\"> "
                                   + PersonelKategorisiMi.FotoAlbumKategoriAdi + "</a> / ";
            LiteralSiteMap.Text += "<a href=\"../FotoAlbumDetay.aspx?Code="
                                   + PersonelKategorisiMi.AlbumId + "\"> "
                                   + PersonelKategorisiMi.AlbumAdi + "</a>";

            if (FGList.Count != 0)
            {
                ListView1.DataSource = FGList;
                ListView1.DataBind();
            }
            else
            {
                ListView1.DataSource = null;
                ListView1.DataBind();
            }
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            FotoAlbumGorselleri FG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
            HL.NavigateUrl = FG.Gorsel.Replace("~/", "../");
            HL.ToolTip = FG.GorselAdi + FG.Aciklama;
        }

        protected void Image1_DataBinding(object sender, EventArgs e)
        {
            Image Resim = (Image) sender;
            int Id = Convert.ToInt32(Resim.ImageUrl);
            FotoAlbumGorselleri FG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
            Resim.ImageUrl = FG.GorselThumbnail.Replace("~/", "../");
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
            // Albümleri ver
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