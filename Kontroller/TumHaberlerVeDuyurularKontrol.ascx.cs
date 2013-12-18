using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class TumHaberlerVeDuyurularKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["HaberCode"] != null)
                    {
                        TumHaberleriVer(Convert.ToInt32(Request.QueryString["HaberCode"]));
                    }
                    else if (Request.QueryString["DuyuruCode"] != null)
                    {
                        TumDuyurulariVer(Convert.ToInt32(Request.QueryString["DuyuruCode"]));
                    }
                    else
                    {
                        HaberleriVeDuyurulariVer();
                    }
                }
                else
                {
                    HaberleriVeDuyurulariVer();
                }
                MenuHaberKategoriler.Items.Clear();
                HaberKategorileriVer(MenuHaberKategoriler.Items);
                MenuDuyuruKategoriler.Items.Clear();
                DuyuruKategorileriVer(MenuDuyuruKategoriler.Items);
            }
        }

        private void HaberleriVeDuyurulariVer()
        {
            // Haberleri ve duyuruları ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HDList = new List<HaberlerVeDuyurularList>();
            var HList = (from H in Veriler.Haberler
                         join HT in Veriler.HaberlerTablosu
                             on H.Id equals HT.HaberId
                         join HK in Veriler.HaberKategorileri
                             on HT.KategoriId equals HK.Id
                         where H.Durum
                               && HK.Durum
                               && HK.DilId == DilId
                         orderby H.KayitTarihi descending
                         select new
                                    {
                                        H.Id,
                                        H.Baslik,
                                        H.Ozet,
                                        Tarih = H.KayitTarihi,
                                        Resim =
                             H.GorselThumbnail1 != null
                                 ? H.GorselThumbnail1
                                 : "../App_Themes/MainTheme/Images/noimage.png",
                                    }).Distinct().ToList();
            foreach (var Item in HList)
            {
                HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                HD.Id = Item.Id;
                HD.Baslik = Item.Baslik;
                HD.Ozet = Item.Ozet;
                HD.Tarih = Item.Tarih.Value;
                HD.Resim = Item.Resim;
                HD.Url = "HaberVeDuyuruDetay.aspx?HaberCode=" + Item.Id;
                HDList.Add(HD);
            }
            var DList = (from D in Veriler.Duyurular
                         join DT in Veriler.DuyurularTablosu
                             on D.Id equals DT.DuyuruId
                         join DK in Veriler.DuyuruKategorileri
                             on DT.KategoriId equals DK.Id
                         where D.Durum
                               && DK.Durum
                               && DK.DilId == DilId
                         orderby D.BaslangicTarihi descending
                         select new
                                    {
                                        D.Id,
                                        D.Baslik,
                                        D.Ozet,
                                        Tarih = D.BaslangicTarihi,
                                        Resim =
                             D.GorselThumbnail1 != null
                                 ? D.GorselThumbnail1
                                 : "../App_Themes/MainTheme/Images/noimage.png",
                                    }).Distinct().ToList();
            foreach (var Item in DList)
            {
                HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                HD.Id = Item.Id;
                HD.Baslik = Item.Baslik;
                HD.Ozet = Item.Ozet;
                HD.Tarih = Item.Tarih.Value;
                HD.Resim = Item.Resim;
                HD.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                HDList.Add(HD);
            }
            ListViewVeriler.DataSource = HDList;
            ListViewVeriler.DataBind();
            LiteralSiteMap.Text = "<a href=\"../TumHaberlerVeDuyurular.aspx\">Tüm Haberler ve Duyurular</a>";
        }

        private void TumHaberleriVer(int KategoriId)
        {
            // Tüm haberleri ver
            var HDList = new List<HaberlerVeDuyurularList>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (KategoriId != 0)
            {
                var HList = (from H in Veriler.Haberler
                             join HT in Veriler.HaberlerTablosu
                                 on H.Id equals HT.HaberId
                             join HK in Veriler.HaberKategorileri
                                 on HT.KategoriId equals HK.Id
                             where H.Durum
                                   && HK.Durum
                                   && HK.DilId == DilId
                                   && HK.Id == KategoriId
                             orderby H.KayitTarihi descending
                             select new
                                        {
                                            H.Id,
                                            H.Baslik,
                                            H.Ozet,
                                            Tarih = H.KayitTarihi,
                                            Resim =
                                 H.GorselThumbnail1 != null
                                     ? H.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in HList)
                {
                    HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                    HD.Id = Item.Id;
                    HD.Baslik = Item.Baslik;
                    HD.Ozet = Item.Ozet;
                    HD.Tarih = Item.Tarih.Value;
                    HD.Resim = Item.Resim;
                    HD.Url = "HaberVeDuyuruDetay.aspx?HaberCode=" + Item.Id;
                    HDList.Add(HD);
                }
                ListViewVeriler.DataSource = HDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumHaberlerVeDuyurular.aspx\">Tüm Haberler ve Duyurular</a> / "
                    + "<a href=\"../TumHaberlerVeDuyurular.aspx?HaberCode=0\">Tüm Haberler</a> / "
                    + "<a href=\"../TumHaberlerVeDuyurular.aspx?HaberCode=" + KategoriId + "\">" +
                    Veriler.HaberKategorileri.Where(p => p.Id == KategoriId).FirstOrDefault().KategoriAdi + "</a>";
            }
            else
            {
                var HList = (from H in Veriler.Haberler
                             join HT in Veriler.HaberlerTablosu
                                 on H.Id equals HT.HaberId
                             join HK in Veriler.HaberKategorileri
                                 on HT.KategoriId equals HK.Id
                             where H.Durum
                                   && HK.Durum
                                   && HK.DilId == DilId
                             orderby H.KayitTarihi descending
                             select new
                                        {
                                            H.Id,
                                            H.Baslik,
                                            H.Ozet,
                                            Tarih = H.KayitTarihi,
                                            Resim =
                                 H.GorselThumbnail1 != null
                                     ? H.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in HList)
                {
                    HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                    HD.Id = Item.Id;
                    HD.Baslik = Item.Baslik;
                    HD.Ozet = Item.Ozet;
                    HD.Tarih = Item.Tarih.Value;
                    HD.Resim = Item.Resim;
                    HD.Url = "HaberVeDuyuruDetay.aspx?HaberCode=" + Item.Id;
                    HDList.Add(HD);
                }
                ListViewVeriler.DataSource = HDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumHaberlerVeDuyurular.aspx\">Tüm Haberler ve Duyurular</a> / "
                    + "<a href=\"../TumHaberlerVeDuyurular.aspx?HaberCode=0\">Tüm Haberler</a>";
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

        private void TumDuyurulariVer(int KategoriId)
        {
            // Tüm duyruları ver
            var HDList = new List<HaberlerVeDuyurularList>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (KategoriId != 0)
            {
                var DList = (from D in Veriler.Duyurular
                             join DT in Veriler.DuyurularTablosu
                                 on D.Id equals DT.DuyuruId
                             join DK in Veriler.DuyuruKategorileri
                                 on DT.KategoriId equals DK.Id
                             where D.Durum
                                   && DK.Durum
                                   && DK.DilId == DilId
                                   && DK.Id == KategoriId
                             orderby D.BaslangicTarihi descending
                             select new
                                        {
                                            D.Id,
                                            D.Baslik,
                                            D.Ozet,
                                            Tarih = D.BaslangicTarihi,
                                            Resim =
                                 D.GorselThumbnail1 != null
                                     ? D.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in DList)
                {
                    HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                    HD.Id = Item.Id;
                    HD.Baslik = Item.Baslik;
                    HD.Ozet = Item.Ozet;
                    HD.Tarih = Item.Tarih.Value;
                    HD.Resim = Item.Resim;
                    HD.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                    HDList.Add(HD);
                }
                ListViewVeriler.DataSource = HDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumHaberlerVeDuyurular.aspx\">Tüm Haberler ve Duyurular</a> / "
                    + "<a href=\"../TumHaberlerVeDuyurular.aspx?DuyuruCode=0\">Tüm Duyurular</a> / "
                    + "<a href=\"../TumHaberlerVeDuyurular.aspx?DuyuruCode=" + KategoriId + "\">" +
                    Veriler.DuyuruKategorileri.Where(p => p.Id == KategoriId).FirstOrDefault().KategoriAdi + "</a>";
            }
            else
            {
                var DList = (from D in Veriler.Duyurular
                             join DT in Veriler.DuyurularTablosu
                                 on D.Id equals DT.DuyuruId
                             join DK in Veriler.DuyuruKategorileri
                                 on DT.KategoriId equals DK.Id
                             where D.Durum
                                   && DK.Durum
                                   && DK.DilId == DilId
                             orderby D.BaslangicTarihi descending
                             select new
                                        {
                                            D.Id,
                                            D.Baslik,
                                            D.Ozet,
                                            Tarih = D.BaslangicTarihi,
                                            Resim =
                                 D.GorselThumbnail1 != null
                                     ? D.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in DList)
                {
                    HaberlerVeDuyurularList HD = new HaberlerVeDuyurularList();
                    HD.Id = Item.Id;
                    HD.Baslik = Item.Baslik;
                    HD.Ozet = Item.Ozet;
                    HD.Tarih = Item.Tarih.Value;
                    HD.Resim = Item.Resim;
                    HD.Url = "HaberVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                    HDList.Add(HD);
                }
                ListViewVeriler.DataSource = HDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumHaberlerVeDuyurular.aspx\">Tüm Haberler ve Duyurular</a> / "
                    + "<a href=\"../TumHaberlerVeDuyurular.aspx?DuyuruCode=0\">Tüm Duyurular</a>";
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
    }
}