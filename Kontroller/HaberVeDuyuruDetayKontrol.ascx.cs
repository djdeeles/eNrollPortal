using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class HaberVeDuyuruDetayKontrol : UserControl
    {
        public static List<Gorseller> GorsellerList;
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HaberVeDuyruDetayVer();
                MenuHaberKategoriler.Items.Clear();
                HaberKategorileriVer(MenuHaberKategoriler.Items);
                MenuDuyuruKategoriler.Items.Clear();
                DuyuruKategorileriVer(MenuDuyuruKategoriler.Items);
            }
        }

        private void HaberVeDuyruDetayVer()
        {
            // Haber veya duyuru detay ver
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["HaberCode"] != null)
                {
                    // Haber

                    # region Haberler

                    int Id = Convert.ToInt32(Request.QueryString["HaberCode"]);
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    var HList = (from H in Veriler.Haberler
                                 join HT in Veriler.HaberlerTablosu
                                     on H.Id equals HT.HaberId
                                 join HK in Veriler.HaberKategorileri
                                     on HT.KategoriId equals HK.Id
                                 where
                                     H.Durum
                                     && HK.Durum
                                     && HK.DilId == DilId
                                     && H.Id == Id
                                 select new
                                            {
                                                H.Id,
                                                H.Baslik,
                                                H.Icerik,
                                                Tarih = H.KayitTarihi,
                                                H.AnahtarKelimeler,
                                                KategoriId = HK.Id,
                                                HK.KategoriAdi,
                                            }).First();
                    LabelBaslik.Text = HList.Baslik;
                    LabelTarih.Text = HList.Tarih.Value.ToString("dd/MM/yyyy");
                    LabelIcerik.Text = HList.Icerik;
                    Page.MetaKeywords = HList.AnahtarKelimeler;
                    Page.Title = Page.Header.Title + " :: " + HList.Baslik;
                    LiteralSiteMap.Text =
                        "<a href=\"../TumHaberlerVeDuyurular.aspx\">Tüm Haberler ve Duyurular</a> / "
                        + "<a href=\"../TumHaberlerVeDuyurular.aspx?HaberCode=0\">Tüm Haberler</a> / "
                        + "<a href=\"../TumHaberlerVeDuyurular.aspx?HaberCode=" + HList.KategoriId + "\"> " +
                        HList.KategoriAdi + "</a>";
                    Haberler HH = Veriler.Haberler.Where(p => p.Id == HList.Id && p.Durum).FirstOrDefault();
                    if (HH != null)
                    {
                        if (HH.OkunmaSayisi == null)
                        {
                            HH.OkunmaSayisi = 1;
                        }
                        else
                        {
                            HH.OkunmaSayisi = HH.OkunmaSayisi + 1;
                        }
                        Veriler.SaveChanges();
                        GorsellerList = new List<Gorseller>();
                        Gorseller G;
                        if (!string.IsNullOrEmpty(HH.GorselThumbnail1))
                        {
                            G = new Gorseller();
                            G.Gorsel = HH.Gorsel1.Replace("~/", "");
                            G.GorselThumbnail = HH.GorselThumbnail1.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(HH.GorselThumbnail2))
                        {
                            G = new Gorseller();
                            G.Gorsel = HH.Gorsel2.Replace("~/", "");
                            G.GorselThumbnail = HH.GorselThumbnail2.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(HH.GorselThumbnail3))
                        {
                            G = new Gorseller();
                            G.Gorsel = HH.Gorsel3.Replace("~/", "");
                            G.GorselThumbnail = HH.GorselThumbnail3.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(HH.GorselThumbnail4))
                        {
                            G = new Gorseller();
                            G.Gorsel = HH.Gorsel4.Replace("~/", "");
                            G.GorselThumbnail = HH.GorselThumbnail4.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (GorsellerList.Count > 1)
                        {
                            DataListResimler.DataSource = GorsellerList;
                            DataListResimler.DataBind();
                        }
                        if (GorsellerList.Count != 0)
                        {
                            ImageHaberGorsel.ImageUrl = "../" + GorsellerList[0].Gorsel;
                        }
                        else
                        {
                            ImageHaberGorsel.ImageUrl = "..//App_Themes/MainTheme/Images/noimage.png";
                        }
                    }

                    #endregion
                }
                else if (Request.QueryString["DuyuruCode"] != null)
                {
                    // Duyru

                    # region Duyurular

                    int Id = Convert.ToInt32(Request.QueryString["DuyuruCode"]);
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    var DList = (from D in Veriler.Duyurular
                                 join DT in Veriler.DuyurularTablosu
                                     on D.Id equals DT.DuyuruId
                                 join DK in Veriler.DuyuruKategorileri
                                     on DT.KategoriId equals DK.Id
                                 where
                                     D.Durum
                                     && DK.Durum
                                     && DK.DilId == DilId
                                     && D.Id == Id
                                 select new
                                            {
                                                D.Id,
                                                D.Baslik,
                                                D.Icerik,
                                                Tarih = D.BaslangicTarihi,
                                                D.AnahtarKelimeler,
                                                KategoriId = DK.Id,
                                                DK.KategoriAdi,
                                            }).First();
                    LabelBaslik.Text = DList.Baslik;
                    LabelTarih.Text = DList.Tarih.Value.ToString("dd/MM/yyyy");
                    LabelIcerik.Text = DList.Icerik;
                    Page.MetaKeywords = DList.AnahtarKelimeler;
                    Page.Title = Page.Header.Title + " :: " + DList.Baslik;
                    LiteralSiteMap.Text =
                        "<a href=\"../TumHaberlerVeDuyurular.aspx\">Tüm Haberler ve Duyurular</a> / "
                        + "<a href=\"../TumHaberlerVeDuyurular.aspx?DuyuruCode=0\">Tüm Haberler</a> / "
                        + "<a href=\"../TumHaberlerVeDuyurular.aspx?DuyuruCode=" + DList.KategoriId + "\"> " +
                        DList.KategoriAdi + "</a>";
                    Duyurular DD = Veriler.Duyurular.Where(p => p.Id == DList.Id && p.Durum).FirstOrDefault();
                    if (DD != null)
                    {
                        if (DD.OkunmaSayisi == null)
                        {
                            DD.OkunmaSayisi = 1;
                        }
                        else
                        {
                            DD.OkunmaSayisi = DD.OkunmaSayisi + 1;
                        }
                        Veriler.SaveChanges();
                        GorsellerList = new List<Gorseller>();
                        Gorseller G;
                        if (!string.IsNullOrEmpty(DD.GorselThumbnail1))
                        {
                            G = new Gorseller();
                            G.Gorsel = DD.Gorsel1.Replace("~/", "");
                            G.GorselThumbnail = DD.GorselThumbnail1.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(DD.GorselThumbnail2))
                        {
                            G = new Gorseller();
                            G.Gorsel = DD.Gorsel2.Replace("~/", "");
                            G.GorselThumbnail = DD.GorselThumbnail2.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(DD.GorselThumbnail3))
                        {
                            G = new Gorseller();
                            G.Gorsel = DD.Gorsel3.Replace("~/", "");
                            G.GorselThumbnail = DD.GorselThumbnail3.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(DD.GorselThumbnail4))
                        {
                            G = new Gorseller();
                            G.Gorsel = DD.Gorsel4.Replace("~/", "");
                            G.GorselThumbnail = DD.GorselThumbnail4.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (GorsellerList.Count > 1)
                        {
                            DataListResimler.DataSource = GorsellerList;
                            DataListResimler.DataBind();
                        }
                        if (GorsellerList.Count != 0)
                        {
                            ImageHaberGorsel.ImageUrl = "../" + GorsellerList[0].Gorsel;
                        }
                        else
                        {
                            ImageHaberGorsel.ImageUrl = "..//App_Themes/MainTheme/Images/noimage.png";
                        }
                    }

                    # endregion
                }
            }
        }

        private void HaberKategorileriVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HKList =
                Veriler.HaberKategorileri.Where(p => p.Durum && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            MenuItem MII = new MenuItem();
            MII.NavigateUrl = "../TumHaberlerVeDuyurular.aspx?HaberCode=0";
            MII.Text = "Tüm Haberler";
            Items.Add(MII);
            foreach (HaberKategorileri HK in HKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumHaberlerVeDuyurular.aspx?HaberCode=" + HK.Id.ToString();
                MI.Text = HK.KategoriAdi;
                Items.Add(MI);
            }
        }

        private void DuyuruKategorileriVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DKList =
                Veriler.DuyuruKategorileri.Where(p => p.Durum && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            MenuItem MII = new MenuItem();
            MII.NavigateUrl = "../TumHaberlerVeDuyurular.aspx?DuyuruCode=0";
            MII.Text = "Tüm Duyurular";
            Items.Add(MII);
            foreach (DuyuruKategorileri DK in DKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumHaberlerVeDuyurular.aspx?DuyuruCode=" + DK.Id.ToString();
                MI.Text = DK.KategoriAdi;
                Items.Add(MI);
            }
        }

        #region Nested type: Gorseller

        public class Gorseller
        {
            public string Gorsel { get; set; }
            public string GorselThumbnail { get; set; }
        }

        #endregion
    }
}