using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class TumIlanlarVeDuyurularKontrol : UserControl
    {
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["IlanCode"] != null)
                    {
                        TumIlanlariVer(Convert.ToInt32(Request.QueryString["IlanCode"]));
                    }
                    else if (Request.QueryString["DuyuruCode"] != null)
                    {
                        TumDuyurulariVer(Convert.ToInt32(Request.QueryString["DuyuruCode"]));
                    }
                    else
                    {
                        IlanlariVeDuyurulariVer();
                    }
                }
                else
                {
                    IlanlariVeDuyurulariVer();
                }
                MenuIlanKategoriler.Items.Clear();
                IlanKategorileriVer(MenuIlanKategoriler.Items);
                MenuDuyuruKategoriler.Items.Clear();
                DuyuruKategorileriVer(MenuDuyuruKategoriler.Items);
            }
        }

        private void IlanlariVeDuyurulariVer()
        {
            // İlanları ve duyuruları ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IDList = new List<IlanlarVeDuyurularList>();
            var IList = (from I in Veriler.Ilanlar
                         join IT in Veriler.IlanlarTablosu
                             on I.Id equals IT.IlanId
                         join IK in Veriler.IlanKategorileri
                             on IT.KategoriId equals IK.Id
                         join K in Veriler.Kullanicilar
                             on I.KaydedenKullaniciId equals K.Id
                         where I.Durum
                               && IK.Durum
                               && IK.DilId == DilId
                         orderby I.BaslangicTarihi descending
                         select new
                                    {
                                        I.Id,
                                        I.Baslik,
                                        I.Ozet,
                                        Tarih = I.BaslangicTarihi,
                                        Ekleyen = K.Ad + " " + K.Soyad,
                                        Resim =
                             I.GorselThumbnail1 != null
                                 ? I.GorselThumbnail1
                                 : "../App_Themes/MainTheme/Images/noimage.png",
                                    }).Distinct().ToList();
            foreach (var Item in IList)
            {
                IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                ID.Id = Item.Id;
                ID.Baslik = Item.Baslik;
                ID.Ozet = Item.Ozet;
                ID.Tarih = Item.Tarih.Value;
                ID.Ekleyen = Item.Ekleyen;
                ID.Resim = Item.Resim;
                ID.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + Item.Id;
                IDList.Add(ID);
            }
            var PDList = (from PD in Veriler.PersonelDuyurular
                          join PDT in Veriler.PersonelDuyurularTablosu
                              on PD.Id equals PDT.DuyuruId
                          join PDK in Veriler.PersonelDuyuruKategorileri
                              on PDT.KategoriId equals PDK.Id
                          join K in Veriler.Kullanicilar
                              on PD.KaydedenKullaniciId equals K.Id
                          where PD.Durum
                                && PDK.Durum
                                && PDK.DilId == DilId
                          orderby PD.BaslangicTarihi descending
                          select new
                                     {
                                         PD.Id,
                                         PD.Baslik,
                                         PD.Ozet,
                                         Tarih = PD.BaslangicTarihi,
                                         Ekleyen = K.Ad + " " + K.Soyad,
                                         Resim =
                              PD.GorselThumbnail1 != null
                                  ? PD.GorselThumbnail1
                                  : "../App_Themes/MainTheme/Images/noimage.png",
                                     }).Distinct().ToList();
            foreach (var Item in PDList)
            {
                IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                ID.Id = Item.Id;
                ID.Baslik = Item.Baslik;
                ID.Ozet = Item.Ozet;
                ID.Tarih = Item.Tarih.Value;
                ID.Ekleyen = Item.Ekleyen;
                ID.Resim = Item.Resim;
                ID.Url = "IlanVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                IDList.Add(ID);
            }
            ListViewVeriler.DataSource = IDList;
            ListViewVeriler.DataBind();
            LiteralSiteMap.Text = "<a href=\"../TumIlanlarVeDuyurular.aspx\">Tüm İlanlar ve Duyurular</a>";
        }

        private void TumIlanlariVer(int KategoriId)
        {
            // tüm ilanları ver
            var IDList = new List<IlanlarVeDuyurularList>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (KategoriId != 0)
            {
                var IList = (from I in Veriler.Ilanlar
                             join IT in Veriler.IlanlarTablosu
                                 on I.Id equals IT.IlanId
                             join IK in Veriler.IlanKategorileri
                                 on IT.KategoriId equals IK.Id
                             join K in Veriler.Kullanicilar
                                 on I.KaydedenKullaniciId equals K.Id
                             where I.Durum
                                   && IK.Durum
                                   && IK.DilId == DilId
                                   && IK.Id == KategoriId
                             orderby I.BaslangicTarihi descending
                             select new
                                        {
                                            I.Id,
                                            I.Baslik,
                                            I.Ozet,
                                            Tarih = I.BaslangicTarihi,
                                            Ekleyen = K.Ad + " " + K.Soyad,
                                            Resim =
                                 I.GorselThumbnail1 != null
                                     ? I.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in IList)
                {
                    IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                    ID.Id = Item.Id;
                    ID.Baslik = Item.Baslik;
                    ID.Ozet = Item.Ozet;
                    ID.Tarih = Item.Tarih.Value;
                    ID.Ekleyen = Item.Ekleyen;
                    ID.Resim = Item.Resim;
                    ID.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + Item.Id;
                    IDList.Add(ID);
                }
                ListViewVeriler.DataSource = IDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumIlanlarVeDuyurular.aspx\">Tüm İlanlar ve Duyurular</a> / "
                    + "<a href=\"../TumIlanlarVeDuyurular.aspx?IlanCode=0\">Tüm İlanlar</a> / "
                    + "<a href=\"../TumIlanlarVeDuyurular.aspx?IlanCode=" + KategoriId + "\">" +
                    Veriler.IlanKategorileri.Where(p => p.Id == KategoriId).FirstOrDefault().KategoriAdi + "</a>";
            }
            else
            {
                var IList = (from I in Veriler.Ilanlar
                             join IT in Veriler.IlanlarTablosu
                                 on I.Id equals IT.IlanId
                             join IK in Veriler.IlanKategorileri
                                 on IT.KategoriId equals IK.Id
                             join K in Veriler.Kullanicilar
                                 on I.KaydedenKullaniciId equals K.Id
                             where I.Durum
                                   && IK.Durum
                                   && IK.DilId == DilId
                             orderby I.BaslangicTarihi descending
                             select new
                                        {
                                            I.Id,
                                            I.Baslik,
                                            I.Ozet,
                                            Tarih = I.BaslangicTarihi,
                                            Ekleyen = K.Ad + " " + K.Soyad,
                                            Resim =
                                 I.GorselThumbnail1 != null
                                     ? I.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in IList)
                {
                    IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                    ID.Id = Item.Id;
                    ID.Baslik = Item.Baslik;
                    ID.Ozet = Item.Ozet;
                    ID.Tarih = Item.Tarih.Value;
                    ID.Ekleyen = Item.Ekleyen;
                    ID.Resim = Item.Resim;
                    ID.Url = "IlanVeDuyuruDetay.aspx?IlanCode=" + Item.Id;
                    IDList.Add(ID);
                }
                ListViewVeriler.DataSource = IDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumIlanlarVeDuyurular.aspx\">Tüm İlanlar ve Duyurular</a> / "
                    + "<a href=\"../TumIlanlarVeDuyurular.aspx?IlanCode=0\">Tüm İlanlar</a>";
            }
        }

        private void IlanKategorileriVer(MenuItemCollection Items)
        {
            // Kategorileri ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IKList =
                Veriler.IlanKategorileri.Where(p => p.Durum && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            MenuItem M1 = new MenuItem();
            M1.NavigateUrl = "../Portal/IlanVeDuyuruListeleri.aspx?IlanCode=IlanEkle";
            M1.Text = "Yeni İlan Ekle";
            Items.Add(M1);

            MenuItem M2 = new MenuItem();
            M2.NavigateUrl = "..//Portal/IlanVeDuyuruListeleri.aspx?IlanCode=0";
            M2.Text = "İlanlarım";
            Items.Add(M2);

            MenuItem MII = new MenuItem();
            MII.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?IlanCode=0";
            MII.Text = "Tüm İlanlar";
            Items.Add(MII);

            foreach (IlanKategorileri IK in IKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?IlanCode=" + IK.Id.ToString();
                MI.Text = IK.KategoriAdi;
                Items.Add(MI);
            }
        }

        private void TumDuyurulariVer(int KategoriId)
        {
            // Duyuruları ver
            var IDList = new List<IlanlarVeDuyurularList>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (KategoriId != 0)
            {
                var DList = (from PD in Veriler.PersonelDuyurular
                             join PDT in Veriler.PersonelDuyurularTablosu
                                 on PD.Id equals PDT.DuyuruId
                             join PDK in Veriler.PersonelDuyuruKategorileri
                                 on PDT.KategoriId equals PDK.Id
                             join K in Veriler.Kullanicilar
                                 on PD.KaydedenKullaniciId equals K.Id
                             where PD.Durum
                                   && PDK.Durum
                                   && PDK.DilId == DilId
                                   && PDK.Id == KategoriId
                             orderby PD.BaslangicTarihi descending
                             select new
                                        {
                                            PD.Id,
                                            PD.Baslik,
                                            PD.Ozet,
                                            Tarih = PD.BaslangicTarihi,
                                            Ekleyen = K.Ad + " " + K.Soyad,
                                            Resim =
                                 PD.GorselThumbnail1 != null
                                     ? PD.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in DList)
                {
                    IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                    ID.Id = Item.Id;
                    ID.Baslik = Item.Baslik;
                    ID.Ozet = Item.Ozet;
                    ID.Tarih = Item.Tarih.Value;
                    ID.Ekleyen = Item.Ekleyen;
                    ID.Resim = Item.Resim;
                    ID.Url = "IlanVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                    IDList.Add(ID);
                }
                ListViewVeriler.DataSource = IDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumIlanlarVeDuyurular.aspx\">Tüm İlanlar ve Duyurular</a> / "
                    + "<a href=\"../TumIlanlarVeDuyurular.aspx?DuyuruCode=0\">Tüm Duyurular</a> / "
                    + "<a href=\"../TumIlanlarVeDuyurular.aspx?DuyuruCode=" + KategoriId + "\">" +
                    Veriler.PersonelDuyuruKategorileri.Where(p => p.Id == KategoriId).FirstOrDefault().KategoriAdi +
                    "</a>";
            }
            else
            {
                var DList = (from PD in Veriler.PersonelDuyurular
                             join PDT in Veriler.PersonelDuyurularTablosu
                                 on PD.Id equals PDT.DuyuruId
                             join PDK in Veriler.PersonelDuyuruKategorileri
                                 on PDT.KategoriId equals PDK.Id
                             join K in Veriler.Kullanicilar
                                 on PD.KaydedenKullaniciId equals K.Id
                             where PD.Durum
                                   && PDK.Durum
                                   && PDK.DilId == DilId
                             orderby PD.BaslangicTarihi descending
                             select new
                                        {
                                            PD.Id,
                                            PD.Baslik,
                                            PD.Ozet,
                                            Tarih = PD.BaslangicTarihi,
                                            Ekleyen = K.Ad + " " + K.Soyad,
                                            Resim =
                                 PD.GorselThumbnail1 != null
                                     ? PD.GorselThumbnail1.Replace("~/", "../")
                                     : "../App_Themes/MainTheme/Images/noimage.png",
                                        }).Distinct().ToList();
                foreach (var Item in DList)
                {
                    IlanlarVeDuyurularList ID = new IlanlarVeDuyurularList();
                    ID.Id = Item.Id;
                    ID.Baslik = Item.Baslik;
                    ID.Ozet = Item.Ozet;
                    ID.Tarih = Item.Tarih.Value;
                    ID.Ekleyen = Item.Ekleyen;
                    ID.Resim = Item.Resim;
                    ID.Url = "IlanVeDuyuruDetay.aspx?DuyuruCode=" + Item.Id;
                    IDList.Add(ID);
                }
                ListViewVeriler.DataSource = IDList;
                ListViewVeriler.DataBind();
                LiteralSiteMap.Text =
                    "<a href=\"../TumIlanlarVeDuyurular.aspx\">Tüm İlanlar ve Duyurular</a> / "
                    + "<a href=\"../TumIlanlarVeDuyurular.aspx?DuyuruCode=0\">Tüm Duyurular</a>";
            }
        }

        private void DuyuruKategorileriVer(MenuItemCollection Items)
        {
            // Kategoriler ver
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var PDKList =
                Veriler.PersonelDuyuruKategorileri.Where(p => p.Durum && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList
                    ();

            MenuItem M1 = new MenuItem();
            M1.NavigateUrl = "../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=DuyuruEkle";
            M1.Text = "Yeni Duyuru Ekle";
            Items.Add(M1);

            MenuItem M2 = new MenuItem();
            M2.NavigateUrl = "../Portal/IlanVeDuyuruListeleri.aspx?DuyuruCode=0";
            M2.Text = "Duyurularım";
            Items.Add(M2);

            MenuItem MII = new MenuItem();
            MII.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?DuyuruCode=0";
            MII.Text = "Tüm Duyurular";
            Items.Add(MII);
            foreach (PersonelDuyuruKategorileri PDK in PDKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../TumIlanlarVeDuyurular.aspx?DuyuruCode=" + PDK.Id.ToString();
                MI.Text = PDK.KategoriAdi;
                Items.Add(MI);
            }
        }
    }
}