using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollPortal.Kutuphaneler;
using EnrollPortal.Kutuphaneler.DataModels;

namespace EnrollPortal.Kontroller
{
    public partial class IlanVeDuyuruDetayKontrol : UserControl
    {
        public static List<Gorseller> GorsellerList;
        private readonly EnrollPortalEntities Veriler = new EnrollPortalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IlanVeDuyruDetayVer();
                MenuIlanKategoriler.Items.Clear();
                IlanKategorileriVer(MenuIlanKategoriler.Items);
                MenuDuyuruKategoriler.Items.Clear();
                DuyuruKategorileriVer(MenuDuyuruKategoriler.Items);
            }
        }

        private void IlanVeDuyruDetayVer()
        {
            // İlan veya duyuru ver
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["IlanCode"] != null)
                {
                    # region Haberler

                    int Id = Convert.ToInt32(Request.QueryString["IlanCode"]);
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    var IList = (from I in Veriler.Ilanlar
                                 join IT in Veriler.IlanlarTablosu
                                     on I.Id equals IT.IlanId
                                 join IK in Veriler.IlanKategorileri
                                     on IT.KategoriId equals IK.Id
                                 join K in Veriler.Kullanicilar
                                     on I.KaydedenKullaniciId equals K.Id
                                 where
                                     I.Durum
                                     && IK.Durum
                                     && IK.DilId == DilId
                                     && I.Id == Id
                                 select new
                                            {
                                                I.Id,
                                                I.Baslik,
                                                I.Icerik,
                                                Tarih = I.BaslangicTarihi,
                                                Ekleyen = K.Ad + " " + K.Soyad,
                                                I.AnahtarKelimeler,
                                                KategoriId = IK.Id,
                                                IK.KategoriAdi,
                                            }).First();
                    LabelBaslik.Text = IList.Baslik;
                    LabelTarih.Text = IList.Tarih.Value.ToString("dd/MM/yyyy") + " <b>Ekleyen:</b>" + IList.Ekleyen;
                    LabelIcerik.Text = IList.Icerik;
                    Page.MetaKeywords = IList.AnahtarKelimeler;
                    Page.Title = Page.Header.Title + " :: " + IList.Baslik;
                    LiteralSiteMap.Text =
                        "<a href=\"../TumIlanlarVeDuyurular.aspx\">Tüm İlanlar ve Duyurular</a> / "
                        + "<a href=\"../TumIlanlarVeDuyurular.aspx?IlanCode=0\">Tüm İlanlar</a> / "
                        + "<a href=\"../TumIlanlarVeDuyurular.aspx?IlanCode=" + IList.KategoriId + "\"> " +
                        IList.KategoriAdi + "</a>";
                    Ilanlar II = Veriler.Ilanlar.Where(p => p.Id == IList.Id && p.Durum).FirstOrDefault();
                    if (II != null)
                    {
                        if (II.OkunmaSayisi == null)
                        {
                            II.OkunmaSayisi = 1;
                        }
                        else
                        {
                            II.OkunmaSayisi = II.OkunmaSayisi + 1;
                        }
                        Veriler.SaveChanges();
                        GorsellerList = new List<Gorseller>();
                        Gorseller G;
                        if (!string.IsNullOrEmpty(II.GorselThumbnail1))
                        {
                            G = new Gorseller();
                            G.Gorsel = II.Gorsel1.Replace("~/", "");
                            G.GorselThumbnail = II.GorselThumbnail1.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(II.GorselThumbnail2))
                        {
                            G = new Gorseller();
                            G.Gorsel = II.Gorsel2.Replace("~/", "");
                            G.GorselThumbnail = II.GorselThumbnail2.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(II.GorselThumbnail3))
                        {
                            G = new Gorseller();
                            G.Gorsel = II.Gorsel3.Replace("~/", "");
                            G.GorselThumbnail = II.GorselThumbnail3.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(II.GorselThumbnail4))
                        {
                            G = new Gorseller();
                            G.Gorsel = II.Gorsel4.Replace("~/", "");
                            G.GorselThumbnail = II.GorselThumbnail4.Replace("~/", "");
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
                    # region Duyurular

                    int Id = Convert.ToInt32(Request.QueryString["DuyuruCode"]);
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    var PDList = (from PD in Veriler.PersonelDuyurular
                                  join PDT in Veriler.PersonelDuyurularTablosu
                                      on PD.Id equals PDT.DuyuruId
                                  join PDK in Veriler.PersonelDuyuruKategorileri
                                      on PDT.KategoriId equals PDK.Id
                                  join K in Veriler.Kullanicilar
                                      on PD.KaydedenKullaniciId equals K.Id
                                  where
                                      PD.Durum
                                      && PDK.Durum
                                      && PDK.DilId == DilId
                                      && PD.Id == Id
                                  select new
                                             {
                                                 PD.Id,
                                                 PD.Baslik,
                                                 PD.Icerik,
                                                 Tarih = PD.BaslangicTarihi,
                                                 Ekleyen = K.Ad + " " + K.Soyad,
                                                 PD.AnahtarKelimeler,
                                                 KategoriId = PDK.Id,
                                                 PDK.KategoriAdi,
                                             }).First();
                    LabelBaslik.Text = PDList.Baslik;
                    LabelTarih.Text = PDList.Tarih.Value.ToString("dd/MM/yyyy") + " <b>Ekleyen:</b>" + PDList.Ekleyen;
                    LabelIcerik.Text = PDList.Icerik;
                    Page.MetaKeywords = PDList.AnahtarKelimeler;
                    Page.Title = Page.Header.Title + " :: " + PDList.Baslik;
                    LiteralSiteMap.Text =
                        "<a href=\"../TumIlanlarVeDuyurular.aspx\">Tüm İlanlar ve Duyurular</a> / "
                        + "<a href=\"../TumIlanlarVeDuyurular.aspx?DuyuruCode=0\">Tüm Duyurular</a> / "
                        + "<a href=\"../TumIlanlarVeDuyurular.aspx?DuyuruCode=" + PDList.KategoriId + "\"> " +
                        PDList.KategoriAdi + "</a>";
                    PersonelDuyurular PDD =
                        Veriler.PersonelDuyurular.Where(p => p.Id == PDList.Id && p.Durum).FirstOrDefault();
                    if (PDD != null)
                    {
                        if (PDD.OkunmaSayisi == null)
                        {
                            PDD.OkunmaSayisi = 1;
                        }
                        else
                        {
                            PDD.OkunmaSayisi = PDD.OkunmaSayisi + 1;
                        }
                        Veriler.SaveChanges();
                        GorsellerList = new List<Gorseller>();
                        Gorseller G;
                        if (!string.IsNullOrEmpty(PDD.GorselThumbnail1))
                        {
                            G = new Gorseller();
                            G.Gorsel = PDD.Gorsel1.Replace("~/", "");
                            G.GorselThumbnail = PDD.GorselThumbnail1.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(PDD.GorselThumbnail2))
                        {
                            G = new Gorseller();
                            G.Gorsel = PDD.Gorsel2.Replace("~/", "");
                            G.GorselThumbnail = PDD.GorselThumbnail2.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(PDD.GorselThumbnail3))
                        {
                            G = new Gorseller();
                            G.Gorsel = PDD.Gorsel3.Replace("~/", "");
                            G.GorselThumbnail = PDD.GorselThumbnail3.Replace("~/", "");
                            GorsellerList.Add(G);
                        }
                        if (!string.IsNullOrEmpty(PDD.GorselThumbnail4))
                        {
                            G = new Gorseller();
                            G.Gorsel = PDD.Gorsel4.Replace("~/", "");
                            G.GorselThumbnail = PDD.GorselThumbnail4.Replace("~/", "");
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

        private void DuyuruKategorileriVer(MenuItemCollection Items)
        {
            // Kategorileri ver
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

        #region Nested type: Gorseller

        public class Gorseller
        {
            public string Gorsel { get; set; }
            public string GorselThumbnail { get; set; }
        }

        #endregion
    }
}